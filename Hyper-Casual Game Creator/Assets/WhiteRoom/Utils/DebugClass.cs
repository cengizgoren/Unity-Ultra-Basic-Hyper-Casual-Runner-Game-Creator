using UnityEngine;

namespace WhiteRoom.Utils.Debug
{
    public class DebugClass : MonoBehaviour
    {
        /// <summary>
        ///   <para>Logs message to the Unity Console using default logger.</para>
        /// </summary>
        /// <param name="message"></param>
        public static void Log(string message)
        {
            Debug(LogType.Log, message);
        }
        
        /// <summary>
        ///   <para>Logs message to the Unity Console using default logger.</para>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="tag"></param>
        public static void Log(string tag, string message)
        {
            Debug(LogType.Log, tag, message);
        }

        /// <summary>
        ///   <para>Log that logs an warning message.</para>
        /// </summary>
        /// <param name="message"></param>
        public static void Warning(string message)
        {
            Debug(LogType.Warning, message);
        }
        
        /// <summary>
        ///   <para>Log that logs an warning message.</para>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="tag"></param>
        public static void Warning(string tag, string message)
        {
            Debug(LogType.Warning, tag, message);
        }

        /// <summary>
        ///   <para>Log that logs an error message.</para>
        /// </summary>
        /// <param name="message"></param>
        public static void Error(string message)
        {
            Debug(LogType.Error, message);
        }
        
        /// <summary>
        ///   <para>Log that logs an error message.</para>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="tag"></param>
        public static void Error(string tag, string message)
        {
            Debug(LogType.Error, tag, message);
        }
        
        /// <summary>
        ///   <para>Main Debug Function</para>
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="tag"></param>
        /// <param name="message"></param>
        private static void Debug(LogType logType,string tag,string message)
        {
            UnityEngine.Debug.unityLogger.Log(logType, tag, message);
        }
        
        /// <summary>
        ///   <para>Main Debug Function</para>
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="message"></param>
        private static void Debug(LogType logType,string message)
        {
            UnityEngine.Debug.unityLogger.Log(logType, message);
        }
    }
}
