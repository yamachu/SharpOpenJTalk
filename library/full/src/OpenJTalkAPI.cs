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

        public bool Synthesis(string text, bool dumpAll = true, bool useWorld = false)
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
                try {
                    synthesisSuccess = Core.OpenJTalkSynthesis(Instance, text, wavPath, labelPath);
                } catch (Exception ex) {
                    Console.Error.WriteLine(ex.StackTrace);
                    synthesisSuccess = false;
                }

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
                try {
                    synthesisSuccess = Core.OpenJTalkSynthesisLabels(Instance, text, wavPath, textAnalysisPath, contextLabelPath);
                } catch (Exception ex) {
                    Console.Error.WriteLine(ex.StackTrace);
                    synthesisSuccess = false;
                }

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

        public short[] SynthesisBuffer(string text, bool useWorld = false)
        {
            if (Instance == IntPtr.Zero)
            {
                return null;
            }

            int buffer_size;
            short[] buffer;

            try {
                IntPtr buffer_ptr;
                buffer_size = Core.OpenJTalkSynthesisBuffer(Instance, text, out buffer_ptr);

                if (buffer_size == 0) return null;

                buffer = new short[buffer_size];
                Marshal.Copy(buffer_ptr, buffer, 0, buffer_size);

                Core.OpenJTalkDestroyBuffer(Instance, ref buffer_ptr);
            } catch (Exception ex) {
                Console.Error.WriteLine(ex.StackTrace);
                return null;
            }

            return buffer;
        }

        public bool ReSynthesis(bool useWorld = false)
        {
            this.WavBuffer.Clear();

            if (Instance == IntPtr.Zero)
            {
                return false;
            }

            var synthesisSuccess = false;

            var wavPath = Path.GetTempFileName();

            try {
                synthesisSuccess = Core.OpenJTalkReSynthesis(Instance, wavPath);
            } catch (Exception ex) {
                Console.Error.WriteLine(ex.StackTrace);
                synthesisSuccess = false;
            }

            if (synthesisSuccess)
            {
                this.ReadWav(wavPath);
            }

            File.Delete(wavPath);

            return synthesisSuccess;
        }

        public short[] ReSynthesisBuffer(bool useWorld = false)
        {
            if (Instance == IntPtr.Zero)
            {
                return null;
            }

            int buffer_size;
            short[] buffer;

            try {
                IntPtr buffer_ptr;
                buffer_size = Core.OpenJTalkReSynthesisBuffer(Instance, out buffer_ptr);

                if (buffer_size == 0) return null;

                buffer = new short[buffer_size];
                Marshal.Copy(buffer_ptr, buffer, 0, buffer_size);

                Core.OpenJTalkDestroyBuffer(Instance, ref buffer_ptr);
            } catch (Exception ex) {
                Console.Error.WriteLine(ex.StackTrace);
                return null;
            }

            return buffer;
        }

        public IEnumerable<string> GetLabels(string text)
        {
            if (Instance == IntPtr.Zero)
            {
                return null;
            }
            return Core.OpenJTalkExtractLabels(Instance, text);
        }

        public double[] GetLF0Array()
        {
            var lf0Length = Core.OpenJTalkGetLF0Length(Instance);
            var lf0 = new double[lf0Length];

            IntPtr ptr_lf0 = Marshal.AllocHGlobal(Marshal.SizeOf<double>() * lf0Length);
            Core.OpenJTalkGetLF0Array(Instance, ptr_lf0, new IntPtr(lf0Length));

            Marshal.Copy(ptr_lf0, lf0, 0, lf0Length);

            Marshal.FreeHGlobal(ptr_lf0);

            return lf0;
        }

        public void SetLF0Array(double [] lf0)
        {
            Core.OpenJTalkSetLF0Array(Instance, lf0, lf0.Length);
        }

        public void SetLF0(double lf0, int index)
        {
            Core.OpenJTalkSetLF0(Instance, lf0, new IntPtr(index));
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