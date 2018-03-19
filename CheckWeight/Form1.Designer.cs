namespace CheckWeight
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txtSN = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.btnResult = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbPort = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDown = new System.Windows.Forms.TextBox();
            this.txtUp = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.MathUpDown = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lineleadercheckweight = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSN
            // 
            this.txtSN.Font = new System.Drawing.Font("宋体", 15F);
            this.txtSN.Location = new System.Drawing.Point(124, 25);
            this.txtSN.Margin = new System.Windows.Forms.Padding(4);
            this.txtSN.Name = "txtSN";
            this.txtSN.Size = new System.Drawing.Size(479, 36);
            this.txtSN.TabIndex = 6;
            this.txtSN.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSN_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15F);
            this.label1.Location = new System.Drawing.Point(24, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "流水号";
            // 
            // btnConnect
            // 
            this.btnConnect.Font = new System.Drawing.Font("宋体", 12F);
            this.btnConnect.Location = new System.Drawing.Point(443, 25);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(4);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(163, 88);
            this.btnConnect.TabIndex = 5;
            this.btnConnect.Text = "初始化";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtServer
            // 
            this.txtServer.Enabled = false;
            this.txtServer.Font = new System.Drawing.Font("宋体", 12F);
            this.txtServer.Location = new System.Drawing.Point(124, 25);
            this.txtServer.Margin = new System.Windows.Forms.Padding(4);
            this.txtServer.Name = "txtServer";
            this.txtServer.ReadOnly = true;
            this.txtServer.Size = new System.Drawing.Size(311, 30);
            this.txtServer.TabIndex = 1;
            // 
            // btnResult
            // 
            this.btnResult.BackColor = System.Drawing.SystemColors.Control;
            this.btnResult.Enabled = false;
            this.btnResult.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnResult.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnResult.Location = new System.Drawing.Point(29, 70);
            this.btnResult.Margin = new System.Windows.Forms.Padding(4);
            this.btnResult.Name = "btnResult";
            this.btnResult.Size = new System.Drawing.Size(575, 255);
            this.btnResult.TabIndex = 4;
            this.btnResult.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 15F);
            this.label2.Location = new System.Drawing.Point(24, 78);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "端口号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 15F);
            this.label3.Location = new System.Drawing.Point(24, 32);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 25);
            this.label3.TabIndex = 6;
            this.label3.Text = "服务器";
            // 
            // cmbPort
            // 
            this.cmbPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPort.Font = new System.Drawing.Font("宋体", 12F);
            this.cmbPort.FormattingEnabled = true;
            this.cmbPort.Location = new System.Drawing.Point(124, 74);
            this.cmbPort.Margin = new System.Windows.Forms.Padding(4);
            this.cmbPort.Name = "cmbPort";
            this.cmbPort.Size = new System.Drawing.Size(308, 28);
            this.cmbPort.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 15F);
            this.label4.Location = new System.Drawing.Point(399, 131);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 25);
            this.label4.TabIndex = 8;
            this.label4.Text = "上限";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 15F);
            this.label5.Location = new System.Drawing.Point(213, 131);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 25);
            this.label5.TabIndex = 9;
            this.label5.Text = "下限";
            // 
            // txtDown
            // 
            this.txtDown.Font = new System.Drawing.Font("宋体", 12F);
            this.txtDown.Location = new System.Drawing.Point(283, 129);
            this.txtDown.Margin = new System.Windows.Forms.Padding(4);
            this.txtDown.Name = "txtDown";
            this.txtDown.Size = new System.Drawing.Size(108, 30);
            this.txtDown.TabIndex = 3;
            // 
            // txtUp
            // 
            this.txtUp.Font = new System.Drawing.Font("宋体", 12F);
            this.txtUp.Location = new System.Drawing.Point(478, 129);
            this.txtUp.Margin = new System.Windows.Forms.Padding(4);
            this.txtUp.Name = "txtUp";
            this.txtUp.Size = new System.Drawing.Size(114, 30);
            this.txtUp.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.MathUpDown);
            this.groupBox1.Controls.Add(this.txtServer);
            this.groupBox1.Controls.Add(this.txtUp);
            this.groupBox1.Controls.Add(this.btnConnect);
            this.groupBox1.Controls.Add(this.txtDown);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cmbPort);
            this.groupBox1.Location = new System.Drawing.Point(16, 9);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(631, 175);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // MathUpDown
            // 
            this.MathUpDown.Font = new System.Drawing.Font("宋体", 12F);
            this.MathUpDown.Location = new System.Drawing.Point(29, 124);
            this.MathUpDown.Margin = new System.Windows.Forms.Padding(4);
            this.MathUpDown.Name = "MathUpDown";
            this.MathUpDown.Size = new System.Drawing.Size(176, 39);
            this.MathUpDown.TabIndex = 12;
            this.MathUpDown.Text = "计算上下限";
            this.MathUpDown.UseVisualStyleBackColor = true;
            this.MathUpDown.Click += new System.EventHandler(this.MathUpDown_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lineleadercheckweight);
            this.groupBox2.Controls.Add(this.btnResult);
            this.groupBox2.Controls.Add(this.txtSN);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(16, 192);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(631, 343);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            // 
            // lineleadercheckweight
            // 
            this.lineleadercheckweight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lineleadercheckweight.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lineleadercheckweight.ForeColor = System.Drawing.Color.Black;
            this.lineleadercheckweight.Location = new System.Drawing.Point(43, 239);
            this.lineleadercheckweight.Margin = new System.Windows.Forms.Padding(4);
            this.lineleadercheckweight.Name = "lineleadercheckweight";
            this.lineleadercheckweight.Size = new System.Drawing.Size(549, 75);
            this.lineleadercheckweight.TabIndex = 13;
            this.lineleadercheckweight.Text = "点击此处，将配件齐全的异常重量产品，\r\n使用线长权限验证过站，过程中请勿移动产品\r\n（出现5pcs线长过站产品，需要重新计算重量上下限）";
            this.lineleadercheckweight.UseVisualStyleBackColor = false;
            this.lineleadercheckweight.Click += new System.EventHandler(this.Lineleadercheckweight_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 541);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "称重测试";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtSN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Button btnResult;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbPort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDown;
        private System.Windows.Forms.TextBox txtUp;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button MathUpDown;
        private System.Windows.Forms.Button lineleadercheckweight;
    }
}

