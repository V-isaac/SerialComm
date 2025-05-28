// definitions are ALWAYS at a top
// #define OLD_LOGIC

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
			Thread.Sleep(50);
			try {
				if (!_serialPort.IsOpen) { return; }

				string data = "";
				data = _serialPort.ReadLine();
				Debug.Print(data);

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
		public static double[,] points = new double[pAmount, 2];
		public void setPoints(double x, double y){ 
			for (int i = 0; i < pAmount; i++){ 
					points[i, 0] = x;
					points[i, 1] = y;
			}
		}
		bool first = true;

		string oldDataX = "";

		static int ArrayIndex = 0;
		double doubleX;
		double doubleY;
		int lastIndex;
		bool match = true;

		/* ----- code for testing (upload to arduino) -----

		for (int j = 0; j < 5; j++){  
			Serial.print(x);
			Serial.print("\t");
			Serial.print(sin(x) + random(-0.1, 0.1));
			Serial.print("\r\n");
		}
		x += 3.14/12;
		if (x > 100)[[unlikely]] { x = 0; }	
		*/

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
				string dataY = data.Substring(index + 1); // right after \t
				doubleX = Convert.ToDouble(dataX, System.Globalization.CultureInfo.GetCultureInfo("en-US"));
				doubleY = Convert.ToDouble(dataY, System.Globalization.CultureInfo.GetCultureInfo("en-US"));
				//Chart.Series[chartIndex].Points.Remove(Chart.Series[chartIndex].Points[1]);

				#if !OLD_LOGIC // on using new logic

				// add at least one point
				if (first){ 
					Chart.Series[chartIndex].Points.AddXY(doubleX, doubleY);
					first = !first;
				} 

				// remember index of last point
				lastIndex = Chart.Series[chartIndex].Points.IndexOf(Chart.Series[chartIndex].Points.Last());

				// iterate trough all points
				for (int i = 0; i < lastIndex; i++){ 
					if (doubleX == Chart.Series[chartIndex].Points[i].XValue){	// if repeating value found
						Chart.Series[chartIndex].Points[i].YValues[0] = doubleY;	// replace value
						match = true;																							// we found a match
						return;																										// exit early
					} else{
						match = false;																						// iterating trough collection yelded no match
					}
				}
				
				if (match != true){ Chart.Series[chartIndex].Points.AddXY(doubleX, doubleY); } // if no match found - add point
				#else
				if (first){
					setPoints(doubleX, doubleY);
					first = !first;
				}

				// check if current point is the same as previous one, replace Y if yes
				if (dataX == oldDataX){ 
					points[ArrayIndex, 1] = doubleY;
				}
				else{ 
					if (ArrayIndex < pAmount - 1) { // if we're not at the end of an array
						// add a point
						points[ArrayIndex, 0] = doubleX;
						points[ArrayIndex, 1] = doubleY;
						ArrayIndex++;
					} 
					else {
						for (int x = 0; x < pAmount - 1; x++){ // shifting array by 1
							points[x, 0] = points[x+1, 0];
							points[x, 1] = points[x+1, 1];
						}

						// adding point in the end
						points[pAmount-1, 0] = doubleX;
						points[pAmount-1, 1] = doubleY;
					} 
				}

				oldDataX = dataX;
				// redraw chart
				Chart.Series[chartIndex].Points.Clear();
				for (int i = 0; i < pAmount; i++){ 
					Chart.Series[chartIndex].Points.AddXY(points[i,0],points[i,1]);
				}
				#endif
			}
		}

		// closing the program 
		private void btnExit_Click(object sender, EventArgs e) {
			if(_serialPort.IsOpen) _serialPort.Close();
			Close(); 
		}

	// checking available ports
		private void btnCheck_Click(object sender, EventArgs e) {
			CBPort.Items.Clear();
			foreach (string s in SerialPort.GetPortNames()){
				CBPort.Items.Add(s);
			}
			CBPort.Text = Convert.ToString(CBPort.Items[0]);
		}

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

					if(Chart.Series.Any() == false) { // if there aren't any graphs - create one
						Chart.Series.Add(GraphName.Text);
						int i;
						i = Chart.Series.IndexOf(GraphName.Text);
						Chart.Series[i].ChartType = SeriesChartType.Spline;
					} 
					else { // otherwise check if any existing ones overlap
						for (int i = 0; i < Chart.Series.Count; i++) {
							if (Chart.Series[i].Name == GraphName.Text) { 
								MessageBox.Show("Нового графика не было построено.\n\rСвопадает имя с уже существующим", "Ошибка постройки графика");	
							}
							else{ 
								Chart.Series.Add(GraphName.Text);
								int j;
								j = Chart.Series.IndexOf(GraphName.Text);
								Chart.Series[j].ChartType = SeriesChartType.Spline;
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

					for(int i = 0; i < pAmount; i++){ 
						points[i, 0] = 0;
						points[i, 1] = 0;
					}


					isEnabled = !isEnabled;
					SwitchElements(isEnabled);

					ArrayIndex = 0;
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
			ArrayIndex = 0;
			Array.Clear(points, 0, points.Length);
			first = !first;
		}
	}
}