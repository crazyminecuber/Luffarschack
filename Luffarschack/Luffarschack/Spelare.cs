using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Luffarschack
{
    public class Spelare
    {
        //Medlemsvariabler
        private Image bild;
        private int spelarnr;
        private string namn;
        private bool ai;

        //Konstruktor

        public Spelare (Image bild, int spelarnr, string namn, bool ai)
        {
            this.bild = bild;
            this.spelarnr = spelarnr;
            this.namn = namn;
            this.ai = ai;
        }

        //Egenskaper

        public Image Bild
        {
            get { return bild;  }
            set { bild = value; }
        }

        public int Spelarnr
        {
            get { return spelarnr; }
            set { spelarnr = Math.Abs(value); }
        }

        public string Namn
        {
            get { return namn;  }
            set { namn = value; }
        }

        public bool AI
        {
            get { return ai; }
            set { ai = value; }
        }


    }
}
