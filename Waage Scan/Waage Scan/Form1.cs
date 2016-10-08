using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;


namespace Waage_Scan
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }



        public class PortChat
        {
            static bool _continue;
            static SerialPort _serialPort;

            public static void test()
            {
                StringComparer stringComparer = StringComparer.OrdinalIgnoreCase;
                Thread readThread = new Thread(Read);

                // Create a new SerialPort object with default settings.
                _serialPort = new SerialPort();

                // Allow the user to set the appropriate properties.
                _serialPort.PortName = "COM3";
                _serialPort.BaudRate = 9600;
                _serialPort.Parity = Parity.Even;
                _serialPort.DataBits = 8;
                _serialPort.StopBits = StopBits.One;
                _serialPort.Handshake = Handshake.None;

                // Set the read/write timeouts
                _serialPort.ReadTimeout = 500000;
                _serialPort.WriteTimeout = 500000;

                _serialPort.Open();
                _continue = true;
                readThread.Start();
                Thread.Sleep(1000);
                while (_continue)
                {
                    if (_serialPort.IsOpen)
                    {
                        Read();
                    }
                    else
                    {
                        Console.WriteLine("NOOOOOOOOO!");
                    }
                }

                readThread.Join();
                _serialPort.Close();
            }

            public static void Read()
            {
                while (_continue)
                {
                    try
                    {
                        string tomodify = _serialPort.ReadLine();
                        //var test = _serialPort.ReadByte();
                        Console.WriteLine(tomodify);
                        Thread.Sleep(1000);
                    }

                    catch (TimeoutException ex)
                    {
                        throw ex;
                    }
                }
            }
            static int stripandcheck(string tomodify)
            {

                return 1;
            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}