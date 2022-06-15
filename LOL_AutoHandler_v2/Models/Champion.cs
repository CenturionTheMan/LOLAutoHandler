using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOL_AutoHandler_v2.Models
{
    public class Champion
    {
        public string Name { get; set; }
        public string UIPicture { get; set; }
        public string BanPhaseSmallPicture { get; set; }
        public string BanPhasePicture { get; set; }
        public string BanPhaseLargePicture { get; set; }
        public string PickPhaseSmallPicture { get; set; }
        public string PickPhasePicture { get; set; }
        public string PickPhaseLargePicture { get; set; }
    }
}
