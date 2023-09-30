using System;
using System.Collections.Generic;

namespace CoordinateConverter.DCS.Aircraft
{
    public class AV8B : DCSAircraft
    {
        public override List<DCSCommand> GetPointActions(CoordinateDataEntry coordinate)
        {
            throw new NotImplementedException();
        }

        public override List<string> GetPointOptionsForType(string pointTypeStr)
        {
            throw new NotImplementedException();
        }

        public override List<string> GetPointTypes()
        {
            throw new NotImplementedException();
        }

        public override List<DCSCommand> GetPostPointActions()
        {
            throw new NotImplementedException();
        }

        public override List<DCSCommand> GetPrePointActions()
        {
            throw new NotImplementedException();
        }
    }
}
