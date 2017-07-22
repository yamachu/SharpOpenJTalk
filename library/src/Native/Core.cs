using System;
using System.Runtime.InteropServices;

namespace SharpOpenJTalk.Native
{
    internal class Core
    {
        public static IntPtr OpenJTalkInitialize()
            => CoreDefinitions.Open_JTalk_initialize();

        public static void OpenJTalkClear(IntPtr instance)
            => CoreDefinitions.Open_JTalk_clear(ref instance);

        public static bool OpenJTalkLoad(IntPtr instance,
            string dictPath, string hmmModelPath)
            => CoreDefinitions.Open_JTalk_load(instance, dictPath, hmmModelPath) == 1;

        public static bool OpenJTalkSynthesis(IntPtr instance,
            string text, string outputAudioPath, string outputLabelPath)
            => CoreDefinitions.Open_JTalk_synthesis(instance,
                text, outputAudioPath, outputLabelPath) == 1;

        public static bool OpenJTalkSynthesisWORLD(IntPtr instance,
            string text, string outputAudioPath, string outputLabelPath)
            => CoreDefinitions.Open_JTalk_synthesis_WORLD(instance,
                text, outputAudioPath, outputLabelPath) == 1;

        public static bool OpenJTalkSynthesisLabels(IntPtr instance,
            string text, string outputAudioPath,
            string outputTextAnalysisPath,
            string outputContextLabelPath)
            => CoreDefinitions.Open_JTalk_synthesis_labels(instance,
                text, outputAudioPath, outputTextAnalysisPath, outputContextLabelPath) == 1;

        public static bool OpenJTalkSynthesisLabelsWORLD(IntPtr instance,
            string text, string outputAudioPath,
            string outputTextAnalysisPath,
            string outputContextLabelPath)
            => CoreDefinitions.Open_JTalk_synthesis_labels_WORLD(instance,
                text, outputAudioPath, outputTextAnalysisPath, outputContextLabelPath) == 1;

        public static void OpenJTalkSetSamplingFrequency(IntPtr instance,
            IntPtr i)
            => CoreDefinitions.Open_JTalk_set_sampling_frequency(instance, i);

        public static void OpenJTalkSetFramePeriod(IntPtr instance,
            IntPtr i)
            => CoreDefinitions.Open_JTalk_set_fperiod(instance, i);

        public static void OpenJTalkSetAlpha(IntPtr instance,
            double f)
            => CoreDefinitions.Open_JTalk_set_alpha(instance, f);

        public static void OpenJTalkSetBeta(IntPtr instance,
            double f)
            => CoreDefinitions.Open_JTalk_set_beta(instance, f);

        public static void OpenJTalkSetSpeed(IntPtr instance,
            double f)
            => CoreDefinitions.Open_JTalk_set_speed(instance, f);

        public static void OpenJTalkSetHalfTone(IntPtr instance,
            double f)
            => CoreDefinitions.Open_JTalk_add_half_tone(instance, f);

        public static void OpenJTalkSetMSDThreshold(IntPtr instance,
            IntPtr i, double f)
            => CoreDefinitions.Open_JTalk_set_msd_threshold(instance, i, f);

        public static void OpenJTalkSetGVWeight(IntPtr instance,
            IntPtr i, double f)
            => CoreDefinitions.Open_JTalk_set_gv_weight(instance, i, f);

        public static void OpenJTalkSetVolume(IntPtr instance,
            double f)
            => CoreDefinitions.Open_JTalk_set_volume(instance, f);
    }
}