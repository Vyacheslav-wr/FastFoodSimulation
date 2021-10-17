using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class OrderTaker
    {
        private static Thread orderTakerThread;
        public static Semaphore semaphore = new Semaphore(0, 100);
        public static Queue<Ticket> tickets = new Queue<Ticket>();
        private static int NUMBER = 0;

        public static void startOrderTakerProcess()
        {
            orderTakerThread = new Thread(orderTakerMethod);
            orderTakerThread.Start();
        }

        public static void abortOrderTakerProcess()
        {
            orderTakerThread.Abort();
        }

        public static void orderTakerMethod()
        {
            while (true)
            {
                semaphore.WaitOne();

                int curnum = NUMBER++;
                Customer currentCustomer = Customer.entryQueue.Peek();

                // создаём чек (номер и информация о заказе)
                Receipt r = new Receipt(curnum, currentCustomer.OrderInfo);
                // отдаём чек клиенту
                currentCustomer.Receipt = r;

                // создаём бумажку с информацией о заказе и номером
                Ticket t = new Ticket(currentCustomer.OrderInfo, curnum);
                (Application.OpenForms["Form1"] as Form1).currentnumber(curnum);
                
                Customer.entryQueue.Dequeue();
                (Application.OpenForms["Form1"] as Form1).safeWaitingCustomers(Customer.entryQueue.Count);
                Server.exitQueue.Enqueue(currentCustomer);

                ListViewItem lvi1 = new ListViewItem(new string[] { t.Number.ToString(), currentCustomer.Name });
                (Application.OpenForms["Form1"] as Form1).safelist2add(lvi1);

                // передаём бумажку повару
                Cook.kitchenqueue.Enqueue(t);
                ListViewItem lvi2 = new ListViewItem(new string[] { t.Number.ToString(), t.Info });
                (Application.OpenForms["Form1"] as Form1).safelist1add(lvi2);
                Cook.semaphore.Release();
            }
        }


    }
}
