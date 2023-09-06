using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Text;

namespace CoordinateConverter
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

        /// <summary>
        /// Sends the request.
        /// </summary>
        /// <param name="requests">The requests.</param>
        /// <returns>The response from the remote host</returns>
        public static DCSMessage sendRequest(DCSMessage requests)
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
            string json = JsonConvert.SerializeObject(requests, jsonSerializerSettings) + '\n';
            byte[] data = Encoding.UTF8.GetBytes(json);

            const int BUFFER_SIZE = 4096;          // maximum data chunk size of received data
            TimeSpan TIMEOUT_TIMESPAN = TimeSpan.FromMilliseconds(300);
            byte[] buffer = new byte[BUFFER_SIZE]; // buffer to return the bytes back into
            string returnMessage = String.Empty;   // string returned by the server

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
                    while (sock.Available == 0 && DateTime.Now < timeoutTime)
                    {
                        System.Threading.Thread.Sleep(50);
                    }

                    if (sock.Available == 0)
                    {
                        throw new Exception("No data received");
                    }
                    
                    // read the answer
                    while (sock.Available > 0)
                    {
                        int byteCount = Math.Min(sock.Available, BUFFER_SIZE);
                        sock.Receive(buffer, byteCount, SocketFlags.None);
                        returnMessage += Encoding.UTF8.GetString(buffer);
                    }
                } // close the socket
                lastConnectionAttempt = DateTime.MinValue;
            }
            catch (Exception)
            {
                lastConnectionAttempt = DateTime.Now;
                return null;
            }

            return JsonConvert.DeserializeObject<DCSMessage> (returnMessage);
        }
    }
}
