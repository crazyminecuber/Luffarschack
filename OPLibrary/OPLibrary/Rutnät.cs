using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace OPLibrary
{
    [SerializableAttribute]
    public class Rutnät
    {
        //variabler
        private int size;
        private int irad;
        private int rutSize;
        private int turnDelay;
        private int antalSpelare;
        private int antalAI;

        private List<Point> prePunkt = new List<Point>();


        //konstruktor
        public Rutnät(int size, int irad, int rutSize, int turnDelay, int antalSpelare, int antalAI, List<Point> prePunkt)
        {
            this.size = size;            
            this.irad = irad;
            this.rutSize = rutSize;
            this.turnDelay = turnDelay;
            this.antalSpelare = antalSpelare;           
            this.antalAI = antalAI;


            this.prePunkt = prePunkt;
        }

        public Rutnät(int size, int irad, int rutSize, int turnDelay, int antalSpelare, int antalAI)
        {
            this.size = size;
            this.irad = irad;
            this.rutSize = rutSize;
            this.turnDelay = turnDelay;
            this.antalSpelare = antalSpelare;
            this.antalAI = antalAI;
            
        }
        public Rutnät()
        {

        }



        //egenskaper
        public int Size
        {
            get { return size; }
            set
            {
                if (value > 0) size = value;
                else size = -value;
            }
        }

        public int Irad
        {
            get { return irad; }
            set
            {
                if (value > 0) irad = value;
                else irad = -value;
            }
        }

        public int RutSize
        {
            get { return rutSize; }
            set { rutSize = value; }
        }

        public int TurnDelay
        {
            get { return turnDelay; }
            set { turnDelay = value; }
        }



        public int AntalSpelare
        {
            get { return antalSpelare; }
            set { if (value > 0) antalSpelare = value;
                else antalSpelare = -value;
            }
        }

        public int AntalAI
        {
            get { return antalAI; } set { antalAI = value; }
        }


        public List<Point> PrePunkt
        {
            get { return prePunkt; }
            set { prePunkt = value; }
        }

        public object this[string propertyName]
        {
            get { return this.GetType().GetProperty(propertyName).GetValue(this, null); }
            set { this.GetType().GetProperty(propertyName).SetValue(this, value, null); }
        }


    }
}
