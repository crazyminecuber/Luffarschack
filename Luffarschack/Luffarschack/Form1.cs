using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Pipes;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using OPLibrary;

namespace Luffarschack
{
    public partial class Form1 : Form
    {
        //TODO loopa igenom alla ai för att stänga dem
        //fixa så att uppkoplig till AI sker asyncront
        //fixa så att Spelet ej hänger sig när AI hänger sig!!!
        int antalspelare;
        int irad;
        int size;
        int antalAI;
        int rutSize;
        int turndelay;
        int topBlockHeight = 200;
        Label lblUpplysning = new Label { Font = new Font("Arial", 24, FontStyle.Bold)};
        Point lastMove = new Point(10000000,1000000);

        Ruta[,] rutor;
        PictureBox[,] uiRutor;

        
        Spelare[] spelare;
        int speltur;
        NamedPipeServerStream[] pipes;

        Rutnät rutnät;

 


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Variabler som kan ändras
            antalspelare = rutnät.AntalSpelare;
            irad = rutnät.Irad;
            size = rutnät.Size;
            rutSize = rutnät.RutSize;
            antalAI = rutnät.AntalAI;
            turndelay = rutnät.TurnDelay;
            rutnät["TurnDelay"] = 300;

            //Bildfält

            //Saker som inte bör ändras
           
            speltur = 0;
          
            this.ClientSize = new Size(size * rutSize, size * rutSize + topBlockHeight);
            rutor = new Ruta[size, size];
            uiRutor = new PictureBox[size, size];
           

            
            

