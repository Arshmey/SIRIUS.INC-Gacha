using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIRIUS.INC_Gacha
{
    internal class TitlePerson
    {
        private Dictionary<string, string> name_person;
        private Dictionary<string, int> haves = new Dictionary<string, int>();
        private List<Panel> panels = new List<Panel>();
        private List<PictureBox> Art = new List<PictureBox>();
        private List<Label> Names = new List<Label>();
        private List<PictureBox> Stars = new List<PictureBox>();
        private List<Label> Rates = new List<Label>();
        private int Y = 3;

        public TitlePerson(Dictionary<string, string> name_person)
        {
            this.name_person = name_person;
        }

        public void CreateTitles(Panel scroledPanel, Dictionary<string, string> person_change, Dictionary<string, string> star_rate, string path)
        {
            int i = 0;
            foreach (string key in person_change.Keys)
            {
                haves.Add(person_change[key], 0);
                panels.Add(new Panel());
                Art.Add(new PictureBox());
                Names.Add(new Label());
                Stars.Add(new PictureBox());
                Rates.Add(new Label());

                panels[i].SetBounds(3, Y, 1323, 164);
                scroledPanel.Controls.Add(panels[i]);

                Art[i].SetBounds(3, 3, 160, 160);
                Art[i].Image = Image.FromFile(@path + person_change[key] + ".png");
                panels[i].Controls.Add(Art[i]);

                Names[i].Name = person_change[key];
                Names[i].SetBounds(166, 3, 256, 32);
                Names[i].Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold);
                Names[i].ForeColor = SystemColors.MenuHighlight;
                Names[i].BackColor = Color.Transparent;
                Names[i].Text = "Имя: " + name_person[person_change[key]] + " x" + haves[person_change[key]];
                panels[i].Controls.Add(Names[i]);

                Stars[i].SetBounds(166, 35, 16, 15);
                Stars[i].Image = Image.FromFile(@"DataSirius/Other/Star.png");
                Stars[i].BackColor = Color.Transparent;
                panels[i].Controls.Add(Stars[i]);

                Rates[i].SetBounds(184, 34, 32, 32);
                Rates[i].ForeColor = SystemColors.MenuHighlight;
                Rates[i].Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
                Rates[i].Text = star_rate[person_change[key]];
                Rates[i].BackColor = Color.Transparent;
                panels[i].Controls.Add(Rates[i]);

                i++; Y += 170;

            }
        }

        public void Winner(string win)
        {
            haves[win]++;
            foreach (Label name in Names)
            {
                if (name.Text.Contains(name_person[win]))
                {
                    name.Invoke(new Action(() => name.Text = "Имя: " + name_person[win] + " x" + haves[win]));
                    break;
                }
            }
        }
    }
}
