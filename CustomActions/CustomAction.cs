using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WixToolset.Dtf.WindowsInstaller;

namespace CustomActions
{
    public class CustomActions
    {
        public const string DCS_EXPORT_LINE = "local CCLfs=require('lfs'); dofile(CCLfs.writedir()..'Scripts/CoordinateConverter.lua')";
        [CustomAction]
        public static ActionResult AddDCSExportsLuaEntry(Session session)
        {
            session.Log("Adding entry to DCS Export Lua");
            using (var msgRec = new Record(0))
            {
                msgRec[0] = $"Modifying DCS Export Lua";
                session.Message(InstallMessage.Info, msgRec);
            }

            var dcsPath = Path.Combine("%USERPROFILE%", "Saved Games", "DCS");
            if(session["DCSFOLDER"] != null)
                dcsPath = session["DCSFOLDER"];

            var file = Path.Combine(dcsPath, "Scripts", "Export.lua");

            if (!File.Exists(file))
            {
                using (var msgRec = new Record(0))
                {
                    msgRec[0] = $"Could not find {file}, it will be created instead";
                    session.Message(InstallMessage.Info, msgRec);
                    session.Message(InstallMessage.User, msgRec);
                }

                File.WriteAllText(file, "");
            }

            string existingLua = File.ReadAllText(file);

            var alreadyHasExport = existingLua.Split('\n')
                .Any(existing => existing.ToLowerInvariant().Trim() == DCS_EXPORT_LINE.ToLowerInvariant().Trim());

            if (alreadyHasExport)
            {
                using (var msgRec = new Record(0))
                {
                    msgRec[0] = $"{file} has already been modified with the required exports, skipping...";
                    session.Message(InstallMessage.Info, msgRec);
                }
                return ActionResult.Success;
            }

            using (var msgRec = new Record(0))
            {
                msgRec[0] = $"Adding {DCS_EXPORT_LINE} to {file}";
                session.Message(InstallMessage.Info, msgRec);
            }

            existingLua += Environment.NewLine;
            existingLua += DCS_EXPORT_LINE;

            File.WriteAllText(file, existingLua);

            return ActionResult.Success;
        }
        [CustomAction]
        public static ActionResult RemoveDCSExportsLuaEntry(Session session)
        {
            session.Log("Removing entry from DCS Export Lua");
            using (var msgRec = new Record(0))
            {
                msgRec[0] = $"Modifying DCS Export Lua";
                session.Message(InstallMessage.Info, msgRec);
            }

            var dcsPath = Path.Combine("%USERPROFILE%", "Saved Games", "DCS");
            if (session["DCSFOLDER"] != null)
                dcsPath = session["DCSFOLDER"];

            var file = Path.Combine(dcsPath, "Scripts", "Export.lua");

            if (!File.Exists(file))
            {
                using (var msgRec = new Record(0))
                {
                    msgRec[0] = $"Could not find {file}, this may mean there will be left overs after install";
                    session.Message(InstallMessage.Info, msgRec);
                }
                return ActionResult.Success;
            }

            string existingLua = File.ReadAllText(file);

            var alreadyHasExport = existingLua.Split('\n')
                .Any(existing => existing.ToLowerInvariant().Trim() == DCS_EXPORT_LINE.ToLowerInvariant().Trim());

            if (!alreadyHasExport)
            {
                using (var msgRec = new Record(0))
                {
                    msgRec[0] = $"{file} already is missing the required exports, skipping...";
                    session.Message(InstallMessage.Info, msgRec);
                }
                return ActionResult.Success;
            }

            using (var msgRec = new Record(0))
            {
                msgRec[0] = $"Removing {DCS_EXPORT_LINE} from {file}";
                session.Message(InstallMessage.Info, msgRec);
            }

            existingLua = existingLua.Replace(DCS_EXPORT_LINE, "").Trim();

            if (existingLua.Length > 0)
            {
                File.WriteAllText(file, existingLua);
                using (var msgRec = new Record(0))
                {
                    msgRec[0] = $"Wrote changes to {file}";
                    session.Message(InstallMessage.Info, msgRec);
                }
            }
            else
            {
                File.Delete(file);
                using (var msgRec = new Record(0))
                {
                    msgRec[0] = $"Deleted empty exports: {file}";
                    session.Message(InstallMessage.Info, msgRec);
                }
            }

            return ActionResult.Success;
        }
    }
}