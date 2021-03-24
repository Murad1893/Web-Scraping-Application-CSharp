using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace k173722_Q2
{
    [Serializable] 
    public class Scripts
    {
        public string Script;
        public string Price;

        public Scripts() { }

        public Scripts(string script, string price) {
            this.Script = script;
            this.Price = price;
        }
    }
}
