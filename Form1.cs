using System;
using System.Diagnostics;
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

		private void MainScreen_Load(object sender, EventArgs e) {
			CBPort.Items.Clear();
			foreach (string s in SerialPort.GetPortNames()){
				CBPort.Items.Add(s);
			}
			CBPort.Text = Convert.ToString(CBPort.Items[0]);
		}

		bool isEnabled = false;
		SerialPort  _serialPort = new SerialPort(	"COM1",
																							115200,
																							Parity.None,
																							8,
																							StopBits.One);

		// deliage for inter-thread communication
		private delegate void SetTextDeleg(string text);

		// executes on data in
		void Received(object sender, SerialDataReceivedEventArgs e) {
			Thread.Sleep(50);
			try {
				if (!_serialPort.IsOpen) { return; }

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
		
		public const int pAmount = 53;
		bool first = true;

		double doubleX;
		double doubleY;
		int lastIndex;
		bool match = true;

		private void DataUpdate(string data){

			string d = "";
			
			// serial text output bit
			if (!RBHEX.Checked){ 
				tbOutput.AppendText(data); 
			}
			else {
				d = string.Join("", data.Select(c => String.Format("{0:X2}", Convert.ToInt32(c))));
				tbOutput.AppendText(d);
			}
			
			if (IsEscaping.Checked) { tbOutput.AppendText("\n"); } // usually \n\r, but C# did me dirty

			// serial plot output bit
			int index = data.IndexOf('\t');
			int chartIndex = Chart.Series.IndexOf(GraphName.Text);

			if (index > 0){ 
				string dataX = data.Substring(0,index);
				string dataY = data.Substring(index + 1);
				doubleX = Convert.ToDouble(dataX, System.Globalization.CultureInfo.GetCultureInfo("en-US"));
				doubleY = Convert.ToDouble(dataY, System.Globalization.CultureInfo.GetCultureInfo("en-US"));

				if (first){ 
					Chart.Series[chartIndex].Points.AddXY(doubleX, doubleY);
					first = !first;
				} 

				lastIndex = Chart.Series[chartIndex].Points.Count();
				for (int i = 0; i < lastIndex; i++){ 
					if (doubleX == Chart.Series[chartIndex].Points[i].XValue){	
						Chart.Series[chartIndex].Points[i].YValues[0] = doubleY;
						match = true;																					
						return;																							
					} else{
						match = false;																		
					}
				}
				
				if (match != true){ Chart.Series[chartIndex].Points.AddXY(doubleX, doubleY); } 
			}
		}

		// closing the program 
		private void btnExit_Click(object sender, EventArgs e) {
			if(_serialPort.IsOpen) _serialPort.Close();
			Close(); 
		}

		bool chartMatch = false;

		// port opening and chart editing			
		private void BtnOpen_Click(object sender, EventArgs e) {
			_serialPort.PortName	= CBPort.Text;
			_serialPort.BaudRate	= Convert.ToInt32(CBBaud.Text);
			_serialPort.Parity		= (Parity)Enum.Parse(typeof(Parity), Convert.ToString(CBParity.SelectedItem));
			_serialPort.DataBits	= Convert.ToInt32(CBBits.Text);
			_serialPort.StopBits	= (StopBits)Enum.Parse(typeof(StopBits), Convert.ToString(CBStopBit.SelectedItem));
			_serialPort.Handshake	= Handshake.XOnXOff;

			_serialPort.ReadTimeout = -1;
			_serialPort.WriteTimeout = -1;

			try { 
				if(! _serialPort.IsOpen) { 
					_serialPort.Open(); 
					_serialPort.DataReceived += new SerialDataReceivedEventHandler(Received);
					Debug.Print("Opened port. Chart count:");
					Debug.Write(Chart.Series.Count);

					// adding charts
					if(Chart.Series.Any() == false) { // if there aren't any - create one
						Chart.Series.Add(GraphName.Text);
						int i;
						i = Chart.Series.IndexOf(GraphName.Text);
						Chart.Series[i].ChartType = SeriesChartType.Spline;
						Debug.Print("Added chart. Chart count:");
						Debug.Write(Chart.Series.Count);
					} 
					else { // otherwise check if any existing ones overlap by name
						for (int i = 0; i < Chart.Series.Count; i++) {
							if (Chart.Series[i].Name == GraphName.Text) { 
								chartMatch = true;
								MessageBox.Show("Имя нового графика совпадает с существующим.\nОн может быть перезаписан", "Повторение имён");
								break;
							}
						}
						// didn't find any
						if (chartMatch == false){ 
							Chart.Series.Add(GraphName.Text);
							int j;
							j = Chart.Series.IndexOf(GraphName.Text);
							Chart.Series[j].ChartType = SeriesChartType.Spline;
						}
					}
					
					chartMatch = false; // resetting the state for another serial open attempt

					isEnabled = !isEnabled;
					SwitchElements(isEnabled);
				}
			}
			catch (Exception err) { 
				MessageBox.Show ("Ошибка в открытии последовательного порта :: " + _serialPort.PortName + " " + err.Message, "Ошибка открытия");
			}
		}

		// attempt at closing the port
		private void BtnClose_Click(object sender, EventArgs e) {
			try {
				if (_serialPort.IsOpen) {
					_serialPort.DiscardInBuffer();
					_serialPort.DiscardOutBuffer();
					_serialPort.Close();

					if (!first){ 
						first = true;
					}

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

			//BtnClearGraph.Enabled = !en;
			//BtnClearAll.Visible = !en;
			GraphName.Visible = en;
		}

		private void BtnClearGraph_Click(object sender, EventArgs e) {
			int chartIndex = Chart.Series.IndexOf(GraphName.Text);
			Chart.Series[chartIndex].Points.Clear();
			first = !first;
		}

		private void btnClearAll_Click(object sender, EventArgs e) {
			for (int i = 0; i < Chart.Series.Count; i++){ 
				Chart.Series[i].Points.Clear();
			}
		}
	}
}