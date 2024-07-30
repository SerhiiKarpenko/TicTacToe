using System;
using System.Reflection;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace Code_Base.Infrastructure.Services.LogService
{
  public class LogService : ILogService
  {
    // private static bool initialized = false;
    //
    // // [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    // // public static void InitializeExtendedLogHandler()
    // // {
    // //   if (initialized)
    // //   {
    // //     return;
    // //   }
    // //
    // //   Debug.unityLogger.logHandler = new ExtendedLogHandler(Debug.unityLogger.logHandler);
    // //   initialized = true;
    // // }
    //
    // private static string  GetTrace(int depth = 0)
    // {
    //   MethodBase methodInfo = new System.Diagnostics.StackTrace().GetFrame(depth).GetMethod();
    //   return methodInfo.ReflectedType.Name;
    // }
    //
    // private class ExtendedLogHandler : ILogHandler
    // {
    //   private readonly ILogHandler logHandler;
    //
    //   public ExtendedLogHandler(ILogHandler logHandler)
    //   {
    //     this.logHandler = logHandler;
    //   }
    //
    //   public void LogException(Exception exception, Object context)
    //   {
    //     logHandler.LogException(exception, context);
    //   }
    //
    //   public void LogFormat(LogType logType, Object context, string format, params object[] args)
    //   {
    //     string trace = GetTrace(4);
    //     string title = trace.Contains("<>") ? "@GenericEvent" : trace;
    //     string message = $"<b><color=#{Utilities.ColorUtilities.GenerateHex(trace)}>[{title}]</color> {format}</b>";
    //
    //     logHandler.LogFormat(logType, context, message, args);
    //   }
    // }
  }
}