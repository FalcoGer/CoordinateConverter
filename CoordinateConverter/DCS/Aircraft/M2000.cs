using System.Collections.Generic;

namespace CoordinateConverter.DCS.Aircraft
{
    public class M2000 : DCSAircraft
    {
        public override List<DCSCommand> GetPointActions(CoordinateDataEntry coordinate)
        {
            throw new System.NotImplementedException();
        }

        public override List<string> GetPointOptionsForType(string pointTypeStr)
        {
            throw new System.NotImplementedException();
        }

        public override List<string> GetPointTypes()
        {
            throw new System.NotImplementedException();
        }

        public override List<DCSCommand> GetPostPointActions()
        {
            throw new System.NotImplementedException();
        }

        public override List<DCSCommand> GetPrePointActions()
        {
            throw new System.NotImplementedException();
        }
    }
}