            //Skapar rutobjekten samt sätter pictureboxar vid rätt position på formen som representerar rutorna 
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    rutor[x, y] = new Ruta(x, y, rutSize, -1);
                    uiRutor[x, y] = new PictureBox();
                    uiRutor[x, y].Location = new Point(rutor[x, y].X * rutSize + 1, rutor[x, y].Y * rutSize + 1 + topBlockHeight);
                    uiRutor[x, y].Size = new Size(rutSize - 1, rutSize - 1);
                    uiRutor[x, y].BackgroundImageLayout = ImageLayout.Stretch;
                    uiRutor[x, y].Click += new EventHandler(RutaClick);
                    this.Controls.Add(uiRutor[x, y]);
                }
            }

            //skapar pipes till alla AI
            pipes = new NamedPipeServerStream[antalAI];
            for (int n = 0; n < antalAI ; n++)
            {
                pipes[n] = new NamedPipeServerStream("test-pipe" + (n).ToString(), PipeDirection.InOut, 1, PipeTransmissionMode.Message);
            }

            // Lägger till kontroller och säger
            this.Controls.Add(lblUpplysning);
            string namn = spelare[speltur].Namn;
            if (namn[namn.Length - 1] != 's') namn += "s";

            UppdateraKontroller(namn + " tur!");
        }

        private void scaleFont(Label lab)
        {
            Image fakeImage = new Bitmap(1, 1); //As we cannot use CreateGraphics() in a class library, so the fake image is used to load the Graphics.
            Graphics graphics = Graphics.FromImage(fakeImage);


            SizeF extent = graphics.MeasureString(lab.Text, lab.Font);


            float hRatio = lab.Height / extent.Height;
            float wRatio = lab.Width / extent.Width;
            float ratio = (hRatio < wRatio) ? hRatio : wRatio;

            float newSize = lab.Font.Size * ratio;



            lab.Font = new Font(lab.Font.FontFamily, newSize, lab.Font.Style);

        }

        void UppdateraKontroller(string text)
        {
            lblUpplysning.Text = text;
            scaleFont(lblUpplysning);
            Point mitt = new Point(this.Width / 2 - lblUpplysning.Width / 2, topBlockHeight / 2 - lblUpplysning.Height / 2);
            lblUpplysning.Location = mitt;
            
           
        }

        //Kodavsnitt för varje ruta.click
         void RutaClick(object sender, EventArgs e)
        {
            string namn;
            if (speltur + 2 > antalAI + antalspelare) namn = spelare[0].Namn;
            else namn = spelare[speltur + 1].Namn;
            lblUpplysning.Text = NamnLogik(namn);
            if (spelare[speltur].AI != true)
            {
                
                var temp = sender as PictureBox;

                int x = temp.Location.X / rutSize;
                int y = (temp.Location.Y - topBlockHeight)/ rutSize;

                if (rutor[x,y].Mode == -1)
                {
                    //Om rutan inte redan är okuperad sätts rätt läga och bild
                    FyllRuta(x, y);
                }
            }   
            else  SkickaRutor(rutor, false);

        }

        string NamnLogik(string namn)
        {
            if (namn[namn.Length - 1] != 's') namn += "s";
            return namn + " tur!";
        }

        //Sätter alla pbx och rutor till blanka samt stänger alla pipes och startar om hela programmet från startform
        void återställ()
        {
            foreach(NamedPipeServerStream pipe in pipes)
            {
                pipe.Disconnect();
            }
            Application.Restart();
            System.Environment.Exit(1);
        }


        //visar vinststräng
        void ShowWinner(string meddelande)
        {
            MessageBox.Show(meddelande, "Vinst", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //Fyller info om vald ruta och sätter korrekt bild

         void FyllRuta(int X, int Y)
        {
            // om ruta redan är upptagen 
            if (rutor[X, Y].Mode != -1)
            {
                ShowWinner("Fuskare! Du är diskvalificerad!!!");
                återställ();
            }
            //annars sätts rätt ruta till rätt backgrund och spelare
            else
            {
                rutor[X, Y].Mode = speltur;
                uiRutor[X, Y].BackgroundImage = spelare[speltur].Bild;
                uiRutor[X, Y].Update();
                rutnät.PrePunkt.Insert(0, new Point(X, Y));
                lastMove = new Point(X, Y);
                Invalidate();

                //vilar om turndelay skall inkluderas vid exempelvis en AI match då det annars går för fort
                System.Threading.Thread.Sleep(turndelay);

                //Kontrollerar vinst
                bool vinst = rutor[X, Y].FemIrad(rutor, irad, size);

                //visar vem som har vunnit om någon har det
                if (vinst == true)
                {

                    ShowWinner("Grattis " + spelare[speltur].Namn);


                    speltur = 0;
                    if(antalAI != 0) SkickaRutor(rutor, true);
                    återställ();
                }
                else
                {

                    // fixar så att nästa speltur får rätt nummer
                    if (speltur > antalspelare + antalAI - 2) speltur = 0;
                    else speltur++;
                    string namn = spelare[speltur].Namn;
                    lblUpplysning.Text = NamnLogik(namn);
                    


                    // om nästa spelare är AI skickas rutor
                    if (spelare[speltur].AI == true && vinst == false)
                    {

                         SkickaRutor(rutor, false);
                    }
                }
            }
        }

        //metod för att skapa rutmönster
        protected override void OnPaint(PaintEventArgs e)
        {
            // ritar krysslinjer
            Graphics g = e.Graphics;
            for (int n = 0; n < size; n++)
            {
                g.DrawLine(Pens.Black, new Point(n * rutSize, topBlockHeight), new Point(n * rutSize, rutSize * size + topBlockHeight));
                g.DrawLine(Pens.Black, new Point(0, n * rutSize + topBlockHeight), new Point(rutSize * size, n * rutSize + topBlockHeight));
            }

            int x1 = lastMove.X * rutSize;
            int y1 = lastMove.Y * rutSize + topBlockHeight;
          
            g.DrawLine(Pens.Red, x1, y1 , x1 + rutSize, y1);
            g.DrawLine(Pens.Red, x1, y1 + rutSize, x1 + rutSize, y1 + rutSize);
            g.DrawLine(Pens.Red, x1 + rutSize, y1, x1 + rutSize, y1 + rutSize);
            g.DrawLine(Pens.Red, x1, y1, x1, y1 + rutSize);
        }


        


        public void SkickaRutor(Ruta[,] rutor, bool slut)
        {
            NamedPipeServerStream pipe = pipes[speltur];
            {
                try
                {

                    while (true)
                    {
                        try
                        {
                            pipe.WaitForConnection();
                            break;
                        }
                        catch (IOException)
                        {
                            pipe.Disconnect();
                            continue;
                        }
                    }

                    byte[] messageSlut = Serialize(slut);
                    byte[] messageRutnät = Serialize(rutnät);
                    byte[] messageRutor = Serialize(rutor);

                    
                    
                    pipe.Write(messageSlut, 0, messageSlut.Length);

                    if (!slut)
                    {
                        pipe.Write(messageRutor, 0, messageRutor.Length);
                        //pipe.ReadByte();
                        pipe.Write(messageRutnät, 0, messageRutnät.Length);
                        byte[] aiSvar = TaEmotMeddelande(pipe).Result;
                        Point temp = (Point)Deserialize(aiSvar);
                        FyllRuta(temp.X, temp.Y);
                        
                    }
                }
                catch(Exception error)
                {
                    MessageBox.Show(error.Message, Text);
                    System.Environment.Exit(1);
                }
                tmrAISvar.Enabled = false;
            }



        }

        //Serialiserar data
        public byte[] Serialize(object anySerializableObject)
        {
            using (var memoryStream = new MemoryStream())
            {
                (new BinaryFormatter()).Serialize(memoryStream, anySerializableObject);
                return memoryStream.ToArray() ;
            }
        }

        //Deserialiserar data
        public object Deserialize(byte[] data)
        {
                using (var memoryStream = new MemoryStream(data))
                    return (new BinaryFormatter()).Deserialize(memoryStream);
        }

        //Tar emot meddelande
        public async Task<byte[]> TaEmotMeddelande(NamedPipeServerStream pipe)
        {
            List<byte> message = new List<byte>();
            byte[] messageBuffer = new byte[1];
            do
            {
                pipe.Read(messageBuffer, 0, 1);
                message.Add(messageBuffer[0]);
                messageBuffer = new byte[1];
            }
            while (!pipe.IsMessageComplete);
            return message.ToArray();
        }


       
        //Importerar information från startform
        public void Import(Spelare[] spl, Rutnät rutn )
        {
            spelare = spl;
            rutnät = rutn;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MessageBox.Show("AI filen svarade inte inom 10 sekunder så programmet kommer därför att startas om. Vänligen välj en annan AI fil.", "Oresponsiv AI", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(1);
        }
    }
}
