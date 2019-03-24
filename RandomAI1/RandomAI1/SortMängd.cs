using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomAI1
{
    class SortMängd<T> : GList <T> where T : IComparable<T>
    {
        public SortMängd() : base()
        {

        }

        public override void LäggTill(T e)
        {
            bool finns = false;
            for (int i = 0; i < antal; i++)
            {
                if (lista[i].Equals(e))
                {
                    finns = true;
                    break;
                }
            }
            if (!finns)
                base.LäggTill(e);
            else throw new DublettException();
        }




    }
}
