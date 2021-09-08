using System;
using System.Runtime.InteropServices;

namespace SharpOpenJTalk.Lang.Native
{
    internal class CoreDefinitions
    {
        private const string DllName = "openjtalk-lang";

#region Instance
        [DllImport(DllName,CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Open_JTalk_initialize();

        [DllImport(DllName,CallingConvention = CallingConvention.Cdecl)]
        public static extern void Open_JTalk_clear(ref IntPtr openJTalkInstance);

        [DllImport(DllName,CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern int Open_JTalk_load_u16(IntPtr openJTalkInstance, string dictPath, string userDictPath);

        [DllImport(DllName,CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern int Open_JTalk_extract_label_u16(IntPtr openJTalkInstance,
                string text, out IntPtr unmanagedLabels, out int labelLength);
#endregion
    }
}
