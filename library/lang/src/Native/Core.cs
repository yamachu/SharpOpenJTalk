using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SharpOpenJTalk.Lang.Native
{
    internal class Core
    {
        public static IntPtr OpenJTalkInitialize()
            => CoreDefinitions.Open_JTalk_initialize();

        public static void OpenJTalkClear(IntPtr instance)
            => CoreDefinitions.Open_JTalk_clear(ref instance);

        public static bool OpenJTalkLoad(IntPtr instance, string dictPath, string userDictPath)
            => CoreDefinitions.Open_JTalk_load_u16(instance, dictPath, userDictPath) == 1;

        public static IEnumerable<string> OpenJTalkExtractLabels(IntPtr instance, string text)
        {
            CoreDefinitions.Open_JTalk_extract_label_u16(instance, text, out var unmanagedLabels, out var labelsLength);

            IntPtr[] tmpIntPtrArr = new IntPtr[labelsLength];
            var labels = new List<string>();

            Marshal.Copy(unmanagedLabels, tmpIntPtrArr, 0, labelsLength);
            Marshal.FreeCoTaskMem(unmanagedLabels);

            for (int i = 0; i < labelsLength; i++)
            {
                labels.Add(Marshal.PtrToStringAnsi(tmpIntPtrArr[i]));
                Marshal.FreeCoTaskMem(tmpIntPtrArr[i]);
            }

            return labels;
        }
    }
}