using System;

namespace Lib_Equipment
{
    public static class AiSessionMemory
    {
        public static string ChatContext = "";
        public static string RtfChatHistory = "";

        public static void ClearMemory()
        {
            ChatContext = "";
            RtfChatHistory = "";
        }
    }
}