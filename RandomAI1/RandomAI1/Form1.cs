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
using System.IO.Pipes;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace RandomAI1
{
    public partial class Form1 : Form
    {
        const int spelare = 0;
        int[] horisontelt = { 1, 0 };
        int[] vertikalt = { 0, 1 };
        int[] sneNed = { 1, 1 };
        int[] sneUpp = { 1, -1 };
        List<int[]> riktningar = new List<int[]>() { new int[] { 1, 0 }, new int[] { 0, 1 }, new int[] { 1, 1 }, new int[] { 1, -1 } };


        Random slump = new Random();
        Ruta[,] rutor;
        Rutnät rutnät;
        List<Rad> motståndarrader;
        List<Rad> egnarader = new List<Rad>();
        Ruta lastegenruta;
        Rad mittImittrutaAttack;
        Rad dödsattack;
        int[] dödsriktning;
        int mittrutasteg;
        int dödrutasteg;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            List<Rad> test = new List<Rad> {new Rad(horisontelt, 6, new List<Point>() ,3, false ),
                new Rad(horisontelt, 1, new List<Point>() ,3, false),
                new Rad(horisontelt, 5, new List<Point>() ,3, false),
                new Rad(horisontelt, 8, new List<Point>() ,3, false),
                new Rad(horisontelt, 8, new List<Point>() ,3, false),
                new Rad(horisontelt, 3, new List<Point>() ,3, false),
                new Rad(horisontelt, 9, new List<Point>() ,3, false)
                };
            test = Sortera(test)
;
            motståndarrader = new List<Rad>();
            egnarader = new List<Rad>();
            HämtaRutor();

            Application.Restart();
            Environment.Exit(1);
        }




        //kod för bräde analys och rutval
        Point AI()
        {
            Point LastRuta = new Point(0, 0);

            if (rutnät.PrePunkt.Count() != 0)
            {
                LastRuta = new Point(rutnät.PrePunkt.First().X, rutnät.PrePunkt.First().Y);
                List<Rad> temp = new List<Rad>();
                foreach(Rad MR in motståndarrader)
                {

                    temp.AddRange(Iradkollare(MR.Riktning, rutor[MR.Rutor.First().X, MR.Rutor.First().Y], rutor[LastRuta.X, LastRuta.Y].Mode));
                    motståndarrader = new List<Rad>();
                    motståndarrader.AddRange(temp);
                }

            }
            else
            {
                motståndarrader = new List<Rad>();
                egnarader = new List<Rad>();
                mittImittrutaAttack = new Rad(horisontelt, 0, new List<Point>(), 3, false);
                dödsattack = new Rad(horisontelt, 0, new List<Point>(), 3, false);
                mittrutasteg = 0;
                dödrutasteg = 0;
            }
            //Hämtar info om hur många irad vid senaste utsatta rutan
            if (rutor[LastRuta.X, LastRuta.Y].Mode != 0)
            {
                motståndarrader.AddRange(antalIrad(rutor[LastRuta.X, LastRuta.Y]));
            }

            //Sorterar måtståndarrader
            bool isdone = false;
            while (!isdone)
            {
                isdone = true;
                foreach (Rad MR in motståndarrader)
                {
                    //ändra tillbaka
                    if (!(MR.Irad >= rutnät.Irad - 2))
                    {
                        isdone = false;
                        motståndarrader.Remove(MR);
                        break;
                    }
                }
            }
            motståndarrader = Sortera(motståndarrader);


            //sorterar egna rader
            if (lastegenruta != null)
                egnarader.AddRange(antalIrad(lastegenruta));

            bool isDone = false;
            while (!isDone)
            {
                isDone = true;
                foreach (Rad ER in egnarader)
                {

                    if (!(ER.Irad >= rutnät.Irad - 2))
                    {
                        isDone = false;
                        egnarader.Remove(ER);
                        break;
                    }
                }
            }
           egnarader =  Sortera(egnarader);


            //Strategi för vad som ska göras prioritering

            //Om jag kan vinna
            if (egnarader.Count != 0)
            {
                if ((egnarader.First().Irad == rutnät.Irad - 1)
                    || (egnarader.First().Irad == rutnät.Irad - 1 && motståndarrader.First().Irad < rutnät.Irad - 1))
                {
                    return Blocker(egnarader);
                }
            }


            //Om det finns motståndarrader som behöver blockar blockeras dem
            if (motståndarrader.Count != 0)
            {
                return Blocker(motståndarrader);
            }
            else if (egnarader.Count() != 0 && mittrutasteg < 3)
            {
                return Blocker(egnarader);
            }

            else
            {
              //  return Mittrutaattack();
                return Dödsradattack();

                //Sätter ut från övre vänstra hörnet om inget bättre finns och kollar om det är möjligt att bilda rad på den raden
                // return Hörnattack();

            }
        }

        Point Dödsradattack()
        {
            Point svar;
            // Metoder fungarar i flera steg

            //initialatiion
            if (dödrutasteg == 0)
            {
                bool klar = false;
                do
                {
                    dödsattack = new Rad(riktningar[slump.Next(0, 4)], 1, new List<Point>(), 3, false);
                    dödsattack.Rutor.Add(new Point(slump.Next(0 + rutnät.Irad, rutnät.Size - rutnät.Irad), slump.Next(0 + rutnät.Irad, rutnät.Size - rutnät.Irad)));
                    try
                    {
                        List<Rad> test = antalIrad(rutor[dödsattack.Rutor.First().X, dödsattack.Rutor.First().Y], -1);
                        if (test.Count != 0)
                        {
                            if (test.First().Irad >= rutnät.Irad)
                            {
                                klar = true;
                            }
                        }
                    }
                    catch {
                        meddela("Fel vid radkollning inför dödsrutaattack");
                    }
                }
                while (!klar);
                dödrutasteg = 1;
                return dödsattack.Rutor.First();
            }
            



            //returnerar punkt +1 i rätt riktning
            else if (dödrutasteg == 1)
            {

                svar = new Point(dödsattack.Rutor.First().X + dödsattack.Riktning[0], dödsattack.Rutor.First().Y + dödsattack.Riktning[1]);
                dödrutasteg++;
            }

            

            //returnerar punkt +3 i rätt riktning
            else if (dödrutasteg == 2)
            {
                bool done = false;
                foreach (int[] riktning in riktningar)
                {
                    if (riktning != dödsattack.Riktning)
                    {
                        Point test = new Point(dödsattack.Rutor.First().X + 2 * dödsattack.Riktning[0], dödsattack.Rutor.First().Y + 2 * dödsattack.Riktning[1]);
                        List<Rad> nyriktning = Iradkollare(riktning, rutor[dödsattack.Rutor.First().X + 2 * dödsattack.Riktning[0], dödsattack.Rutor.First().Y + 2 * dödsattack.Riktning[1]], -1);
                        Sortera(nyriktning);
                        if (nyriktning.Count != 0)
                        {
                            if (nyriktning.First().Irad >= rutnät.Irad )
                            {
                                dödsriktning = riktning;
                                done = true;
                                break;
                            }

                        }
                    }
                }
                if (done == true)
                {

                    svar = new Point(dödsattack.Rutor.First().X + 2 * dödsattack.Riktning[0] + 2 * dödsriktning[0], dödsattack.Rutor.First().Y + 2 * dödsattack.Riktning[1] + 2 * dödsriktning[1]);
                    dödrutasteg++;
                }
                else
                {
                    dödrutasteg = 0;
                    return Dödsradattack();
                }
            }
            
            //returnerar punkt +2 i rätt riktning
            else if (dödrutasteg == 3)
            {
                svar = new Point(dödsattack.Rutor.First().X + 2 * dödsattack.Riktning[0] +  dödsriktning[0], dödsattack.Rutor.First().Y + 2 * dödsattack.Riktning[1] +  dödsriktning[1]);
                dödrutasteg++;
            }
            
            else if(dödrutasteg == 4)
            {
                svar = new Point(dödsattack.Rutor.First().X + 2 * dödsattack.Riktning[0] , dödsattack.Rutor.First().Y + 2 * dödsattack.Riktning[1] );
                dödrutasteg++;
            }
            else
            {
                meddela("dödrutastegfel");
                return Hörnattack();
            }

            // Om fel uppstår (som alltid...)
            if (rutor[svar.X, svar.Y].Mode != -1)
            {
                dödrutasteg = 0;
                if (dödrutasteg != 0)
                {

                    return Mittrutaattack();
                }

                return Hörnattack();
            }
            else
            {
                dödsattack.Rutor.Add(svar);
                return svar;
            }
        } 


        //primitiv metod för att utföra mittruta attack
        Point Mittrutaattack()
        {
            Point svar;
            // Metoder fungarar i flera steg

            //initialatiion
            if (mittrutasteg == 0)
            {
                bool klar = false;
                do
                {
                    mittImittrutaAttack = new Rad(riktningar[slump.Next(0, 4)], 1, new List<Point>(), 3, false);
                    mittImittrutaAttack.Rutor.Add(new Point(slump.Next(0 + rutnät.Irad, rutnät.Size - rutnät.Irad), slump.Next(0 + rutnät.Irad, rutnät.Size - rutnät.Irad)));
                    if (Iradkollare(horisontelt, rutor[mittImittrutaAttack.Rutor.First().X, mittImittrutaAttack.Rutor.First().Y], -1).First().Irad >= rutnät.Irad)
                    {
                        klar = true;
                    }
                }
                while (!klar);
                mittrutasteg = 1;
                return mittImittrutaAttack.Rutor.First();
            }
            //koll innan varje steg;
            List<Rad> temp = Iradkollare(mittImittrutaAttack.Riktning, rutor[mittImittrutaAttack.Rutor.Last().X, mittImittrutaAttack.Rutor.Last().Y], -1);
            if (temp.Count != 0)
            {
                if (temp.First().Irad >= rutnät.Irad)
                {
                    mittrutasteg = 0;
                    return Mittrutaattack();
                }
            }



            //returnerar punkt +1 i rätt riktning
            if (mittrutasteg == 1)
            {
                svar = new Point(mittImittrutaAttack.Rutor.First().X + mittImittrutaAttack.Riktning[0], mittImittrutaAttack.Rutor.First().Y + mittImittrutaAttack.Riktning[1]);
                mittrutasteg++;
            }
            //returnerar punkt +4 i rätt riktning
            else if (mittrutasteg == 2)
            {
                svar = new Point(mittImittrutaAttack.Rutor.First().X + 4 * mittImittrutaAttack.Riktning[0], mittImittrutaAttack.Rutor.First().Y + 4 * mittImittrutaAttack.Riktning[1]);
                mittrutasteg++;
            }

            //returnerar punkt +3 i rätt riktning
            else if (mittrutasteg == 3)
            {
                svar = new Point(mittImittrutaAttack.Rutor.First().X + 3 * mittImittrutaAttack.Riktning[0], mittImittrutaAttack.Rutor.First().Y + 3 * mittImittrutaAttack.Riktning[1]);
                mittrutasteg++;
            }

            //returnerar punkt +2 i rätt riktning
            else if (mittrutasteg == 4)
            {
                svar = new Point(mittImittrutaAttack.Rutor.First().X + 2 * mittImittrutaAttack.Riktning[0], mittImittrutaAttack.Rutor.First().Y + 2 * mittImittrutaAttack.Riktning[1]);
                mittrutasteg++;
            }
            else
            {
                meddela("mittrutastegfel");
                return Hörnattack();
            }

            // Om fel uppstår (som alltid...)
            if (rutor[svar.X, svar.Y].Mode != 0)
            {
                mittrutasteg = 0;
                if (mittrutasteg != 0)
                {
                    
                    return Mittrutaattack();
                }
                
                return Hörnattack();
            }
            else
            {
                mittImittrutaAttack.Rutor.Add(svar);
                return svar;
            }




            /*returnerar punkt +1 i rätt riktning
            if (mittrutasteg == 5)
            {
                svar = new Point(mittImittrutaAttack.Rutor.First().X + mittImittrutaAttack.Riktning[0], mittImittrutaAttack.Rutor.First().Y + mittImittrutaAttack.Riktning[1]);
                mittImittrutaAttack.Rutor.Add(svar);
                return svar;
            }
            */


        }

        Point SlumpadEnradsAttack()
        {
            Point svar = new Point();
            bool klar = false;
            if (egnarader.Count() == 0)
            {
                do
                {
                    svar = new Point(slump.Next(0, rutnät.Size), slump.Next(0, rutnät.Size));
                    if (Iradkollare(horisontelt, rutor[svar.X, svar.Y], 0).First().Irad > rutnät.Irad * 1.5)
                    {
                        klar = true;
                        egnarader.Add(Iradkollare(horisontelt, rutor[svar.X, svar.Y], 0).First());
                    }
                    else if (Iradkollare(vertikalt, rutor[svar.X, svar.Y], 0).First().Irad > rutnät.Irad * 1.5)
                    {
                        klar = true;
                        egnarader.Add(Iradkollare(vertikalt, rutor[svar.X, svar.Y], 0).First());
                    }
                    else if (Iradkollare(sneNed, rutor[svar.X, svar.Y], 0).First().Irad > rutnät.Irad * 1.5)
                    {
                        klar = true;
                        egnarader.Add(Iradkollare(sneNed, rutor[svar.X, svar.Y], 0).First());
                    }
                    else if (Iradkollare(sneUpp, rutor[svar.X, svar.Y], 0).First().Irad > rutnät.Irad * 1.5)
                    {
                        klar = true;
                        egnarader.Add(Iradkollare(sneUpp, rutor[svar.X, svar.Y], 0).First());
                    }
                }
                while (klar);
            }
            else
            {
                //Check if still epty row
                //else start new row
                //or continue
            }
            return (svar);
        }

        //Sorterar rader beroende på längd och openmid
        List<Rad> Sortera(List<Rad> Rader)
        {
            //om utan element returnerar -1
            int i = -1;
            List<Rad> svar = new List<Rad>();
            //nestlad foreachloop som sätter i en rad i taget från input()
            foreach (Rad IN in Rader)
            {


                if (svar.Count == 0)
                {
                    svar.Add(IN);

                }
                else
                {
                    foreach (Rad UT in svar)
                    {
                        i++;
                        if (IN.Irad < UT.Irad)
                        {
                            svar.Insert(i, IN);
                            break;
                        }
                        else if(IN.Irad == UT.Irad)
                        {
                            if(IN.OpenMid == true)
                            {
                                int n = 0;
                                bool done = false;
                                while(i - n >= 0  )
                                {

                                    

                                    if (svar[i - n].OpenMid == false || svar[i - n].Irad > IN.Irad)
                                    {
                                        svar.Insert(i - n + 1, IN);
                                        done = true;
                                        break;
                                    }
                                    n++;

                                }
                                if (done == false) svar.Insert(0, IN);

                                break;
                            }
                            else
                            {
                                svar.Insert(i, IN);
                                break;
                            }
                        }

                        else if (i == svar.Count - 1)
                        {
                            svar.Add(IN);
                            break;
                        }
                    }
                }

                i = -1;
            }
            svar.Reverse();
            return svar;
        }

        //Kollar om rad med mer än irad irad
        int FinnsAntalIradIRadsamling(int irad, List<Rad> rader)
        {
            int index = -1;
            int i = 0;
            foreach (Rad rad in rader)
            {
                if (rad.Irad >= irad)
                {
                    index = i;
                    break;
                }
                i++;
            }
            return index;
        }

















        //metod för att genomföra antalirad utan att ange mode
        List<Rad> antalIrad(Ruta checkRuta)
        {
            return antalIrad(checkRuta, checkRuta.Mode);
        }


        //metod för att ta reda på hur många irad runt en viss ruta
        List<Rad> antalIrad(Ruta checkRuta, int mode)
        {
            List<Rad> tempRader = new List<Rad>();

            //Kollar hur många är i rad för alla riktningar från den klickade rutan

            tempRader.AddRange(Iradkollare(horisontelt, checkRuta, mode));
            tempRader.AddRange(Iradkollare(vertikalt, checkRuta, mode));
            tempRader.AddRange(Iradkollare(sneNed, checkRuta, mode));
            tempRader.AddRange(Iradkollare(sneUpp, checkRuta, mode));


            return tempRader;
        }

        //Metod för att kolla efter rader i en viss riktning med ett visst mode
        List<Rad> Iradkollare(int[] riktning, Ruta checkRuta, int mode)
        {
            try
            {
                int x = checkRuta.X;
                int y = checkRuta.Y;
                int antalirad = 0;
                List<Point> kordinater = new List<Point>();
                List<Rad> rader = new List<Rad>();
                int ends = 3;
                bool openMid = false;
                //loopar igenom från -5 rutor till + 5 futor i rätt riktning
                for (int n = -(rutnät.Irad - 1); n < (rutnät.Irad); n++)
                {
                    //om ruta finns på brädet
                    if (x + n * riktning[0] >= 0 && x + n * riktning[0] < rutnät.Size && y + n * riktning[1] >= 0 && y + n * riktning[1] < rutnät.Size)
                    {
                        //om ruta är samma mode
                        if (rutor[x + n * riktning[0], y + n * riktning[1]].Mode == mode)
                        {
                            antalirad++;
                            kordinater.Add(new Point(x + n * riktning[0], y + n * riktning[1]));
                        }
                        //om denna är ledig och nästa är rätt mode och har ingen tidigare öppning
                        //fel i detta stycke

                        //Om rutor vid runt openmid finns på brädet
                    else if
                        (((0 <= x + (n + 1) * riktning[0] && x + (n + 1) * riktning[0] < rutnät.Size)
                        && (0 <= y + (n + 1) * riktning[1] && y + (n + 1) * riktning[1] < rutnät.Size))
                        && ((0 <= x + (n - 1) * riktning[0] && x + (n - 1) * riktning[0] < rutnät.Size)
                        && (0 <= y + (n - 1) * riktning[1] && y + (n - 1) * riktning[1] < rutnät.Size))
                        && rutor[x + n * riktning[0], y + n * riktning[1]].Mode == -1)
                        {

                                //om ruta är openmid och ingen tidigare ruta är openmid
                                if ((rutor[x + (n - 1) * riktning[0], y + (n - 1) * riktning[1]].Mode == mode) && (rutor[x + (1 + n) * riktning[0], y + (1 + n) * riktning[1]].Mode == mode && openMid != true))
                                {
                                    openMid = true;
                                    kordinater.Add(new Point(x + n * riktning[0], y + n * riktning[1]));
                                    ends = kordinater.Count() - 1;
                                }
                                //om ej rad ta bort
                                else if (antalirad < 2)
                                {
                                    antalirad = 0;
                                    kordinater.Clear();
                                    openMid = false;
                                }
                                // lägg till raden
                                else
                                {
                                    if (openMid == false)
                                    {
                                        //om det finns kordinater i änden av raden
                                        if (kordinater[0].X - riktning[0] >= 0 && kordinater[0].X - riktning[0] < rutnät.Size && kordinater[0].Y - riktning[1] >= 0 && kordinater[0].Y - riktning[1] < rutnät.Size)
                                        {
                                            if (rutor[kordinater[0].X - riktning[0], kordinater[0].Y - riktning[1]].Mode != -1) ends--;
                                        }
                                        else ends--;

                                        if (kordinater.Last().X + riktning[0] >= 0 && kordinater.Last().X + riktning[0] < rutnät.Size && kordinater.Last().Y + riktning[1] >= 0 && kordinater.Last().Y + riktning[1] < rutnät.Size)
                                        {
                                            if (rutor[kordinater.Last().X + riktning[0], kordinater.Last().Y + riktning[1]].Mode != -1) ends -= 2;
                                        }
                                        else ends -= 2;

                                    }
                                    // om det inte finns några ändor på raden och raden inte är openmid 4 irad
                                    if (ends != 0)
                                    {
                                        
                                        rader.Add(new Rad(riktning, antalirad, kordinater.ToList(), ends, openMid));
                                    }
                                    else if (antalirad == rutnät.Irad - 1 && openMid == true)
                                    {
                                        rader.Add(new Rad(riktning, antalirad, kordinater.ToList(), ends, openMid));
                                    }
                                //clearar raddata
                                antalirad = 0;
                                kordinater.Clear();
                                openMid = false;
                                ends = 3;

                            }

                         }
                        //om rad nått slut på brädet
                        else
                        {
                            if (antalirad > 1)
                            {
                                if (openMid == false)
                                {
                                    //om det finns kordinater i änden av raden
                                    if (kordinater[0].X - riktning[0] >= 0 && kordinater[0].X - riktning[0] < rutnät.Size && kordinater[0].Y - riktning[1] >= 0 && kordinater[0].Y - riktning[1] < rutnät.Size)
                                    {
                                        if (rutor[kordinater[0].X - riktning[0], kordinater[0].Y - riktning[1]].Mode != -1) ends--;
                                    }
                                    else ends--;

                                    if (kordinater.Last().X + riktning[0] >= 0 && kordinater.Last().X + riktning[0] < rutnät.Size && kordinater.Last().Y + riktning[1] >= 0 && kordinater.Last().Y + riktning[1] < rutnät.Size)
                                    {
                                        if (rutor[kordinater.Last().X + riktning[0], kordinater.Last().Y + riktning[1]].Mode != -1) ends -= 2;
                                    }
                                    else ends -= 2;

                                }
                                // om det inte finns några ändor på raden och raden inte är openmid 4 irad
                                if (ends != 0)
                                {
                                    
                                    rader.Add(new Rad(riktning, antalirad, kordinater.ToList(), ends, openMid));
                                }
                                else if(antalirad == rutnät.Irad -1 && openMid == true )
                                {
                                    rader.Add(new Rad(riktning, antalirad, kordinater.ToList(), ends, openMid));
                                }
                                

                            }
                            //clearar raddata
                            antalirad = 0;
                            kordinater.Clear();
                            openMid = false;
                            ends = 3;
                        }

                    }


                }
                //om rad finns efter loop är klar
                if (antalirad > 1)
                {
                    if (openMid == false)
                    {
                        if (kordinater[0].X - riktning[0] >= 0 && kordinater[0].Y - riktning[1] >= 0 && kordinater[0].X - riktning[0] < rutnät.Size && kordinater[0].Y - riktning[1] < rutnät.Size)
                        {
                            if (rutor[kordinater[0].X - riktning[0], kordinater[0].Y - riktning[1]].Mode != -1) ends--;
                        }
                        else ends--;
                        if (kordinater.Last().X + riktning[0] >= 0 && kordinater.Last().Y + riktning[1] >= 0 && kordinater.Last().X + riktning[0] < rutnät.Size && kordinater.Last().Y + riktning[1] < rutnät.Size)
                        {
                            if (rutor[kordinater.Last().X + riktning[0], kordinater.Last().Y + riktning[1]].Mode != -1) ends -= 2;
                        }
                        else ends -= 2;
                    }
                    // om det inte finns några ändor på raden och raden inte är openmid 4 irad
                    if (ends != 0)
                    {
                      
                        rader.Add(new Rad(riktning, antalirad, kordinater.ToList(), ends, openMid));
                    }
                    antalirad = 0;
                    kordinater.Clear();
                    openMid = false;
                }
                //behövs extra kontroll om enruta är fienderuta?
                return rader;
            }
            catch
            {
                meddela("Radkollar fel");
            }
            return null;
        }





















































































        /// <summary>
        /// Fungerande motoder
        /// </summary>
        /// <param name="text"></param>

        //Referens?
        //riktning snett nedåt krasher vid fyra irad
        Point Blocker(List<Rad> rader)
        {
            Rad rad = rader.First();
            int ends = rad.Ends;
            //Todo om inga änder är lediga ends = 0
            Point svar = new Point();
            if (rad.OpenMid == true)
            {
                svar = rad.Rutor[ends];
                rader.RemoveAt(0);
                return svar;
            }
            else if (ends == 3)
            {
                svar = new Point(rad.Rutor.First().X - rad.Riktning[0], rad.Rutor.First().Y - rad.Riktning[1]);
                if (rader.First().Irad >= rutnät.Irad - 1)
                    rader.First().Ends = 2;
                else rader.RemoveAt(0);

            }
            else if (ends == 2)
            {
                svar = new Point(rad.Rutor.Last().X + rad.Riktning[0], rad.Rutor.Last().Y + rad.Riktning[1]);
                rader.RemoveAt(0);
            }
            else if (ends == 1)
            {
                svar = new Point(rad.Rutor.First().X - rad.Riktning[0], rad.Rutor.First().Y - rad.Riktning[1]);
                rader.RemoveAt(0);
            }
            return svar;
        }

        //Sätter ut från övre vänstra hörnet om inget bättre finns och kollar om det är möjligt att bilda rad på den raden
        Point Hörnattack()
        {
            bool done = false;

            for (int y = 0; y < rutnät.Size; y++)
            {
                for (int x = 0; x < rutnät.Size; x++)
                {
                    if (rutor[x, y].Mode == -1)
                    {
                        for (int x2 = 0; x2 < rutnät.Irad && x2 + x < rutnät.Size; x2++)
                        {
                            if (!(rutor[x2 + x, y].Mode == -1 || rutor[x2 + x, y].Mode == spelare))
                            {
                                done = false;
                                break;
                            }
                            else done = true;
                        }
                        if (done == true) return new Point(x, y);
                        else break;
                    }
                }
            }
            return random();
        }

        //Metod för att sätt ut en slumpad bricka som inte är upptagen

        Point random()
        {
            Point svar = new Point();
            do
            {
                svar = new Point(slump.Next(rutnät.Size), slump.Next(rutnät.Size));
            }
            while (rutor[svar.X, svar.Y].Mode != -1);
            return svar;
        }


        void meddela(string text)
        {
            MessageBox.Show(text);
        }

        //Deserialiserar data
        public object Deserialize(byte[] data)
        {
            using (var memoryStream = new MemoryStream(data))
                return (new BinaryFormatter()).Deserialize(memoryStream);
        }

        //Serialiserar data
        public byte[] Serialize(object anySerializableObject)
        {
            using (var memoryStream = new MemoryStream())
            {
                (new BinaryFormatter()).Serialize(memoryStream, anySerializableObject);
                return memoryStream.ToArray();
            }
        }

        //Tar emot brädets position från servern
        public byte[]TaEmotMeddelande(NamedPipeClientStream pipe)
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

        //hämtar rutlistan samt rutbrädet som bytes och returerar som objekt 
        public void HämtaRutor()
        {
            while (true)
            {
                using (NamedPipeClientStream pipe = new NamedPipeClientStream(".", "test-pipe" + spelare.ToString(), PipeDirection.InOut))
                {

                    try
                    {
                        {
                            bool slut;
                            //Ta emot rutor och avgör om det är dennas tur
                            try
                            {
                                pipe.Connect(30000);
                            }
                            catch
                            {
                                meddela("brädet svarade inte inom 30 sekunder och AIn stängs därför av.");
                                System.Environment.Exit(1);
                                
                            }
                            if (pipe.IsConnected)
                            {

                                pipe.ReadMode = PipeTransmissionMode.Message;

                                byte[] messageSlut = TaEmotMeddelande(pipe);
                                slut = (bool)Deserialize(messageSlut);
                                if (slut) System.Environment.Exit(1);
                                byte[] messageRutor = TaEmotMeddelande(pipe);
                                rutor = (Ruta[,])Deserialize(messageRutor);
                                //pipe.WriteByte(1);
                                byte[] messageRutnät = TaEmotMeddelande(pipe);
                                rutnät = (Rutnät)Deserialize(messageRutnät);
                                Point svar = AI();

                                if (rutor[svar.X, svar.Y].Mode != -1)
                                {
                                    meddela("upptagenpunkt");

                                    svar = random();
                                }
                                lastegenruta = new Ruta(svar.X, svar.Y, 1, spelare);
                                byte[] messagebytes = Serialize(svar);
                                pipe.Write(messagebytes, 0, messagebytes.Length);
                            }
                            else
                            {
                                meddela("Kunde ej ansluta till brädet.");
                                Application.Exit();
                            }

                        }
                    }
                 catch { meddela("krash"); System.Environment.Exit(1); }
                }
            }

        }
    }
}

