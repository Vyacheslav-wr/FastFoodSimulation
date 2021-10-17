using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class Cook
    {
        private static Thread cookThread;
        public static int COOK_INTERVAL;
        public static Semaphore semaphore = new Semaphore(0, 100);
        public static Queue<Ticket> kitchenqueue = new Queue<Ticket>();

        public static void startCookProcess()
        {
            cookThread = new Thread(cookMethod);
            cookThread.Start();
        }

        public static void abortCookProcess()
        {
            cookThread.Abort();
        }

        public static void cookMethod()
        {
            while (true)
            {
                semaphore.WaitOne();

                // забирает бумажку
                Ticket t = kitchenqueue.Dequeue();
                (Application.OpenForms["Form1"] as Form1).safelist1remove(0);
                (Application.OpenForms["Form1"] as Form1).safetextbox1(t.Number);
                
                //готовит
                Thread.Sleep(COOK_INTERVAL*1000);

                // приготовил заказ по информации с бумажки
                Order ro = new Order(t.Info);
                // передал бумажку серверу
                Server.tickets.Enqueue(t);
                (Application.OpenForms["Form1"] as Form1).safetextbox2(t.Number);
                // передал заказ серверу
                Server.readyorders.Enqueue(ro);

                Server.semaphore.Release();
            }
        }
    }
}
