using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    class Item
    {
        public int ID;
        public string Name;
        public string Code;
        public DateTime DateFrom;
        public DateTime DateTo;
        public int isExt;
        public int ExtID;

        public Item()
        {
            ExtID = -1;
        }
    }
}
