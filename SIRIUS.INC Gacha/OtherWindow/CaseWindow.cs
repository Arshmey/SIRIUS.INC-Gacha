using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIRIUS.INC_Gacha.OtherWindow
{
    internal class CaseWindow
    {

        private Main main;

        private Panel MainPanel = new Panel();
        private Panel ScroledPanel = new Panel();
        private VScrollBar ScrollPanel = new VScrollBar();
        private Label nameWin = new Label();
        private Label lTrys = new Label();
        private Button Spin = new Button();
        private PictureBox personHaved = new PictureBox();

        private Dictionary<string, string> person_change = new Dictionary<string, string>();
        private Dictionary<string, string> name_person = new Dictionary<string, string>();
        private Dictionary<string, string> star_rate = new Dictionary<string, string>();
        private TitlePerson titlePerson;
        private RandomPerson randomPerson;
        private Thread rndPersonThread;
        private string path = "";
        private int trys = 0;

        public CaseWindow(Main main, string path)
        {
            this.main = main;
            this.path = path;

            setChange();
            setNames();
            setStarRates();

            LoadCase();
            titlePerson = new TitlePerson(name_person);
            randomPerson = new RandomPerson(titlePerson, path);
            rndPersonThread = new Thread(() => { randomPerson.RandomizePerson(personHaved, person_change, name_person, nameWin); });

            personHaved.Image = Image.FromFile(@path + "IMG_CASE.png");
            titlePerson.CreateTitles(ScroledPanel, person_change, star_rate, path);
        }

        public void LoadCase()
        {
            MainPanel.Anchor = AnchorStyles.None;
            MainPanel.BackColor = Color.Transparent;
            MainPanel.Controls.Add(ScroledPanel);
            MainPanel.Controls.Add(ScrollPanel);
            MainPanel.Location = new Point(5, 289);
            MainPanel.Name = "MainPanel";
            MainPanel.Size = new Size(1361, 512);
            main.Controls.Add(MainPanel);

            ScroledPanel.Anchor = AnchorStyles.None;
            ScroledPanel.BackColor = Color.Transparent;
            ScroledPanel.Location = new Point(3, 3);
            ScroledPanel.Name = "ScroledPanel";
            ScroledPanel.Size = new Size(1329, (person_change.Count * 170));
            MainPanel.Controls.Add(ScroledPanel);

            ScrollPanel.Anchor = AnchorStyles.None;
            ScrollPanel.Location = new Point(1335, 3);
            ScrollPanel.Maximum = ScroledPanel.Height - 500;
            ScrollPanel.Minimum = 3;
            ScrollPanel.Name = "ScrollPanel";
            ScrollPanel.Size = new Size(22, 509);
            ScrollPanel.Value = 3;
            ScrollPanel.Scroll += new ScrollEventHandler(ScrollPanel_ValueChanged);
            MainPanel.Controls.Add(ScrollPanel);

            nameWin.BackColor = Color.Transparent;
            nameWin.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            nameWin.ForeColor = SystemColors.ButtonFace;
            nameWin.Location = new Point(541, 205);
            nameWin.Name = "nameWin";
            nameWin.Size = new Size(295, 23);
            nameWin.Text = "Имя: Кейс Пантеон";
            nameWin.TextAlign = ContentAlignment.MiddleCenter;
            main.Controls.Add(nameWin);

            lTrys.AutoSize = true;
            lTrys.BackColor = Color.Transparent;
            lTrys.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold);
            lTrys.ForeColor = SystemColors.ButtonFace;
            lTrys.Location = new Point(790, 111);
            lTrys.Name = "lTrys";
            lTrys.Size = new Size(87, 16);
            lTrys.Text = "Попыток: 0";
            main.Controls.Add(lTrys);

            Spin.Anchor = AnchorStyles.None;
            Spin.BackColor = Color.Transparent;
            Spin.Location = new Point(605, 231);
            Spin.Name = "Spin";
            Spin.Size = new Size(160, 23);
            Spin.Text = "Крутить";
            Spin.UseVisualStyleBackColor = false;
            Spin.Click += new EventHandler(Spin_Click);
            main.Controls.Add(Spin);

            personHaved.Anchor = AnchorStyles.None;
            personHaved.BackColor = Color.Transparent;
            personHaved.Location = new Point(605, 42);
            personHaved.Name = "personHaved";
            personHaved.Size = new Size(160, 160);
            personHaved.TabStop = false;
            main.Controls.Add(personHaved);

        }

        private void Spin_Click(object sender, EventArgs e)
        {
            if (!rndPersonThread.IsAlive)
            {
                lTrys.Text = "Попыток: " + ++trys;
                rndPersonThread = new Thread(() => { randomPerson.RandomizePerson(personHaved, person_change, name_person, nameWin); });
                rndPersonThread.Start();
            }
        }

        private void setChange()
        {
            foreach (string name in File.ReadLines(@path + "Changes.txt"))
            {
                string[] spt = name.Split(new string[] { ">: " }, StringSplitOptions.None);
                person_change.Add(spt[0], spt[1]);
            }
        }

        private void setNames()
        {
            foreach (string name in File.ReadLines(@path +"Names.txt"))
            {
                string[] spt = name.Split(new string[] { ">: " }, StringSplitOptions.None);
                name_person.Add(spt[0], spt[1]);
            }
        }

        private void setStarRates()
        {
            foreach (string name in File.ReadLines(@path + "StarRate.txt"))
            {
                string[] spt = name.Split(new string[] { ">: " }, StringSplitOptions.None);
                star_rate.Add(spt[0], spt[1]);
            }
        }

        private void ScrollPanel_ValueChanged(object sender, EventArgs e)
        {
            ScroledPanel.Location = new Point(3, ScrollPanel.Value * -1);
        }

    }
}
