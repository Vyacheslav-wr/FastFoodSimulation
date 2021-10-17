
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class Customer
    {
        public string Name { get; }
        public string OrderInfo { get; }
        public Receipt Receipt { get; set; }
        public Order Order { get; set; }

        private static Thread customersCreateThread;
        public static int ARRIVAL_INTERVAL;
        public static Queue<Customer> entryQueue = new Queue<Customer>();

        public Customer (string name, string info)
        {
            Name = name;
            OrderInfo = info;
        }

        public int getNumber()
        {
            return Receipt.Number;
        }

        public static void startCustomerProcess()
        {
            customersCreateThread = new Thread(createCustomers);
            customersCreateThread.Start();
        }

        public static void abortCustomerProcess()
        {
            customersCreateThread.Abort();
        }

        public static void createCustomers()
        {
            while (true)
            {
                Customer customer = new Customer(RandomStringUp(5), RandomStringLow(10));
                entryQueue.Enqueue(customer);
                (Application.OpenForms["Form1"] as Form1).safeWaitingCustomers(entryQueue.Count);
                OrderTaker.semaphore.Release();
                Thread.Sleep(ARRIVAL_INTERVAL*1000);
            }
        }

        public static string RandomStringLow(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[(new Random()).Next(s.Length)]).ToArray());
        }

        public static string RandomStringUp(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[(new Random()).Next(s.Length)]).ToArray());
        }
    }
}
