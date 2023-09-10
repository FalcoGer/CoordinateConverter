using CoordinateConverter.DCS.Communication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace CoordinateConverter.DCS.Aircraft
{
    /// <summary>
    /// This class interfaces with DCS
    /// </summary>
    public static class DCSConnection
    {
        /// <summary>
        /// The TCP endpoint
        /// </summary>
        public static readonly System.Net.IPEndPoint TCP_ENDPOINT = new System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 42020);
        private static DateTime lastConnectionAttempt = DateTime.MinValue;

        private const int BUFFER_SIZE = 4096;
        private static TimeSpan TIMEOUT_TIMESPAN = TimeSpan.FromMilliseconds(2500);

        /// <summary>
        /// Sends the request.
        /// </summary>
        /// <param name="requestMessage">The requests.</param>
        /// <returns>The response from the remote host</returns>
        public static DCSMessage sendRequest(DCSMessage requestMessage)
        {
            if ((DateTime.Now - lastConnectionAttempt).TotalSeconds < 15)
            {
                // FIXME: async solution!
                // don't try to reconnect
                return null;
            }
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings() {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.None,
                TypeNameHandling = TypeNameHandling.None
            };
            string json = JsonConvert.SerializeObject(requestMessage, jsonSerializerSettings) + '\n';
            byte[] data = Encoding.UTF8.GetBytes(json);

            string responseString = String.Empty;   // string returned by the server
            DCSMessage responseMessage = null;
            try
            {
                // open TCP socket to DCS
                using (Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    sock.SendTimeout = 100;
                    sock.ReceiveTimeout = 100;
                    sock.Connect(TCP_ENDPOINT);
                    sock.Send(data);

                    DateTime timeoutTime = DateTime.Now + TIMEOUT_TIMESPAN;
                    // wait for the answer
                    do
                    {
                        // read the answer
                        while (sock.Available > 0)
                        {
                            byte[] buffer = new byte[BUFFER_SIZE]; // buffer to return the bytes back into
                            int byteCount = Math.Min(sock.Available, BUFFER_SIZE);
                            sock.Receive(buffer, byteCount, SocketFlags.None);
                            responseString += Encoding.UTF8.GetString(buffer);
                            timeoutTime = DateTime.Now + TIMEOUT_TIMESPAN;
                        }
                        System.Threading.Thread.Sleep(50);
                    } while (!responseString.Contains("�") && DateTime.Now < timeoutTime);
                    responseMessage = JsonConvert.DeserializeObject<DCSMessage>(responseString.Split('�').First());
                } // close the socket
                lastConnectionAttempt = DateTime.MinValue;
            }
            catch (JsonException ex)
            {
                // Clipboard.SetText(responseString);
                // MessageBox.Show(ex.Message + "\n\nJson data copied to clipboard for analysis.", "Exception", MessageBoxButtons.OK);
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK);
                return null;
            }
            catch (Exception)
            {
                lastConnectionAttempt = DateTime.Now;
                return null;
            }
            return responseMessage;
        }
    }
}
