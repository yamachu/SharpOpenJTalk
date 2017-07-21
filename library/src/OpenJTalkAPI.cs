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
        private uint samplingFrequency;
        private uint fperiod;
        private double alpha;
        private double beta;
        private double speed;
        private double halfTone;
        private double msdThreshold;
        private double gvWeightSpectrum;
        private double gvWeightLF0;
        private double volume;

        public List<List<string>> Labels { get; private set; } = new List<List<string>>();
        public List<byte> WavBuffer { get; private set; } = new List<byte>();

        public uint SamplingFrequency {
            get { return samplingFrequency; }
            set {
                samplingFrequency = value;

                if (Instance == IntPtr.Zero)
                {
                    throw new Exception("Instance is not initialized");
                }

                if (value >= 1)
                {
                    Core.OpenJTalkSetSamplingFrequency(Instance, new IntPtr(samplingFrequency));
                }
            }
        }

        public uint FramePeriod {
            get { return fperiod; }
            set {
                fperiod = value;

                if (Instance == IntPtr.Zero)
                {
                    throw new Exception("Instance is not initialized");
                }

                if (value >= 1)
                {
                    Core.OpenJTalkSetFramePeriod(Instance, new IntPtr(fperiod));
                }
            }
        }

        public double Alpha {
            get { return alpha; }
            set {
                alpha = value;

                if (Instance == IntPtr.Zero)
                {
                    throw new Exception("Instance is not initialized");
                }

                if (value >= 0.0 && value <= 1.0)
                {
                    Core.OpenJTalkSetAlpha(Instance, alpha);
                }
            }
        }

        public double Beta {
            get { return beta; }
            set {
                beta = value;

                if (Instance == IntPtr.Zero)
                {
                    throw new Exception("Instance is not initialized");
                }

                if (value >= 0.0 && value <= 1.0)
                {
                    Core.OpenJTalkSetBeta(Instance, beta);
                }
            }
        }

        public double Speed {
            get { return speed; }
            set {
                speed = value;

                if (Instance == IntPtr.Zero)
                {
                    throw new Exception("Instance is not initialized");
                }

                if (value >= 0.0)
                {
                    Core.OpenJTalkSetSpeed(Instance, speed);
                }
            }
        }

        public double HarfTone {
            get { return halfTone; }
            set {
                halfTone = value;

                if (Instance == IntPtr.Zero)
                {
                    throw new Exception("Instance is not initialized");
                }

                Core.OpenJTalkSetHalfTone(Instance, halfTone);
            }
        }
        public double MSDThreshold {
            get { return msdThreshold; }
            set {
                msdThreshold = value;

                if (Instance == IntPtr.Zero)
                {
                    throw new Exception("Instance is not initialized");
                }

                if (value >= 0.0 && value <= 1.0)
                {
                    Core.OpenJTalkSetMSDThreshold(Instance, new IntPtr(1), msdThreshold);
                }
            }
        }
        public double GVWeightSpectrum {
            get { return gvWeightSpectrum; }
            set {
                gvWeightSpectrum = value;

                if (Instance == IntPtr.Zero)
                {
                    throw new Exception("Instance is not initialized");
                }

                if (value >= 0.0)
                {
                    Core.OpenJTalkSetGVWeight(Instance, new IntPtr(0), gvWeightSpectrum);
                }
            }
        }
        public double GVWeightLF0 {
            get { return gvWeightLF0; }
            set {
                gvWeightLF0 = value;

                if (Instance == IntPtr.Zero)
                {
                    throw new Exception("Instance is not initialized");
                }

                if (value >= 0.0)
                {
                    Core.OpenJTalkSetGVWeight(Instance, new IntPtr(1), gvWeightLF0);
                }
            }
        }
        public double Volume {
            get { return volume; }
            set {
                volume = value;

                if (Instance == IntPtr.Zero)
                {
                    throw new Exception("Instance is not initialized");
                }

                Core.OpenJTalkSetVolume(Instance, volume);
            }
        }

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
                this.ReadWav(wavPath);
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

        private void ReadWav(string wavFilePath)
        {
            using (var reader = new BinaryReader(new FileStream(wavFilePath, FileMode.Open)))
            {
                this.WavBuffer.AddRange(reader.ReadBytes((int)reader.BaseStream.Length));
            }
        }
    }
}