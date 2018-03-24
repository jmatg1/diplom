namespace ОПдляКК
{
    partial class fPort
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pYes = new System.Windows.Forms.Button();
            this.fPortListPorts = new System.Windows.Forms.ComboBox();
            this.pCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelspeed = new System.Windows.Forms.Label();
            this.fPortSpeed = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pYes
            // 
            this.pYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.pYes.Location = new System.Drawing.Point(43, 174);
            this.pYes.Name = "pYes";
            this.pYes.Size = new System.Drawing.Size(75, 23);
            this.pYes.TabIndex = 0;
            this.pYes.Text = "Да";
            this.pYes.UseVisualStyleBackColor = true;
            this.pYes.Click += new System.EventHandler(this.button1_Click);
            // 
            // fPortListPorts
            // 
            this.fPortListPorts.FormattingEnabled = true;
            this.fPortListPorts.Location = new System.Drawing.Point(77, 19);
            this.fPortListPorts.Name = "fPortListPorts";
            this.fPortListPorts.Size = new System.Drawing.Size(121, 21);
            this.fPortListPorts.TabIndex = 1;
            this.fPortListPorts.SelectedIndexChanged += new System.EventHandler(this.fPortListPorts_SelectedIndexChanged);
            // 
            // pCancel
            // 
            this.pCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.pCancel.Location = new System.Drawing.Point(125, 174);
            this.pCancel.Name = "pCancel";
            this.pCancel.Size = new System.Drawing.Size(75, 23);
            this.pCancel.TabIndex = 4;
            this.pCancel.Text = "Отмена";
            this.pCancel.UseVisualStyleBackColor = true;
            this.pCancel.Click += new System.EventHandler(this.pCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.labelspeed);
            this.groupBox1.Controls.Add(this.fPortSpeed);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.fPortListPorts);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(227, 119);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Порт и его настройки";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(180, 103);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(33, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "None";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(110, 103);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Flow control:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(180, 76);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(13, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(110, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Stop bits:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(61, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "None";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(61, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "8";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Parity:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Data bits:";
            // 
            // labelspeed
            // 
            this.labelspeed.AutoSize = true;
            this.labelspeed.Location = new System.Drawing.Point(6, 50);
            this.labelspeed.Name = "labelspeed";
            this.labelspeed.Size = new System.Drawing.Size(58, 13);
            this.labelspeed.TabIndex = 4;
            this.labelspeed.Text = "Скорость:";
            // 
            // fPortSpeed
            // 
            this.fPortSpeed.FormattingEnabled = true;
            this.fPortSpeed.Location = new System.Drawing.Point(77, 47);
            this.fPortSpeed.Name = "fPortSpeed";
            this.fPortSpeed.Size = new System.Drawing.Size(121, 21);
            this.fPortSpeed.TabIndex = 3;
            this.fPortSpeed.SelectedIndexChanged += new System.EventHandler(this.fPortSpeed_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Порт:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(18, 145);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(216, 13);
            this.label10.TabIndex = 6;
            this.label10.Text = "По умолчанию скорость порта  9600 бод.";
            // 
            // fPort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(251, 211);
            this.ControlBox = false;
            this.Controls.Add(this.label10);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pCancel);
            this.Controls.Add(this.pYes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fPort";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Параметры порта";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button pYes;
        private System.Windows.Forms.ComboBox fPortListPorts;
        private System.Windows.Forms.Button pCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelspeed;
        private System.Windows.Forms.ComboBox fPortSpeed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label10;
    }
}