using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;



namespace OPLibrary
{
        [SerializableAttribute]
    public class Ruta 
    {
        //Medlemsvariabler

        private int x;
        private int y;
        private int mode;
        private int rutSize;

        //Konstruktor
        public Ruta(int x, int y, int rutSize, int mode)
        {
            this.x = x;
            this.y = y;
            this.mode = mode;
            this.rutSize = rutSize;
        }

        //Egenskper

        public int X
        {
            get { return x; }
            set
            {
                if (value > 0) x = value;
                else x = -value;
            }
        }

        public int Y
        {
            get { return y; }
            set
            {
                if (value > 0) y = value;
                else y = -value;
            }
        }

        public int Mode
        {
            get { return mode; }
            set
            {
                if (value < 0) mode = 0;
                else mode = value;
            }
        }

        public int RutSize
        {
            get; set;
        }

        public object this[string propertyName]
        {
            get { return this.GetType().GetProperty(propertyName).GetValue(this, null); }
            set { this.GetType().GetProperty(propertyName).SetValue(this, value, null); }
        }

        //Metoder

        public bool FemIrad(Ruta[,] ruta, int irad, int size)
        {
            int xled = 0;
            int yled = 0;
            int zled = 0;
            int qled = 0;


            //Kollar hur många är i rad för alla riktningar från den klickade rutan
            for (int n = -(irad - 1); n < (irad); n++)
            {
                //Horisontellt
                if (x + n >= 0 && x + n <= size - 1)
                {
                    if (ruta[x + n, y].Mode == this.mode) xled++;
                    else if (xled < irad) xled = 0;
                }

                //vertikalt
                if (y + n >= 0 && y + n <= size - 1)
                {
                    if (ruta[x, y + n].Mode == this.mode) yled++;
                    else if (yled < irad) yled = 0; ;
                }

                //snett nedåt
                if (x + n >= 0 && x + n <= size - 1 && y + n >= 0 && y + n <= size - 1)
                {
                    if (ruta[x + n, y + n].Mode == this.mode) zled++;
                    else if (zled < irad) zled = 0;
                }

                //snett uppåt
                if (x + n >= 0 && x + n <= size - 1 && y - n >= 0 && y - n <= size - 1)
                {
                    if (ruta[x + n, y - n].Mode == this.mode) qled++;
                    else if (qled < irad) qled = 0;
                }
            }

            if (xled >= irad || yled >= irad || zled >= irad || qled >= irad) return true;
            else return false;
        }
    }
}
