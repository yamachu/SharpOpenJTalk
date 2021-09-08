using System;
using System.Runtime.InteropServices;

namespace SharpOpenJTalk.Native
{
    internal class CoreDefinitions
    {
        private const string DllName = "openjtalk";

#region Instance
        [DllImport(DllName,CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Open_JTalk_initialize();

        [DllImport(DllName,CallingConvention = CallingConvention.Cdecl)]
        public static extern void Open_JTalk_clear(ref IntPtr openJTalkInstance);

        [DllImport(DllName,CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern int Open_JTalk_load_u16(IntPtr openJTalkInstance,
                string dictPath, string hmmModelPath, string userDictPath);

        [DllImport(DllName,CallingConvention = CallingConvention.Cdecl)]
        public static extern int Open_JTalk_destroy_buffer(IntPtr openJTalkInstance,
                ref IntPtr buffer);

        [DllImport(DllName,CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern int Open_JTalk_synthesis_buffer_u16(IntPtr openJTalkInstance,
                string text, out IntPtr buffer);

        [DllImport(DllName,CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern int Open_JTalk_synthesis_u16(IntPtr openJTalkInstance,
                string text, string outputAudioPath, string outputLabelPath);

        [DllImport(DllName,CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern int Open_JTalk_synthesis_labels_u16(IntPtr openJTalkInstance,
                string text, string outputAudioPath,
                string outputTextAnalysisPath,
                string outputContextLabelPath);

        [DllImport(DllName,CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern int Open_JTalk_extract_label_u16(IntPtr openJTalkInstance,
                string text, out IntPtr unmanagedLabels, out int labelLength);

        [DllImport(DllName,CallingConvention = CallingConvention.Cdecl)]
        public static extern int Open_JTalk_resynthesis_buffer(IntPtr openJTalkInstance, out IntPtr buffer);

        [DllImport(DllName,CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern int Open_JTalk_resynthesis_u16(IntPtr openJTalkInstance, string outputAudioPath);
#endregion

#region Parameters
        [DllImport(DllName,CallingConvention = CallingConvention.Cdecl)]
        public static extern void Open_JTalk_set_sampling_frequency(IntPtr openJTalkInstance, IntPtr i);

        [DllImport(DllName,CallingConvention = CallingConvention.Cdecl)]
        public static extern void Open_JTalk_set_fperiod(IntPtr openJTalkInstance, IntPtr i);

        [DllImport(DllName,CallingConvention = CallingConvention.Cdecl)]
        public static extern void Open_JTalk_set_alpha(IntPtr openJTalkInstance, double f);

        [DllImport(DllName,CallingConvention = CallingConvention.Cdecl)]
        public static extern void Open_JTalk_set_beta(IntPtr openJTalkInstance, double f);

        [DllImport(DllName,CallingConvention = CallingConvention.Cdecl)]
        public static extern void Open_JTalk_set_speed(IntPtr openJTalkInstance, double f);

        [DllImport(DllName,CallingConvention = CallingConvention.Cdecl)]
        public static extern void Open_JTalk_add_half_tone(IntPtr openJTalkInstance, double f);

        [DllImport(DllName,CallingConvention = CallingConvention.Cdecl)]
        public static extern void Open_JTalk_set_msd_threshold(IntPtr openJTalkInstance, IntPtr i, double f);

        [DllImport(DllName,CallingConvention = CallingConvention.Cdecl)]
        public static extern void Open_JTalk_set_gv_weight(IntPtr openJTalkInstance, IntPtr i, double f);

        [DllImport(DllName,CallingConvention = CallingConvention.Cdecl)]
        public static extern void Open_JTalk_set_volume(IntPtr openJTalkInstance, double f);

        [DllImport(DllName,CallingConvention = CallingConvention.Cdecl)]
        public static extern void Open_JTalk_set_lf0(IntPtr openJTalkInstance, double lf0, IntPtr frame_index);

        [DllImport(DllName,CallingConvention = CallingConvention.Cdecl)]
        public static extern void Open_JTalk_set_lf0_array(IntPtr openJTalkInstance, [In][MarshalAs(UnmanagedType.LPArray, SizeParamIndex=2)] double[] lf0, int buffer_length);

        
        [DllImport(DllName,CallingConvention = CallingConvention.Cdecl)]
        public static extern int Open_JTalk_get_lf0_length(IntPtr openJTalkInstance);
        
        [DllImport(DllName,CallingConvention = CallingConvention.Cdecl)]
        public static extern void Open_JTalk_get_lf0_array(IntPtr openJTalkInstance, [In][Out] IntPtr lf0_buffer, IntPtr buffer_length);
#endregion
    }
}
