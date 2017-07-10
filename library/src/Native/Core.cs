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

        public static bool OpenJTalkSynthesisLabels(IntPtr instance,
            string text, string outputAudioPath,
            string outputTextAnalysisPath,
            string outputContextLabelPath)
            => CoreDefinitions.Open_JTalk_synthesis_labels(instance,
                text, outputAudioPath, outputTextAnalysisPath, outputContextLabelPath) == 1;
    }
}