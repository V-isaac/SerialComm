using System;
using System.Drawing.Text;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SerialCommunication {
	public partial class MainScreen : Form {
		public MainScreen(){ InitializeComponent(); }
			
		bool isEnabled = false;
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
				if (!_serialPort.IsOpen) { return; } // if port somehow got closed preemptively - return
				
				string data = "";
				data = _serialPort.ReadLine();

				this.BeginInvoke( new SetTextDeleg(DataUpdate),
													new object[] { data } );
			}
			catch (Exception err) { 
				MessageBox.Show ("Устройство не ответило :: " + _serialPort.PortName + " " + err, "Ошибка чтения");
				_serialPort.Close();

				isEnabled = !isEnabled;
				SwitchElements(isEnabled);
			}
		}

		private void DataUpdate(string data){

			string d = "";
			
			// serial port output bit
			if (RBHEX.Checked){ 
				d = string.Join("", data.Select(c => String.Format("{0:X2}", Convert.ToInt32(c))));
				tbOutput.AppendText(d);
			}
			else { tbOutput.AppendText(data); }
			
			if (IsEscaping.Checked) { tbOutput.AppendText("\n"); }

			// serial plot output bit

		}


		private void btnExit_Click(object sender, EventArgs e) {
			if(_serialPort.IsOpen) _serialPort.Close();
			Close(); 
		}

		private void btnCheck_Click(object sender, EventArgs e) {
			CBPort.Items.Clear();
			foreach (string s in SerialPort.GetPortNames()){
				CBPort.Items.Add(s);
			}
			CBPort.Text = Convert.ToString(CBPort.Items[0]);
		}

		private void BtnOpen_Click(object sender, EventArgs e) {
			_serialPort.PortName	= CBPort.Text;
			_serialPort.BaudRate	= Convert.ToInt32(CBBaud.Text);
			_serialPort.Parity		= (Parity)Enum.Parse(typeof(Parity), Convert.ToString(CBParity.SelectedItem));
			_serialPort.DataBits	= Convert.ToInt32(CBBits.Text);
			_serialPort.StopBits	= (StopBits)Enum.Parse(typeof(StopBits), Convert.ToString(CBStopBit.SelectedItem));
			_serialPort.Handshake	= Handshake.RequestToSend;

			_serialPort.ReadTimeout = 500;
			_serialPort.WriteTimeout = 500;

			
			try { 
				if(! _serialPort.IsOpen){ 
					_serialPort.Open(); 
					_serialPort.DataReceived += new SerialDataReceivedEventHandler(Received);

					if(!Chart.Series.Any()){
						Chart.Series.Add(GraphName.Text);
					} 
					else{
						for (int i = 0; i < Chart.Series.Count; i++) {
							if (Chart.Series[i].Name == GraphName.Text) { 
								MessageBox.Show("Нового графика не было построено. Свопадает имя с уже существующим", "Ошибка постройки графика");	
							}
							else{ 
								Chart.Series.Add(GraphName.Text);
							} 
						}
					}
					
					isEnabled = !isEnabled;
					SwitchElements(isEnabled);
				}
			}
			catch (Exception err) { 
				MessageBox.Show ("Ошибка в открытии последовательного порта :: " + _serialPort.PortName + " " + err.Message, "Ошибка открытия");
			}
		}


		private void BtnClose_Click(object sender, EventArgs e) {
			try {
				if (_serialPort.IsOpen) {
					_serialPort.DiscardInBuffer();
					_serialPort.DiscardOutBuffer();
					_serialPort.Close();

					isEnabled = !isEnabled;
					SwitchElements(isEnabled);
				}
			}
			catch (Exception err) { 
				MessageBox.Show ("Ошибка в закрытии последовательного порта :: " + _serialPort.PortName + " " + err.Message, "Ошибка закрытия");
			}
		}

		private void btnClear_Click(Object sender, EventArgs e) {
			tbOutput.Text = "";
		}

		private void BtnSend_Click(Object sender, EventArgs e) {
			try {
				string data = tbInput.Text;
				_serialPort.Write(data);
				tbInput.Text = "";
			}
			catch (Exception err) { 
				MessageBox.Show ("Время ожидания отправки истекло \n\rили порт закрыт :: " + _serialPort.PortName + " " + err, " Ошибка порта");
			}
		}

		private void SwitchElements(bool en){
			en = !en;

			BtnClose.Enabled = !en;
			BtnSend.Enabled = !en;

			tbInput.Enabled = !en;
			BtnOpen.Enabled = en;
			CBPort.Enabled = en;
			CBBaud.Enabled = en;
			CBParity.Enabled = en;
			CBStopBit.Enabled = en;
			CBBits.Enabled = en;
			btnCheck.Enabled = en;

			BtnClearGraph.Enabled = !en;
			GraphName.Visible = en;
		}

		private void BtnClearGraph_Click(object sender, EventArgs e) {
			Chart.Series[0].Points.Clear();
		}
	}
}
