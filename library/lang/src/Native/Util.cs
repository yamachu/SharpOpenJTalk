using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SharpOpenJTalk.Lang.Native
{
    internal class Util
    {
        public static bool OpenJTalkDictGen(string dictPath, string userCsvPath, string outputUserDictPath)
            => CoreDefinitions.Open_JTalk_dict_gen_u16(dictPath, userCsvPath, outputUserDictPath) == 1;
    }
}