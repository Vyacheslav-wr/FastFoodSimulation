using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private delegate void SafeCallDelegate(int num);
        private delegate void SafeCallDelegateLVI(ListViewItem lvi);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void startButton_Click(object sender, EventArgs e)
        {
            bool b = true;
            try
            {
                Customer.ARRIVAL_INTERVAL = int.Parse(arrivalInterval.Text);
                Cook.COOK_INTERVAL = int.Parse(cookInterval.Text);
                b = false;
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid input");
            }

            if (b == true) return;

            Customer.startCustomerProcess();
            OrderTaker.startOrderTakerProcess();
            Cook.startCookProcess();
            Server.startServerProcess();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            Customer.abortCustomerProcess();
            OrderTaker.abortOrderTakerProcess();
            Cook.abortCookProcess();
            Server.abortServerProcess();
        }

        public void safeWaitingCustomers(int num)
        {
            if (waitingCustomers.InvokeRequired)
            {
                var d = new SafeCallDelegate(safeWaitingCustomers);
                waitingCustomers.Invoke(d, new object[] { num });
            }
            else
            {
                waitingCustomers.Text = num.ToString();
            }
        }

        public void safelist1add(ListViewItem lvi)
        {
            if (listView1.InvokeRequired)
            {
                var d = new SafeCallDelegateLVI(safelist1add);
                listView1.Invoke(d, new object[] { lvi });
            }
            else
            {
                listView1.Items.Add(lvi);
            }
        }

        public void safelist2add(ListViewItem lvi)
        {
            if (listView2.InvokeRequired)
            {
                var d = new SafeCallDelegateLVI(safelist2add);
                listView2.Invoke(d, new object[] { lvi });
            }
            else
            {
                listView2.Items.Add(lvi);
            }
        }

        public void safelist1remove(int num)
        {
            if (listView1.InvokeRequired)
            {
                var d = new SafeCallDelegate(safelist1remove);
                listView1.Invoke(d, new object[] { num });
            }
            else
            {
                listView1.Items.RemoveAt(0);
            }
        }

        public void safelist2remove(int num)
        {
            if (listView2.InvokeRequired)
            {
                var d = new SafeCallDelegate(safelist2remove);
                listView2.Invoke(d, new object[] { num });
            }
            else
            {
                listView2.Items.RemoveAt(0);
            }
        }

        public void safetextbox1(int num)
        {
            if (textBox1.InvokeRequired)
            {
                var d = new SafeCallDelegate(safetextbox1);
                textBox1.Invoke(d, new object[] { num });
            }
            else
            {
                textBox1.Text = num.ToString();
            }
        }

        public void safetextbox2(int num)
        {
            if (textBox2.InvokeRequired)
            {
                var d = new SafeCallDelegate(safetextbox2);
                textBox2.Invoke(d, new object[] { num });
            }
            else
            {
                textBox2.Text = num.ToString();
            }
        }

        public void currentnumber(int num)
        {
            if (currentNumber.InvokeRequired)
            {
                var d = new SafeCallDelegate(currentnumber);
                currentNumber.Invoke(d, new object[] { num });
            }
            else
            {
                currentNumber.Text = num.ToString();
            }
        }
    }
}
