using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Receipt
    {
        public int Number { get; }
        public string OrderInfo { get; }

        public Receipt(int num, string info)
        {
            Number = num;
            OrderInfo = info;
        }
    }
}
