using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using Newtonsoft.Json;

namespace CoordinateConverter
{
    public abstract class DCSAircraft
    {
        public readonly System.Net.IPEndPoint TCP_ENDPOINT = new System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 42070);
        public bool SendToDCS(List<CoordinateDataEntry> coordinateList)
        {
            List<DCSCommand> commands = GenerateCommands(coordinateList);
            if (commands == null)
            {
                return false;
            }
            string json = JsonConvert.SerializeObject(commands) + '\n';
            byte[] data = System.Text.Encoding.UTF8.GetBytes(json);

            try
            {
                // open TCP socket to DCS TheWay
                using (Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    sock.Connect(TCP_ENDPOINT);
                    sock.Send(data);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        private List<DCSCommand> GenerateCommands(List<CoordinateDataEntry> coordinateList)
        {
            coordinateList = coordinateList.Where(x => x.XFer).ToList(); // filter out the ones that are not set for xfer
            if (coordinateList.Count == 0)
            {
                return null;
            }

            List<DCSCommand> commands = GetPrePointActions();
            foreach (CoordinateDataEntry entry in coordinateList)
            {
                commands.AddRange(GetPointActions(entry).Where(x => x != null));
            }
            commands.AddRange(GetPostPointActions());
            return commands;
        }

        public abstract List<string> GetPointTypes();
        public abstract List<string> GetPointOptionsForType(string pointTypeStr);

        public abstract List<DCSCommand> GetPrePointActions();
        public abstract List<DCSCommand> GetPointActions(CoordinateDataEntry coordinate);
        public abstract List<DCSCommand> GetPostPointActions();
    }
}
