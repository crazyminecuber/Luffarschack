using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OPLibrary;
using System.IO;

namespace Luffarschack
{
    public partial class StartForm : Form
    {

        /// <summary>
        /// TODO!!!
        /// 
        /// UI och spel
        /// 
        /// 

        /// Lägg till tidtare för varje drag
        /// lägg till område som visar timer och hur lång tid det är kvar på draget
        /// lägg till max timer
        /// ändra bakgrund

        /// visa senaste draget
        /// 
        /// </summary>
        List<GroupBox> boxarAI, boxarSpelare;
        List<PictureBox> spelarBilder, aiBilder;
        List<TextBox> spelarNamnboxar, aiNamnboxar;
        string[] aiFilvägar;
        public List<Image> bilder;
        Random slump = new Random();
        bool överskridAIFil = false;

        int irad = 5;
        int rutSize = 40;
        int turnDelay = 0;


        public StartForm()
        {
            InitializeComponent();
        }


        //Om index i cbxAntalSpelaer ändras ankallas showGroupboxes med rätt visningsparametrar
        private void cbxAntalSpelare_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbxAntalSpelare.SelectedIndex)
            {
                //Enspelareläge (1 AI + en spelare)
                case 0:
                    ShowGroupboxes(1, 1);
                    break;

                //Tvåspelarläge (0 AI + 2 spelare)
                case 1:
                    ShowGroupboxes(0,2);
                    break;

                //Enspelareläge (0 AI + 3 spelare)
                case 2:
                    ShowGroupboxes(0, 3);
                    break;

                //Enspelareläge (2 AI + 0 spelare)
                case 3:
                    ShowGroupboxes(2, 0);
                    break;

            }
        }

     
        //Sker vid laddning av formen
        private void StartForm_Load(object sender, EventArgs e)
        {
            InitieraUI();
            


            //Visar ingen groupbox;
            ShowGroupboxes(0, 0);

            

            //Slumpar bilder till alla spelare och AIs
            SlumpaBilder();
            cbxAntalSpelare.Update();
            ActiveControl = btnKör;


            //Utvecklarkontroll
            cbxAntalSpelare.SelectedIndex = 0;
            tbxSpelarNamn1.Text = "";
            tbxSpelarNamn2.Text = "";
            tbxAI1.Text = "";
            tbxAI2.Text = "";
            cbxBrädstorlek.SelectedIndex = 4;

        }

      

        //kontrollerar så att spelet är startklart annars meddelar den vad som är fel
        //Om allt är klart för start startar den.
        private void btnKör_Click(object sender, EventArgs e)
        {
            bool startKlar = false;

            //kontrollerar om klart för start genom att kalla på KollaSpelare för respektive spelläge
            switch (cbxAntalSpelare.SelectedIndex)
            {
                case 0:
                    startKlar = KollaSpelare(1,1);
                    break;

                case 1:
                    startKlar = KollaSpelare(0,2);
                    break;

                case 2:
                    startKlar = KollaSpelare(0,3);
                    break;

                case 3:
                    startKlar = KollaSpelare(2,0);
                    break;

                default:
                    Meddela("Vänligen ange hur många som ska spela.", MessageBoxIcon.Information);
                    cbxAntalSpelare.SelectAll();
                    startKlar = false;
                    break;
            }

            //startar respektive spelläge och samlar in spelarna i en lista
            if(startKlar == true)
            {
                List<Spelare> spelare = new List<Spelare>();
                Rutnät rutnät = new Rutnät();
                switch (cbxAntalSpelare.SelectedIndex)
                {
                    case 0:
                        spelare = AddSpelare(1, 1);
                        rutnät = AddRutnät(1, 1);
                        break;

                    case 1:
                        spelare = AddSpelare(0, 2);
                        rutnät = AddRutnät(0, 2);
                        break;

                    case 2:
                        spelare = AddSpelare(0, 3);
                        rutnät = AddRutnät(0, 3);
                        break;

                    case 3:
                        spelare = AddSpelare(2, 0);
                        rutnät = AddRutnät(2, 0);
                        break;

                    default:
                        Meddela("Hur gick det där till!!??? Vänligen säg til programeraren hur du gjorde!!???", MessageBoxIcon.Information);
                        break;
                }
                //Sätter igång AI från valda filer of minns
                foreach (string fil in aiFilvägar)
                {
                    if (File.Exists(fil))
                    {
                        System.Diagnostics.Process.Start(fil);
                    }
                }

                
                //Startar form
                Form1 form = new Form1();
                form.Import(spelare.ToArray(), rutnät);
                form.Show();
                this.Hide();

            }

               
        }

        private Rutnät AddRutnät(int ai, int spelare)
        {
            return new Rutnät(int.Parse(cbxBrädstorlek.Text), irad, rutSize, turnDelay, spelare, ai);
        }

        //Lägger till spelare från UI till spelare
        private List<Spelare> AddSpelare(int AI, int Spelare)
        {
            List<Spelare> temp = new List<Spelare>();
            for(int i = 0; i < AI; i++)
            {
                temp.Add(new Spelare(aiBilder[i].BackgroundImage, i + 1, aiNamnboxar[i].Text, true));
            }

            for (int i = 0; i < Spelare; i++)
            {
                temp.Add(new Spelare(spelarBilder[i].BackgroundImage, i + 1 + AI, spelarNamnboxar[i].Text, false));
            }

            return temp;
        }
        

        //Kollar så att de aktuella spelarna har angett tillräklig och korrekt infomation
        private bool KollaSpelare(int AI, int spelare)
        {
            //kollar så brädstorlek har valts
            if(cbxBrädstorlek.Text == "Brädstorlek")
            {
                Meddela("Vänligen ange brädstorlek.", MessageBoxIcon.Information);
                cbxBrädstorlek.SelectAll();
                return false;
            }




            //Kollar så att alla relevanta AI och spelare har fått namn
            for(int i = 0; i < AI; i++)
            {
                if(aiNamnboxar[i].Text == "")
                {
                    Meddela("Vänligen ange ett namn för AI " + (i + 1).ToString() + ".", MessageBoxIcon.Information);
                    aiNamnboxar[i].SelectAll();
                    return false;
                }
            }

            for (int i = 0; i < spelare; i++)
            {
                if (spelarNamnboxar[i].Text == "")
                {
                   
                    Meddela("Vänligen ange ett namn för spelare " + (i + 1).ToString() + ".", MessageBoxIcon.Information);
                    spelarNamnboxar[i].SelectAll();
                    return false;
                }
            }




            //Kollar så att alla delatande AI och spelare har en bild
            for (int i = 0; i < AI; i++)
            {
                if (aiBilder[i].BackgroundImage == null)
                {
                    Meddela("Vänligen ge AI" + (i + 1).ToString() + " en spelarbild.", MessageBoxIcon.Information);
                    aiNamnboxar[i].SelectAll();
                    return false;
                }
            }

            for (int i = 0; i < spelare; i++)
            {
                if (spelarBilder[i].BackgroundImage == null)
                {

                    Meddela("Vänligen ge spelare " + (i + 1).ToString() + " en spelarbild.", MessageBoxIcon.Information);
                    spelarNamnboxar[i].SelectAll();
                    return false;
                }
            }



            //Kollar så att AIarna har fått en fil överskrid AIfil är till för debuggning av externt startad AI
            for (int i = 0; i < AI; i++)
            {
                if (!(File.Exists(aiFilvägar[i])) && (!överskridAIFil))
                {
                    Meddela("Vänligen välj en AI-fil för AI " + (i + 1).ToString() + ".", MessageBoxIcon.Information);
                    return false;
                }
            }


            return true;
        }




        //Nollställer alla spelare till start
        private void NollställSpelare()
        {
            foreach(TextBox tb in aiNamnboxar)
            {
                tb.Text = "";
            }

            foreach (TextBox tb in spelarNamnboxar)
            {
                tb.Text = "";
            }
        }



        //slumpar bilder genom att kopiera bilderna i bildlistan och sedan ta bart den en coh en till antal pictureboxar är slu
        //eller att det finns för få bilder. I så fall meddelas detta
        private void SlumpaBilder()
        {
            List<Image> tempBilder = new List<Image>();
            tempBilder.AddRange(bilder);

            //loopar igenom för alla pictureboxar i menyn
            foreach (PictureBox pb in spelarBilder)
            {
                if (tempBilder.Count != 0)
                {
                    int bildNr = slump.Next(0, tempBilder.Count());
                    pb.BackgroundImage = tempBilder[bildNr];
                    tempBilder.RemoveAt(bildNr);
                }
                else
                {
                    Meddela("Det finns för få bilder inskrivna i programmet. Vänligen kontakta programeraren så att han kan fixa detta.", MessageBoxIcon.Error);
                    break;
                }
            }

            foreach (PictureBox pb in aiBilder)
            {
                if (tempBilder.Count != 0)
                {
                    int bildNr = slump.Next(0, tempBilder.Count());
                    pb.BackgroundImage = tempBilder[bildNr];
                    tempBilder.RemoveAt(bildNr);
                }
                else
                {
                    Meddela("Det finns för få bilder inskrivna i programmet. Vänligen kontakta programeraren så att han kan fixa detta.", MessageBoxIcon.Error);
                    break;
                }
            }

        }



        //Viser rätt groupboxer genom att dölja alla och sedan visa det antalet som angetts i metodanroppet
        private void ShowGroupboxes(int AI, int Spelare)
        {
            //döljer
            foreach (GroupBox gb in boxarAI)
            {
                gb.Visible = false;
                gb.Enabled = false;
            }

            foreach (GroupBox gb in boxarSpelare)
            {
                gb.Visible = false;
                gb.Enabled = false;
            }

            //Visar groupboxar i AIlista
            for (int i = 0; i < AI; i++)
            {
                boxarAI[i].Visible = true;
                boxarAI[i].Enabled = true;
            }

            //Visar gorupboxar i SpelarLista
            for (int i = 0; i < Spelare; i++)
            {
                boxarSpelare[i].Visible = true;
                boxarSpelare[i].Enabled = true;
            }

            //Om läget har ändrats skall spelarnamn nollställas
            NollställSpelare();

        }
























        //metoder för att handtera val av speler/AIbilder/aivägar
        private void btnAI1_Click(object sender, EventArgs e)
        {
            DialogResult r = fbdHämtaBild.ShowDialog();
            if (r == DialogResult.OK)
            {
                pbxAI1.BackgroundImage = Image.FromFile(fbdHämtaBild.FileName);
            }
        }

        private void btnAIBild2_Click(object sender, EventArgs e)
        {
            DialogResult r = fbdHämtaBild.ShowDialog();
            if (r == DialogResult.OK)
            {
                pbxAI2.BackgroundImage = Image.FromFile(fbdHämtaBild.FileName);
            }
        }

        private void btnBild1_Click(object sender, EventArgs e)
        {
            DialogResult r = fbdHämtaBild.ShowDialog();
            if (r == DialogResult.OK)
            {
                pbxSpelare1.BackgroundImage = Image.FromFile(fbdHämtaBild.FileName);
            }
        }

        private void btnBild2_Click(object sender, EventArgs e)
        {
            DialogResult r = fbdHämtaBild.ShowDialog();
            if (r == DialogResult.OK)
            {
                pbxSpelare2.BackgroundImage = Image.FromFile(fbdHämtaBild.FileName);
            }
        }

        private void btnBild3_Click(object sender, EventArgs e)
        {
            DialogResult r = fbdHämtaBild.ShowDialog();
            if (r == DialogResult.OK)
            {
                pbxSpelare3.BackgroundImage = Image.FromFile(fbdHämtaBild.FileName);
            }
        }

        //AI filvägar
        private void btnAIfil1_Click(object sender, EventArgs e)
        {
            DialogResult r = AIfilväljare.ShowDialog();
            if (r == DialogResult.OK)
            {
                aiFilvägar[0] = AIfilväljare.FileName;
            }
        }

        private void StartForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(1);
        }

        private void cbxBrädstorlek_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int tal = cbxBrädstorlek.SelectedIndex;

                switch (tal)
                {
                    case 0:
                        rutSize = 80;
                        break;

                    case 1:
                        rutSize = 60;
                        break;

                    case 2:
                        rutSize = 40;
                        break;

                    case 3:
                        rutSize = 30;
                        break;

                    case 4:
                        rutSize = 25;
                        break;

                    default:
                        break;
                }


            }
            catch
            {

            }
        }

        private void btnAIFil2_Click(object sender, EventArgs e)
        {
            DialogResult r = AIfilväljare.ShowDialog();
            if (r == DialogResult.OK)
            {
                aiFilvägar[1] = AIfilväljare.FileName;
            }
        }


        private void InitieraUI()
        {
            boxarAI = new List<GroupBox>(){
            grpAI1,
            grpAI2,
            };

            //Adderar alla spelargroupboxar
            boxarSpelare = new List<GroupBox>(){
            grpSpelare1,
            grpSpelare2,
            grpSpelare3,
            };

            //Ai bilder
            aiBilder = new List<PictureBox>()
            {
                pbxAI1,
                pbxAI2,
            };

            //skapar en lista på alla pictureboxar
            spelarBilder = new List<PictureBox>()
            {

                pbxSpelare1,
                pbxSpelare2,
                pbxSpelare3,
            };

            aiNamnboxar = new List<TextBox>()
            {
                 tbxAI1,
                tbxAI2,
            };

            spelarNamnboxar = new List<TextBox>()
            {
                tbxSpelarNamn1,
                tbxSpelarNamn2,
                tbxSpelarNamn3,
            };

            aiFilvägar = new string[2];

            //hämtar alla bilder
            bilder = new List<Image>()
            {
                Luffarschack.Properties.Resources.Basta_dommaren,
                Luffarschack.Properties.Resources.puck,
                Luffarschack.Properties.Resources.svart,
                Luffarschack.Properties.Resources.backspace_arrow,
                Luffarschack.Properties.Resources.Korv,
                Luffarschack.Properties.Resources.Ocelot,
                Luffarschack.Properties.Resources.Einstein,

            };
        }



        //meddellar en string som en Messagebox då man kan välja icon.
        private void Meddela(string meddelande, MessageBoxIcon icon)
        {
            MessageBox.Show(meddelande, "Meddelande", MessageBoxButtons.OK, icon);
        }
    }
}
