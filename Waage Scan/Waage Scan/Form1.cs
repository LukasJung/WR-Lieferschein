using System;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;


namespace Waage_Scan
{
    public partial class Form1 : Form
    {
        private PortChat mySerialPort;

        public Form1()
        {
            InitializeComponent();
            mySerialPort = new PortChat();
            mySerialPort.PropertyChanged += MySerialPort_PropertyChanged;

           
        }

        private void MySerialPort_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //TODO:
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           
        }
    }
}