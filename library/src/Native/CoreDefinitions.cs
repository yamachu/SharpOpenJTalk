using System;
using System.Runtime.InteropServices;

namespace SharpOpenJTalk.Native
{
    internal class CoreDefinitions
    {
#if __OSX
        private const string DllName = "libopenjtalk.dylib";
#elif __Linux
        private const string DllName = "libopenjtalk.so";
#elif __Win
        private const string DllName = "openjtalk.dll";
#endif

#region Instance
        [DllImport(DllName,CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Open_JTalk_initialize();

        [DllImport(DllName,CallingConvention = CallingConvention.Cdecl)]
        public static extern void Open_JTalk_clear(ref IntPtr openJTalkInstance);

        [DllImport(DllName,CallingConvention = CallingConvention.Cdecl)]
        public static extern int Open_JTalk_load(IntPtr openJTalkInstance,
                string dictPath, string hmmModelPath);

        [DllImport(DllName,CallingConvention = CallingConvention.Cdecl)]
        public static extern int Open_JTalk_synthesis(IntPtr openJTalkInstance,
                string text, string outputAudioPath, string outputLabelPath);

        [DllImport(DllName,CallingConvention = CallingConvention.Cdecl)]
        public static extern int Open_JTalk_synthesis_labels(IntPtr openJTalkInstance,
                string text, string outputAudioPath,
                string outputTextAnalysisPath,
                string outputContextLabelPath);
        #endregion
    }
}
