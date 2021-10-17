using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class Server
    {
        private static Thread serverThread;
        public static Queue<Customer> exitQueue = new Queue<Customer>();
        public static Semaphore semaphore = new Semaphore(0, 100);
        public static Queue<Ticket> tickets = new Queue<Ticket>();
        public static Queue<Order> readyorders = new Queue<Order>();

        public static void startServerProcess()
        {
            serverThread = new Thread(serverMethod);
            serverThread.Start();
        }

        public static void abortServerProcess()
        {
            serverThread.Abort();
        }

        public static void serverMethod()
        {
            while (true)
            {
                semaphore.WaitOne();

                // взял заказ
                Order o = readyorders.Dequeue();
                // взял бумажку
                Ticket t = tickets.Dequeue();

                // находим человека, у которого номер чека такой же, как и на бумажке
                Customer customer = exitQueue.Where(c => c.getNumber() == t.Number).FirstOrDefault();

                // отдаем ему заказ
                customer.Order = o;

                // убираем его из очереди
                exitQueue = new Queue<Customer>(exitQueue.Where(c => c != customer));
                (Application.OpenForms["Form1"] as Form1).safelist2remove(0);
            }
        }
    }
}
