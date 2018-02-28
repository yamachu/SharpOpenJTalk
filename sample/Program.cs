using System;
using System.IO;
using SharpOpenJTalk;

namespace sample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var instance = new OpenJTalkAPI();
            if (!instance.Initialize("/Users/yamachu/Downloads/open_jtalk_dic_utf_8-1.10", "/Users/yamachu/Downloads/hts_voice_nitech_jp_atr503_m001-1.05/nitech_jp_atr503_m001.htsvoice"))
            {
                System.Console.WriteLine("Initialize failed");
                return;
            }
            var buffer = instance.SynthesisBuffer("これはテストです");
            if (buffer == null)
            {
                System.Console.WriteLine("Synthesis failed");
                return;
            }
            System.Console.WriteLine(buffer.Length);

            SaveToFile(buffer, "test.wav");
        }

        static void SaveToFile(short[] buffer, string path)
        {
            var byteWidth = 16 / 8;
            var freq = 48000;
            
            using (var fs = new FileStream(path, FileMode.OpenOrCreate))
            using (var bw = new BinaryWriter(fs))
            {
                bw.Write("RIFF".ToCharArray());
                bw.Write((UInt32)(36 + buffer.Length * byteWidth));
                bw.Write("WAVE".ToCharArray());
                bw.Write("fmt ".ToCharArray());
                bw.Write((UInt32)16);
                bw.Write((UInt16)1);
                bw.Write((UInt16)1);
                bw.Write((UInt32)freq);
                bw.Write((UInt32)(freq * byteWidth));
                bw.Write((UInt16)(byteWidth));
                bw.Write((UInt16)16);
                bw.Write("data".ToCharArray());
                bw.Write((UInt32)(buffer.Length * byteWidth));

                var maxVal = (short)32767;
                var minVal = (short)-32768;

                foreach (var v in buffer)
                {
                    if (v > maxVal)
                        bw.Write(maxVal);
                    else if (v < minVal)
                        bw.Write(minVal);
                    else
                        bw.Write((short)v);
                }
            }
        }
    }
}
