﻿using System;
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
			Thread.Sleep(100);
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

		
		const int pAmount = 200;
		static double[,] points = new double[pAmount, 2];
		string oldDataX = "";

		static int ArrayIndex = 0;

		private void DataUpdate(string data){

			string d = "";
			
			// serial port output bit
			if (RBHEX.Checked){ 
				d = string.Join("", data.Select(c => String.Format("{0:X2}", Convert.ToInt32(c))));
				tbOutput.AppendText(d);
			}
			else { tbOutput.AppendText(data); }
			
			if (IsEscaping.Checked) { tbOutput.AppendText("\n"); } // usually \n\r, but C# did me dirty

			// serial plot output bit
			// data == text
			// BOLD ASSUMPTION - VALUES DON'T WRAP BACK, to infinity and beyond!
			int index = data.IndexOf('\t');
			int chartIndex = Chart.Series.IndexOf(GraphName.Text);

			if (index >= 0){ 
				string dataX = data.Substring(0,index);
				string dataY = data.Substring(index + 1); // right after \t

				// Debug.WriteLine(dataX + "\t" + oldDataX);
				// Debug.WriteLine(dataX == oldDataX);
				

				// Debug.WriteLine(dataX + ".." + dataY + " " + points[ArrayIndex, 0] + ".." + points[ArrayIndex, 1]);
				if (dataX == oldDataX){ 
					points[ArrayIndex, 1] = Convert.ToDouble(dataY, System.Globalization.CultureInfo.GetCultureInfo("en-US"));
				}
				else{ 
					if (ArrayIndex < pAmount - 1) { 
						points[ArrayIndex, 0] = Convert.ToDouble(dataX, System.Globalization.CultureInfo.GetCultureInfo("en-US"));
						points[ArrayIndex, 1] = Convert.ToDouble(dataY, System.Globalization.CultureInfo.GetCultureInfo("en-US"));

						ArrayIndex++;
					} 
					else {
						for (int x = 0; x < pAmount - 1; x++){ 
							points[x, 0] = points[x+1, 0];
							points[x, 1] = points[x+1, 1];
						}

						points[pAmount-1, 0] = Convert.ToDouble(dataX, System.Globalization.CultureInfo.GetCultureInfo("en-US"));
						points[pAmount-1, 1] = Convert.ToDouble(dataY, System.Globalization.CultureInfo.GetCultureInfo("en-US"));
					} 
				}

				oldDataX = dataX;
				// redraw chart
				Chart.Series[chartIndex].Points.Clear();
				for (int i = 0; i < pAmount; i++){ 
					Chart.Series[chartIndex].Points.AddXY(points[i,0],points[i,1]);
					// Debug.WriteLine(points[i, 0] + "\t" + points[i,1]);
				}
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
			_serialPort.Handshake	= Handshake.RequestToSend;

			_serialPort.ReadTimeout = 500;
			_serialPort.WriteTimeout = 500;

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
		}
	}
}