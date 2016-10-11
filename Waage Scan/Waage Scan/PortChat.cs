using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.Ports;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Waage_Scan.Annotations;


namespace Waage_Scan
{
    public class PortChat : INotifyPropertyChanged
    {
        #region members
        private bool _continue;
        private SerialPort _serialPort;
        private decimal _gewicht;
        private string _toModify;
        private string _windowname;
        public string _easylog;
        #endregion

        #region Fields
        public string ToModify
        {
            get { return _toModify; }
            set { _toModify = value; }
        }

        public string easylog
        {
            get { return _easylog; }
            set { _easylog = value; }
        }

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
        public string WindowName
        {
            get { return _windowname; }
            set { _windowname = value; }
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
        #endregion


        public PortChat()
        {
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
        }

        public void StartRead()
        {
            SerialPort.Open();
            _continue = true;

            if (_serialPort.IsOpen)
            {
                Read();
            }
            else
            {
                Console.WriteLine("NOOOOOOOOO!");
            }
            _serialPort.Close();
        }

        private void Read()
        {
            try
            {
                ToModify = _serialPort.ReadLine().ToString();
            }
            catch (TimeoutException ex)
            {
                Debugger.Break();
                MessageBox.Show(string.Format("Message:\n{0}\n\nInnerMessage:\n{1}", ex.Message, ex.InnerException));
            }
            StripAndCheck();
        }

        public decimal StripAndCheck()
        {
            ToModify = ToModify.Replace(" ", "").Replace("+", "").Replace("-", "").Replace("\r", "").Replace(".", ",");

            if (ToModify.Contains("kg"))
            {
                ToModify = ToModify.Substring(0, ToModify.Length - 3);
                Gewicht = System.Convert.ToDecimal(ToModify);
            }
            else if (ToModify.Contains("lb"))
            {
                ToModify = ToModify.Substring(0, ToModify.Length - 2);
                Gewicht = System.Convert.ToDecimal(ToModify);
                var umrechner = 0.4535m;
                Gewicht = Gewicht * umrechner;
            }
            else if (ToModify.Contains("N"))
            {
                ToModify = ToModify.Substring(0, ToModify.Length - 1);
                Gewicht = System.Convert.ToDecimal(ToModify);
                var umrechner = 9.81m;
                Gewicht = Gewicht / umrechner;
            }
            else if (ToModify.Contains("g"))
            {
                ToModify = ToModify.Substring(0, ToModify.Length - 1);
                Gewicht = System.Convert.ToDecimal(ToModify);
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
