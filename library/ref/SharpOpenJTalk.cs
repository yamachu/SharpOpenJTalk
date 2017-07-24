using System;
using System.IO;
using System.Collections.Generic;

namespace SharpOpenJTalk
{
    public class OpenJTalkAPI: IDisposable
    {
        public List<List<string>> Labels { get; private set; }
        public List<byte> WavBuffer { get; private set; }
        public uint SamplingFrequency { get; set; }
        public uint FramePeriod { get; set; }
        public double Alpha { get; set; }
        public double Beta { get; set; }
        public double Speed { get; set; }
        public double HarfTone { get; set; }
        public double MSDThreshold { get; set; }
        public double GVWeightSpectrum { get; set; }
        public double GVWeightLF0 { get; set; }
        public double Volume { get; set; }

        ~OpenJTalkAPI() {}

        public bool Initialize(string dictPath, string hmmModelPath)
        {
          throw null;
        }

        public bool Synthesis(string text, bool dumpAll = true, bool useWorld = false)
        {
            throw null;
        }

        public bool ReSynthesis(bool useWorld = false)
        {
            throw null;
        }

        public double[] GetLF0Array()
        {
            throw null;
        }

        public void SetLF0Array(double[] lf0) { }

        public void SetLF0(double lf0, int index) { }

        public void Clear() { }

        public void Dispose() { }
    }
}