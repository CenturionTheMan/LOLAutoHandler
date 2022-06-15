using LOL_AutoHandler_v2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOL_AutoHandler_v2.Utilty
{
    public enum WindowSize
    {
        OTHER = 0,
        NONE = 1,
        MINIMIZED = 2,
        SMALL = 3,
        MEDIUM = 4,
        LARGE =5
    }

    public enum ClientState
    {
        NONE,
        QUEUE,
        LOOKING_FOR_ROLE,
        BANPHASE,
        PICKPHASE,
        GAME_OR_DODGE
    }


    public static class Helpers
    {
        public const int SmallWindowSizeX = 1024;
        public const int SmallWindowSizeY = 576;

        public const int MediumWindowSizeX = 1280;
        public const int MediumindowSizeY = 720;

        public const int LargeWindowSizeX = 1600;
        public const int LargeWindowSizeY = 900;



        public static string GetStringWithin(string strSource, string strStart, string strEnd)
        {
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                int Start, End;
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }

            return "";
        }

        public static string GetProjectDirectory()
        {
            return Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\LOL_AutoHandler_v2\";
        }

        public static string GetProjectDirectory(string relativePath)
        {
            return Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\LOL_AutoHandler_v2\" + relativePath;
        }

        public static Champion[] ReadChampionsFormJson(string file)
        {
            Champion[] items = null;
            using (StreamReader r = new StreamReader(file))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<Champion[]>(json);
            }
            return items;
        }

        public static Champion GetChampionByName(string name, Champion[] range)
        {
            for (int i = 0; i < range.Length; i++)
            {
                if (name == range[i].Name)
                {
                    return range[i];
                }
            }

            return null;
        }
    }
}
