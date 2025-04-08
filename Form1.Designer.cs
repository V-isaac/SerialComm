namespace SerialCommunication {
	partial class MainScreen {
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent() {
			this.FlowPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.lblPort = new System.Windows.Forms.Label();
			this.CBPort = new System.Windows.Forms.ComboBox();
			this.lblBaud = new System.Windows.Forms.Label();
			this.CBBaud = new System.Windows.Forms.ComboBox();
			this.lblParity = new System.Windows.Forms.Label();
			this.CBParity = new System.Windows.Forms.ComboBox();
			this.lblStopBit = new System.Windows.Forms.Label();
			this.CBStopBit = new System.Windows.Forms.ComboBox();
			this.lblDataBits = new System.Windows.Forms.Label();
			this.CBBits = new System.Windows.Forms.ComboBox();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.lblViewBlock = new System.Windows.Forms.Label();
			this.RBText = new System.Windows.Forms.RadioButton();
			this.RBHEX = new System.Windows.Forms.RadioButton();
			this.btnExit = new System.Windows.Forms.Button();
			this.btnCheck = new System.Windows.Forms.Button();
			this.BtnOpen = new System.Windows.Forms.Button();
			this.BtnClose = new System.Windows.Forms.Button();
			this.BtnSend = new System.Windows.Forms.Button();
			this.tbInput = new System.Windows.Forms.TextBox();
			this.lblPortBlock = new System.Windows.Forms.Label();
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.tbOutput = new System.Windows.Forms.TextBox();
			this.btnClear = new System.Windows.Forms.Button();
			this.RBBin = new System.Windows.Forms.RadioButton();
			this.IsEscaping = new System.Windows.Forms.CheckBox();
			this.FlowPanel.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// FlowPanel
			// 
			this.FlowPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.FlowPanel.Controls.Add(this.lblPort);
			this.FlowPanel.Controls.Add(this.CBPort);
			this.FlowPanel.Controls.Add(this.lblBaud);
			this.FlowPanel.Controls.Add(this.CBBaud);
			this.FlowPanel.Controls.Add(this.lblParity);
			this.FlowPanel.Controls.Add(this.CBParity);
			this.FlowPanel.Controls.Add(this.lblStopBit);
			this.FlowPanel.Controls.Add(this.CBStopBit);
			this.FlowPanel.Controls.Add(this.lblDataBits);
			this.FlowPanel.Controls.Add(this.CBBits);
			this.FlowPanel.Location = new System.Drawing.Point(692, 12);
			this.FlowPanel.Margin = new System.Windows.Forms.Padding(5);
			this.FlowPanel.Name = "FlowPanel";
			this.FlowPanel.Size = new System.Drawing.Size(96, 204);
			this.FlowPanel.TabIndex = 2;
			// 
			// lblPort
			// 
			this.lblPort.AutoSize = true;
			this.lblPort.Location = new System.Drawing.Point(3, 0);
			this.lblPort.Name = "lblPort";
			this.lblPort.Size = new System.Drawing.Size(35, 13);
			this.lblPort.TabIndex = 0;
			this.lblPort.Text = "Порт:";
			// 
			// CBPort
			// 
			this.CBPort.FormattingEnabled = true;
			this.CBPort.Location = new System.Drawing.Point(3, 16);
			this.CBPort.Name = "CBPort";
			this.CBPort.Size = new System.Drawing.Size(85, 21);
			this.CBPort.TabIndex = 5;
			this.CBPort.Text = "check ports";
			// 
			// lblBaud
			// 
			this.lblBaud.AutoSize = true;
			this.lblBaud.Location = new System.Drawing.Point(3, 40);
			this.lblBaud.Name = "lblBaud";
			this.lblBaud.Size = new System.Drawing.Size(85, 13);
			this.lblBaud.TabIndex = 6;
			this.lblBaud.Text = "Скорость (бод):";
			// 
			// CBBaud
			// 
			this.CBBaud.FormattingEnabled = true;
			this.CBBaud.Items.AddRange(new object[] {
            "300",
            "600",
            "750",
            "1200",
            "2400",
            "9600",
            "19200",
            "31250",
            "38400",
            "57600",
            "74880",
            "115200",
            "230400",
            "250000",
            "460800",
            "500000",
            "921600",
            "1000000",
            "2000000"});
			this.CBBaud.Location = new System.Drawing.Point(3, 56);
			this.CBBaud.Name = "CBBaud";
			this.CBBaud.Size = new System.Drawing.Size(85, 21);
			this.CBBaud.TabIndex = 7;
			this.CBBaud.Text = "115200";
			// 
			// lblParity
			// 
			this.lblParity.AutoSize = true;
			this.lblParity.Location = new System.Drawing.Point(3, 80);
			this.lblParity.Name = "lblParity";
			this.lblParity.Size = new System.Drawing.Size(58, 13);
			this.lblParity.TabIndex = 8;
			this.lblParity.Text = "Чётность:";
			// 
			// CBParity
			// 
			this.CBParity.FormattingEnabled = true;
			this.CBParity.Items.AddRange(new object[] {
            "None",
            "Odd",
            "Even",
            "Mark",
            "Space"});
			this.CBParity.Location = new System.Drawing.Point(3, 96);
			this.CBParity.Name = "CBParity";
			this.CBParity.Size = new System.Drawing.Size(85, 21);
			this.CBParity.TabIndex = 9;
			this.CBParity.Text = "None";
			// 
			// lblStopBit
			// 
			this.lblStopBit.AutoSize = true;
			this.lblStopBit.Location = new System.Drawing.Point(3, 120);
			this.lblStopBit.Name = "lblStopBit";
			this.lblStopBit.Size = new System.Drawing.Size(84, 13);
			this.lblStopBit.TabIndex = 10;
			this.lblStopBit.Text = "Бит остановки:";
			// 
			// CBStopBit
			// 
			this.CBStopBit.FormattingEnabled = true;
			this.CBStopBit.Items.AddRange(new object[] {
            "None",
            "One",
            "OnePointFive",
            "Two"});
			this.CBStopBit.Location = new System.Drawing.Point(3, 136);
			this.CBStopBit.Name = "CBStopBit";
			this.CBStopBit.Size = new System.Drawing.Size(85, 21);
			this.CBStopBit.TabIndex = 11;
			this.CBStopBit.Text = "One";
			// 
			// lblDataBits
			// 
			this.lblDataBits.AutoSize = true;
			this.lblDataBits.Location = new System.Drawing.Point(3, 160);
			this.lblDataBits.Name = "lblDataBits";
			this.lblDataBits.Size = new System.Drawing.Size(68, 13);
			this.lblDataBits.TabIndex = 12;
			this.lblDataBits.Text = "Бит данных:";
			// 
			// CBBits
			// 
			this.CBBits.FormattingEnabled = true;
			this.CBBits.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8"});
			this.CBBits.Location = new System.Drawing.Point(3, 176);
			this.CBBits.Name = "CBBits";
			this.CBBits.Size = new System.Drawing.Size(85, 21);
			this.CBBits.TabIndex = 13;
			this.CBBits.Text = "8";
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.flowLayoutPanel1.Controls.Add(this.lblViewBlock);
			this.flowLayoutPanel1.Controls.Add(this.RBText);
			this.flowLayoutPanel1.Controls.Add(this.RBHEX);
			this.flowLayoutPanel1.Controls.Add(this.RBBin);
			this.flowLayoutPanel1.Controls.Add(this.IsEscaping);
			this.flowLayoutPanel1.Location = new System.Drawing.Point(692, 218);
			this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(5);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(96, 109);
			this.flowLayoutPanel1.TabIndex = 14;
			// 
			// lblViewBlock
			// 
			this.lblViewBlock.AutoSize = true;
			this.lblViewBlock.Location = new System.Drawing.Point(3, 0);
			this.lblViewBlock.Name = "lblViewBlock";
			this.lblViewBlock.Size = new System.Drawing.Size(29, 13);
			this.lblViewBlock.TabIndex = 0;
			this.lblViewBlock.Text = "Вид:";
			// 
			// RBText
			// 
			this.RBText.Checked = true;
			this.RBText.Location = new System.Drawing.Point(3, 16);
			this.RBText.Name = "RBText";
			this.RBText.Size = new System.Drawing.Size(57, 17);
			this.RBText.TabIndex = 1;
			this.RBText.TabStop = true;
			this.RBText.Text = "Text";
			this.RBText.UseVisualStyleBackColor = true;
			// 
			// RBHEX
			// 
			this.RBHEX.Location = new System.Drawing.Point(3, 39);
			this.RBHEX.Name = "RBHEX";
			this.RBHEX.Size = new System.Drawing.Size(85, 17);
			this.RBHEX.TabIndex = 2;
			this.RBHEX.Text = "HEX";
			this.RBHEX.UseVisualStyleBackColor = true;
			// 
			// btnExit
			// 
			this.btnExit.Location = new System.Drawing.Point(692, 413);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(96, 23);
			this.btnExit.TabIndex = 5;
			this.btnExit.Text = "Close application";
			this.btnExit.UseVisualStyleBackColor = true;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// btnCheck
			// 
			this.btnCheck.Location = new System.Drawing.Point(692, 350);
			this.btnCheck.Name = "btnCheck";
			this.btnCheck.Size = new System.Drawing.Size(96, 23);
			this.btnCheck.TabIndex = 16;
			this.btnCheck.Text = "Check ports";
			this.btnCheck.UseVisualStyleBackColor = true;
			this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
			// 
			// BtnOpen
			// 
			this.BtnOpen.Location = new System.Drawing.Point(84, 395);
			this.BtnOpen.Name = "BtnOpen";
			this.BtnOpen.Size = new System.Drawing.Size(75, 23);
			this.BtnOpen.TabIndex = 0;
			this.BtnOpen.Text = "Open";
			this.BtnOpen.UseVisualStyleBackColor = true;
			this.BtnOpen.Click += new System.EventHandler(this.BtnOpen_Click);
			// 
			// BtnClose
			// 
			this.BtnClose.Enabled = false;
			this.BtnClose.Location = new System.Drawing.Point(3, 395);
			this.BtnClose.Name = "BtnClose";
			this.BtnClose.Size = new System.Drawing.Size(75, 23);
			this.BtnClose.TabIndex = 1;
			this.BtnClose.Text = "Close";
			this.BtnClose.UseVisualStyleBackColor = true;
			this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
			// 
			// BtnSend
			// 
			this.BtnSend.Location = new System.Drawing.Point(588, 366);
			this.BtnSend.Name = "BtnSend";
			this.BtnSend.Size = new System.Drawing.Size(75, 23);
			this.BtnSend.TabIndex = 4;
			this.BtnSend.Text = "Send";
			this.BtnSend.UseVisualStyleBackColor = true;
			this.BtnSend.Click += new System.EventHandler(this.BtnSend_Click);
			// 
			// tbInput
			// 
			this.tbInput.Location = new System.Drawing.Point(3, 366);
			this.tbInput.Name = "tbInput";
			this.tbInput.Size = new System.Drawing.Size(579, 20);
			this.tbInput.TabIndex = 3;
			// 
			// lblPortBlock
			// 
			this.lblPortBlock.Location = new System.Drawing.Point(3, 0);
			this.lblPortBlock.Name = "lblPortBlock";
			this.lblPortBlock.Size = new System.Drawing.Size(75, 22);
			this.lblPortBlock.TabIndex = 0;
			this.lblPortBlock.Text = "Выход порта:";
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.flowLayoutPanel2.Controls.Add(this.lblPortBlock);
			this.flowLayoutPanel2.Controls.Add(this.tbOutput);
			this.flowLayoutPanel2.Controls.Add(this.tbInput);
			this.flowLayoutPanel2.Controls.Add(this.BtnSend);
			this.flowLayoutPanel2.Controls.Add(this.BtnClose);
			this.flowLayoutPanel2.Controls.Add(this.BtnOpen);
			this.flowLayoutPanel2.Controls.Add(this.btnClear);
			this.flowLayoutPanel2.Location = new System.Drawing.Point(14, 12);
			this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(5);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Size = new System.Drawing.Size(674, 424);
			this.flowLayoutPanel2.TabIndex = 15;
			// 
			// tbOutput
			// 
			this.tbOutput.Location = new System.Drawing.Point(3, 25);
			this.tbOutput.Multiline = true;
			this.tbOutput.Name = "tbOutput";
			this.tbOutput.ReadOnly = true;
			this.tbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbOutput.Size = new System.Drawing.Size(660, 335);
			this.tbOutput.TabIndex = 5;
			this.tbOutput.TabStop = false;
			this.tbOutput.WordWrap = false;
			// 
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point(165, 395);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(97, 23);
			this.btnClear.TabIndex = 6;
			this.btnClear.Text = "Clear Serial Port";
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// RBBin
			// 
			this.RBBin.Location = new System.Drawing.Point(3, 62);
			this.RBBin.Name = "RBBin";
			this.RBBin.Size = new System.Drawing.Size(85, 17);
			this.RBBin.TabIndex = 3;
			this.RBBin.Text = "Binary";
			this.RBBin.UseVisualStyleBackColor = true;
			// 
			// IsEscaping
			// 
			this.IsEscaping.AutoSize = true;
			this.IsEscaping.Checked = true;
			this.IsEscaping.CheckState = System.Windows.Forms.CheckState.Checked;
			this.IsEscaping.Location = new System.Drawing.Point(3, 85);
			this.IsEscaping.Name = "IsEscaping";
			this.IsEscaping.Size = new System.Drawing.Size(45, 17);
			this.IsEscaping.TabIndex = 4;
			this.IsEscaping.Text = "\\r\\n";
			this.IsEscaping.UseVisualStyleBackColor = true;
			// 
			// MainScreen
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.btnCheck);
			this.Controls.Add(this.flowLayoutPanel2);
			this.Controls.Add(this.FlowPanel);
			this.Controls.Add(this.flowLayoutPanel1);
			this.Controls.Add(this.btnExit);
			this.Name = "MainScreen";
			this.Text = "Serial communication";
			this.FlowPanel.ResumeLayout(false);
			this.FlowPanel.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.flowLayoutPanel2.ResumeLayout(false);
			this.flowLayoutPanel2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.FlowLayoutPanel FlowPanel;
		private System.Windows.Forms.Label lblPort;
		private System.Windows.Forms.ComboBox CBPort;
		private System.Windows.Forms.Label lblBaud;
		private System.Windows.Forms.ComboBox CBBaud;
		private System.Windows.Forms.Label lblParity;
		private System.Windows.Forms.ComboBox CBParity;
		private System.Windows.Forms.Label lblStopBit;
		private System.Windows.Forms.ComboBox CBStopBit;
		private System.Windows.Forms.Label lblDataBits;
		private System.Windows.Forms.ComboBox CBBits;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Label lblViewBlock;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.Button btnCheck;
		private System.Windows.Forms.Button BtnOpen;
		private System.Windows.Forms.Button BtnClose;
		private System.Windows.Forms.Button BtnSend;
		private System.Windows.Forms.TextBox tbInput;
		private System.Windows.Forms.Label lblPortBlock;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
		private System.Windows.Forms.RadioButton RBText;
		private System.Windows.Forms.RadioButton RBHEX;
		private System.Windows.Forms.TextBox tbOutput;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.RadioButton RBBin;
		private System.Windows.Forms.CheckBox IsEscaping;
	}
}

