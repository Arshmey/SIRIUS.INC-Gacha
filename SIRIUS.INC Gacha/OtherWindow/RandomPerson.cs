using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIRIUS.INC_Gacha
{
    internal class RandomPerson
    {

        private Random rand_person = new Random();
        private TitlePerson titlePerson;
        private string path = "";
        string win = "";

        public RandomPerson(TitlePerson titlePerson, string path)
        {
            this.titlePerson = titlePerson;
            this.path = path;
        }

        public void RandomizePerson(PictureBox person, Dictionary<string, string> person_change, Dictionary<string, string> name_person, Label nameWin)
        {
            int i = 0;
            int rand;
            while (i != 25)
            {
                rand = rand_person.Next(1, 101);

                foreach (string key in person_change.Keys)
                {
                    string[] splt = key.Split(new string[] { "<VAL>" }, StringSplitOptions.None);
                    if (rand >= int.Parse(splt[0]) && rand <= int.Parse(splt[1]))
                    {
                        person.Image = Image.FromFile(@path + person_change[key] + ".png");
                        nameWin.Invoke(new Action(() => nameWin.Text = "Имя: " + name_person[person_change[key]]));
                        win = person_change[key];
                        break;
                    }
                }

                Thread.Sleep(100);
                i++;
            }
            titlePerson.Winner(win);
        }
    }
}
