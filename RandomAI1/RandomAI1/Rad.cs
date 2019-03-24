using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RandomAI1
{
    class Rad : IComparable <Rad>
    {
        //medlemsvariabler
        //riktning anges enlingt om x öker så är första sant. Annars är den falsk. I y led gäller samma för andra variblen
        private int[] riktning = { 1,0 };
        private List <Point> rutor = new List<Point>();
        private int irad = 0;
        private int ends = 3;
        private bool openMid = false;

        public Rad (int[] riktning, int irad, List<Point> rutor, int ends, bool openMid)
        {
            this.riktning = riktning;
            this.rutor = rutor;
            this.irad = irad;
            this.ends = ends;
            this.openMid = openMid;
        }

        public int[] Riktning
        {
            get { return riktning; }
            set { riktning = value; }
        }

        public List<Point> Rutor
        {
            get { return rutor; }
            set { rutor = value; }
        }

       
        public int Irad
        {
            get { return irad; }
            set { irad = value; }
        }

        //ger om open (mid = falese) 1 = föregående ruta öppen 2 = nästa ruta öppen
        //annars ger den positionen på open mid (0 är position)
        public int Ends 
        {
            get { return ends; }
            set { ends = value; }
        }

        public bool OpenMid
        {
            get { return openMid; }
            set { openMid = value; }
        }

        public object this[string propertyName]
        {
            get { return this.GetType().GetProperty(propertyName).GetValue(this, null); }
            set { this.GetType().GetProperty(propertyName).SetValue(this, value, null); }
        }

        public int CompareTo(Rad row)
        {
            return irad.CompareTo(row.Irad);
        }

    }
}
