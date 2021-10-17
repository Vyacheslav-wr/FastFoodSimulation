using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Ticket
    {
        public string Info { get; }
        public int Number { get; }

        public Ticket(string i, int n)
        {
            Info = i;
            Number = n;
        }
    }
}
