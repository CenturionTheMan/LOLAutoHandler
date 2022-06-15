using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LOL_AutoHandler_v2.Models;
using Newtonsoft.Json;

namespace LOL_AutoHandler_v2.Utilty
{
    public static class JasonHandler
    {
        public static void SaveChampionstToJson(Champion[,] champions, string outDiretory)
        {
            string conv = JsonConvert.SerializeObject(champions);
            File.WriteAllText(outDiretory, conv);
        }

        public static Champion[,] ReadChampionsFromFile(string path)
        {
            if (File.Exists(path))
            {
                string readData = File.ReadAllText(path);
                Champion[,] champions = JsonConvert.DeserializeObject<Champion[,]>(readData);
                return champions;
            }
            else
            {
                return null;
            }
        }
    }
}
