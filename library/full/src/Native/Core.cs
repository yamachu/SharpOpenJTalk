using System;
using System.Collections.Generic;
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
            string dictPath, string hmmModelPath, string userDictPath)
            => CoreDefinitions.Open_JTalk_load_u16(instance, dictPath, hmmModelPath, userDictPath) == 1;

        public static void OpenJTalkDestroyBuffer(IntPtr instance, ref IntPtr buffer)
            => CoreDefinitions.Open_JTalk_destroy_buffer(instance, ref buffer);

        public static int OpenJTalkSynthesisBuffer(IntPtr instance, string text, out IntPtr buffer)
            => CoreDefinitions.Open_JTalk_synthesis_buffer_u16(instance, text, out buffer);

        public static bool OpenJTalkSynthesis(IntPtr instance,
            string text, string outputAudioPath, string outputLabelPath)
            => CoreDefinitions.Open_JTalk_synthesis_u16(instance,
                text, outputAudioPath, outputLabelPath) == 1;

        public static bool OpenJTalkSynthesisLabels(IntPtr instance,
            string text, string outputAudioPath,
            string outputTextAnalysisPath,
            string outputContextLabelPath)
            => CoreDefinitions.Open_JTalk_synthesis_labels_u16(instance,
                text, outputAudioPath, outputTextAnalysisPath, outputContextLabelPath) == 1;

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

        public static int OpenJTalkReSynthesisBuffer(IntPtr instance,
            out IntPtr buffer)
            => CoreDefinitions.Open_JTalk_resynthesis_buffer(instance, out buffer);

        public static bool OpenJTalkReSynthesis(IntPtr instance,
            string outputAudioPath)
            => CoreDefinitions.Open_JTalk_resynthesis_u16(instance, outputAudioPath) == 1;

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

        public static void OpenJTalkSetLF0(IntPtr instance,
            double f, IntPtr i)
            => CoreDefinitions.Open_JTalk_set_lf0(instance, f, i);

        public static void OpenJTalkSetLF0Array(IntPtr instance,
            double[] lf0, int buffer_length)
            => CoreDefinitions.Open_JTalk_set_lf0_array(instance, lf0, buffer_length);

        public static int OpenJTalkGetLF0Length(IntPtr instance)
            => CoreDefinitions.Open_JTalk_get_lf0_length(instance);

        public static void OpenJTalkGetLF0Array(IntPtr instance,
            IntPtr lf0_buffer, IntPtr buffer_length)
            => CoreDefinitions.Open_JTalk_get_lf0_array(instance, lf0_buffer, buffer_length);
    }
}