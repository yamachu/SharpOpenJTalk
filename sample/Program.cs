using System;
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
        }
    }
}
