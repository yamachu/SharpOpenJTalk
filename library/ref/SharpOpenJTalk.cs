using System;
using System.IO;
using System.Collections.Generic;

namespace SharpOpenJTalk
{
    public class OpenJTalkAPI: IDisposable
    {
        public List<List<string>> Labels { get; private set; }
        public List<float> WavBuffer { get; private set; }

        ~OpenJTalkAPI() {}

        public bool Initialize(string dictPath, string hmmModelPath)
        {
          throw null;
        }

        public bool Synthesis(string text, bool dumpAll = true)
        {
            throw null;
        }

        public void Clear() { }

        public void Dispose() { }
    }
}