using System;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace SerialCommunication {
	public partial class MainScreen : Form {
		public MainScreen(){ InitializeComponent();
	}
		
		SerialPort  _serialPort = new SerialPort(	"COM1",
																							115200,
																							Parity.None,
																							8,
																							StopBits.One);

		// deliage for inter-thread communication
		private delegate void SetTextDeleg(string text);
		// excecutes on data in
		void Received(object sender, SerialDataReceivedEventArgs e) {
			Thread.Sleep(500);
			try {
				string data = _serialPort.ReadLine();
				this.BeginInvoke(new SetTextDeleg(DataUpdate),
													new object[] { data });
			}
			catch (TimeoutException) { 
				MessageBox.Show ("Устройство не отвечает в течении 5 секунд :: " + _serialPort.PortName, "Ошибка чтения");
				_serialPort.Close();
				BtnClose.Enabled = false;
				BtnOpen.Enabled = true;
			}
		}

		// updates lable window with data
		private void DataUpdate(string data){
			lblPortWindow.Text += data.Trim() + "\n\r";
		}


		private void btnExit_Click(object sender, EventArgs e) {
			_serialPort.Close();
			Close(); 
		}

		private void btnCheck_Click(object sender, EventArgs e) {
			CBPort.Items.Clear();
			foreach (string s in SerialPort.GetPortNames()){
				CBPort.Items.Add(s);
			}
			CBPort.Text = Convert.ToString(CBPort.Items[1]);
		}

		private void BtnOpen_Click(object sender, EventArgs e) {
			_serialPort.PortName	= CBPort.Text;
			_serialPort.BaudRate	= Convert.ToInt32(CBBaud.Text);
			_serialPort.Parity		= (Parity)Enum.Parse(typeof(Parity), Convert.ToString(CBParity.SelectedItem));
			_serialPort.DataBits	= Convert.ToInt32(CBBits.Text);
			_serialPort.StopBits	= (StopBits)Enum.Parse(typeof(StopBits), Convert.ToString(CBStopBit.SelectedItem));
			_serialPort.Handshake	= Handshake.None;

			_serialPort.ReadTimeout = 5000;
			_serialPort.WriteTimeout = 5000;

			try { 
				if(! _serialPort.IsOpen){ 
					_serialPort.Open(); 
					_serialPort.DataReceived += new SerialDataReceivedEventHandler(Received);

					BtnClose.Enabled = true;
					BtnOpen.Enabled = false;
				}
			}
			catch(Exception err){ 
				MessageBox.Show ("Ошибка в открытии последовательного порта :: " + _serialPort.PortName + " " + err.Message, "Ошибка открытия");
			}
	}


		private void BtnClose_Click(object sender, EventArgs e) {
			try{ 
				if(_serialPort.IsOpen){ 
					_serialPort.Close();
					BtnClose.Enabled = false;
					BtnOpen.Enabled = true;
				}
			}
			catch(Exception err){ 
				MessageBox.Show ("Ошибка в закрытии последовательного порта :: " + _serialPort.PortName + " " + err.Message, "Ошибка закрытия");
			}
		}
	}
}