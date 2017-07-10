using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using SharpOpenJTalk.Native;

namespace SharpOpenJTalk
{
    public class OpenJTalkAPI: IDisposable
    {
        private IntPtr Instance = IntPtr.Zero;

        public List<List<string>> Labels { get; private set; } = new List<List<string>>();
        public List<float> WavBuffer { get; private set; } = new List<float>();

        ~OpenJTalkAPI()
        {
            this.Dispose();
        }

        public bool Initialize(string dictPath, string hmmModelPath)
        {
            Instance = Core.OpenJTalkInitialize();
            var loadSuccess = Core.OpenJTalkLoad(Instance, dictPath, hmmModelPath);
            if (!loadSuccess)
            {
                Core.OpenJTalkClear(Instance);
                Instance = IntPtr.Zero;
            }

            return loadSuccess;
        }

        public bool Synthesis(string text, bool dumpAll = true)
        {
            this.WavBuffer.Clear();
            this.Labels.Clear();

            if (Instance == IntPtr.Zero)
            {
                return false;
            }

            var synthesisSuccess = false;

            var wavPath = Path.GetTempFileName();

            if (dumpAll)
            {
                var labelPath = Path.GetTempFileName();
                synthesisSuccess = Core.OpenJTalkSynthesis(Instance, text, wavPath, labelPath);
                if (synthesisSuccess)
                {
                    this.ReadLabel(labelPath);
                }

                File.Delete(labelPath);
            }
            else
            {
                var textAnalysisPath = Path.GetTempFileName();
                var contextLabelPath = Path.GetTempFileName();
                synthesisSuccess = Core.OpenJTalkSynthesisLabels(Instance, text, wavPath, textAnalysisPath, contextLabelPath);
                if (synthesisSuccess)
                {
                    this.ReadLabel(textAnalysisPath);
                    this.ReadLabel(contextLabelPath);
                }

                File.Delete(textAnalysisPath);
                File.Delete(contextLabelPath);
            }

            if (synthesisSuccess)
            {
                this.ReadWavRaw(wavPath);
            }

            File.Delete(wavPath);

            return synthesisSuccess;
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

        private void ReadLabel(string labelFilePath)
        {
            var tmp = new List<string>();
            using (var reader = new StreamReader(new FileStream(labelFilePath, FileMode.Open)))
            {
                while (reader.Peek() >= 0)
                {
                    tmp.Add(reader.ReadLine());
                }
            }
            this.Labels.Add(tmp);
        }

        private void ReadWavRaw(string wavFilePath)
        {
            using (var reader = new BinaryReader(new FileStream(wavFilePath, FileMode.Open)))
            {
                // Supports 44-bytes header only
                reader.ReadBytes(44);
                while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    // 16bit wav only
                    this.WavBuffer.Add((float)reader.ReadInt16() / 32768.0f);
                }
            }
        }
    }
}