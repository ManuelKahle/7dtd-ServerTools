﻿using System;
using System.Collections.Generic;
using System.Xml;

namespace ServerTools
{
    class AutoBackupConsole : ConsoleCmdAbstract
    {
        public override string GetDescription()
        {
            return "[ServerTools]- Enable or Disable Auto Backup.";
        }

        public override string GetHelp()
        {
            return "Usage:\n" +
                   "  1. AutoBackup off\n" +
                   "  2. AutoBackup on\n" +
                   "  3. AutoBackup\n" +
                   "1. Turn off world auto backup\n" +
                   "2. Turn on world auto backup\n" +
                   "3. Start a backup manually\n";
        }

        public override string[] GetCommands()
        {
            return new string[] { "st-AutoBackup", "autobackup", "ab" };
        }

        public override void Execute(List<string> _params, CommandSenderInfo _senderInfo)
        {
            try
            {
                if (_params.Count > 1)
                {
                    SdtdConsole.Instance.Output(string.Format("Wrong number of arguments, expected 0 or 1, found {0}", _params.Count));
                    return;
                }
                if (_params.Count == 0)
                {
                    SdtdConsole.Instance.Output(string.Format("World backup has been initiated"));
                    AutoBackup.BackupExec();
                    Timers._tBS = 0;
                    SdtdConsole.Instance.Output(string.Format("World backup completed"));
                    return;
                }
                else if (_params[0].ToLower().Equals("off"))
                {
                    AutoBackup.IsEnabled = false;
                    LoadConfig.WriteXml();
                    SdtdConsole.Instance.Output(string.Format("Auto backup has been set to off"));
                    return;
                }
                else if (_params[0].ToLower().Equals("on"))
                {
                    AutoBackup.IsEnabled = true;
                    LoadConfig.WriteXml();
                    SdtdConsole.Instance.Output(string.Format("Auto backup has been set to on"));
                    return;
                }
                else
                {
                    SdtdConsole.Instance.Output(string.Format("Invalid argument {0}.", _params[0]));
                }
            }
            catch (Exception e)
            {
                Log.Out(string.Format("[SERVERTOOLS] Error in AutoBackup.Run: {0}.", e));
            }
        }
    }
}
