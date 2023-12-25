using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIRIUS.INC_Gacha.Framework_Sirius.INC
{
    internal class LoadImgCase
    {

        private Dictionary<string, string> imgCases = new Dictionary<string, string>();

        public void Load()
        {
            foreach (string spt in File.ReadLines(@"DataSirius\CASE_IMG.txt"))
            {
                string[] splt = spt.Split(new string[] { ">: " }, StringSplitOptions.None);
                imgCases.Add(splt[0], splt[1]);
            }
        }

        public Dictionary<string, string> getImgCases()
        {
            return imgCases;
        }


    }
}
