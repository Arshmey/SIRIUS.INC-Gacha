using SIRIUS.INC_Gacha.Framework_Sirius.INC;
using SIRIUS.INC_Gacha.OtherWindow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace SIRIUS.INC_Gacha
{
    public partial class Main : Form
    {
        
        LoadImgCase loadImgCase = new LoadImgCase();
        List<PictureBox> imgCases;
        List<Label> labelNames;

        public Main()
        {
            loadImgCase.Load();
            InitializeComponent();
            imgCases = new List<PictureBox>() { imgCase_1, imgCase_2, imgCase_3, imgCase_4, imgCase_5, imgCase_6 };
            labelNames = new List<Label>() { nameCase_1, nameCase_2, nameCase_3, nameCase_4, nameCase_5, nameCase_6 };

            getImgElements();
        }

        private void getImgElements()
        {
            int index = 0;
            foreach (string key in loadImgCase.getImgCases().Keys) 
            {
                imgCases[index].BackgroundImage = Image.FromFile(@loadImgCase.getImgCases()[key]);
                imgCases[index].Tag = loadImgCase.getImgCases()[key].Replace("IMG_CASE.png", "");
                imgCases[index].Click += new EventHandler(imgCase_Click);
                index++;
            }
        }

        private void imgCase_Click(object sender, EventArgs e)
        {
            foreach (PictureBox img in imgCases)
            {
                Controls.Remove(img);
            }

            foreach (Label lable in labelNames)
            {
                Controls.Remove(lable);
            }

            new CaseWindow(this, ((PictureBox)sender).Tag.ToString());
        }
    }
}
