using System;
using System.ComponentModel;
using System.IO.Ports;
using System.Runtime.CompilerServices;
using System.Threading;
using Waage_Scan.Annotations;

namespace Waage_Scan
{
    public class PortChat :INotifyPropertyChanged
    {
        private bool _continue;
        private SerialPort _serialPort;
        private decimal _gewicht;


        public bool Continue
        {
            get { return _continue; }
            set { _continue = value; }
        }

        public SerialPort SerialPort
        {
            get { return _serialPort; }
            set { _serialPort = value; }
        }

        public decimal Gewicht
        {
            get { return _gewicht; }
            set
            {
                _gewicht = value;
                OnPropertyChanged(nameof(Gewicht));
            }
        }


        public void Test()
        {
            var stringComparer = StringComparer.OrdinalIgnoreCase;
            var readThread = new Thread(Read);

            // Create a new SerialPort object with default settings.
            _serialPort = new SerialPort
            {
                PortName = "COM3",
                BaudRate = 9600,
                Parity = Parity.Even,
                DataBits = 8,
                StopBits = StopBits.One,
                Handshake = Handshake.None,
                ReadTimeout = 500000,
                WriteTimeout = 500000
            };

            // Allow the user to set the appropriate properties.

            // Set the read/write timeouts

            SerialPort.Open();
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

        public void Read()
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
        public decimal StripAndCheck(string tomodify)
        {
            tomodify.Replace(" ", "");
            tomodify.Replace("+", "");
            tomodify.Replace("-", "");

            if (tomodify.Contains("kg"))
            {
                tomodify.Substring(0, tomodify.Length - 3);
                Gewicht = System.Convert.ToDecimal(tomodify);
                Gewicht = Gewicht * 1;
            }
            else if (tomodify.Contains("lb"))
            {
                tomodify.Substring(0, tomodify.Length - 2);
                Gewicht = System.Convert.ToDecimal(tomodify);
                var umrechner = 0.4535m;
                Gewicht = Gewicht * umrechner;
            }
            else if (tomodify.Contains("N"))
            {
                tomodify.Substring(0, tomodify.Length - 1);
                Gewicht = System.Convert.ToDecimal(tomodify);
                var umrechner = 9.81m;
                Gewicht = Gewicht / umrechner;

            }
            else if (tomodify.Contains("g"))
            {
                tomodify.Substring(0, tomodify.Length - 1);
                Gewicht = System.Convert.ToDecimal(tomodify);
                Gewicht = Gewicht * 1000;
            }
            return Gewicht;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
