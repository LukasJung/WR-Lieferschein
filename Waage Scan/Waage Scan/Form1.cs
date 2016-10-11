using System;
using System.Windows.Forms;
using System.IO.Ports;
using System.Security.Cryptography.X509Certificates;
using System.Threading;


namespace Waage_Scan
{
    public partial class Form1 : Form
    {
        private PortChat _mySerialPort;
        private string sendme;

        public Form1()
        {
            InitializeComponent();
            _mySerialPort = new PortChat();

            try
            {
                _mySerialPort.CheckConnection();
            }
            catch(System.IO.IOException ex)
            {
                Console.WriteLine("IOException");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception");
                Console.WriteLine(ex.Message);
            }
            
        }

        private void gewichtfeld_click(object sender, EventArgs e)
        {
            _mySerialPort.Continue = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sendme = textBox2.Text;
            easylogconnection.sendtoexternalandprint(textBox2.Text);
            textBox1.Focus();
            _mySerialPort.Continue = true;
            _mySerialPort.StartRead();
        }

        private void lieferschein_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            var keyCode = e.KeyCode;

            switch (keyCode)
            {
                case (Keys.Enter):
                    _mySerialPort.StartRead();
                    var LsNR = textBox1.Text;
                    easylogconnection.sendtoexternal(LsNR);
                    textBox2.Text = _mySerialPort.Gewicht.ToString();
                    break;
                default:
                    break;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}