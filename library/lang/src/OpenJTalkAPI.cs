using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using SharpOpenJTalk.Lang.Native;

namespace SharpOpenJTalk.Lang
{
    public class OpenJTalkAPI: IDisposable
    {
        private IntPtr Instance = IntPtr.Zero;

        ~OpenJTalkAPI()
        {
            this.Dispose();
        }

        public bool Initialize(string dictPath)
        {
            Instance = Core.OpenJTalkInitialize();
            var loadSuccess = Core.OpenJTalkLoad(Instance, dictPath);
            if (!loadSuccess)
            {
                Instance = IntPtr.Zero;
            }

            return loadSuccess;
        }

        public IEnumerable<string> GetLabels(string text)
        {
            if (Instance == IntPtr.Zero)
            {
                return null;
            }
            return Core.OpenJTalkExtractLabels(Instance, text);
        }

        public void Clear()
        {
            if (Instance != IntPtr.Zero)
            {
                Core.OpenJTalkClear(Instance);
                Instance = IntPtr.Zero;
            }
        }

        public void Dispose()
        {
            Clear();
        }
    }
}