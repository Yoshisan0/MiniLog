using System;
using System.IO;
using System.Threading;
using UnityEngine;

namespace Asterios.MiniLog
{
    public class MiniLog
    {
        private static string m_Path = "";
        private static object m_Sync = null;

        public static void Init()
        {
            m_Sync = new object();

            string time = DateTime.Now.ToString("yyyyMMddHHmmss");
            m_Path = Path.Combine(Application.persistentDataPath, time + ".log");
        }

        private static void AppendLine(string _level, string _log)
        {
            string time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");
            string line = time + " " + _level + " " + _log;

            Monitor.Enter(m_Sync);
            try
            {
                File.AppendAllText(m_Path, line + "\n");
            }
            finally
            {
                Monitor.Exit(m_Sync);
            }
        }

        public static void Log(string _log, bool _out = true)
        {
            AppendLine("INFO", _log);

            if (_out)
            {
                Debug.Log(_log);
            }
        }

        public static void LogWarning(string _log, bool _out = true)
        {
            AppendLine("WARN", _log);

            if (_out)
            {
                Debug.LogWarning(_log);
            }
        }

        public static void LogError(string _log, bool _out = true)
        {
            AppendLine("ERROR", _log);

            if (_out)
            {
                Debug.LogError(_log);
            }
        }
    }
}
