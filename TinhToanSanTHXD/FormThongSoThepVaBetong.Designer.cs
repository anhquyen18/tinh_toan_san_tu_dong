
namespace TinhToanSanTHXD
{
    partial class FormThongSoThepVaBetong
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormThongSoThepVaBetong));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txbCeR = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txbAlphaR = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cbxHeSoLamViec = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txbEs = new System.Windows.Forms.TextBox();
            this.txbEb = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txbRsc = new System.Windows.Forms.TextBox();
            this.txbRbt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txbRs = new System.Windows.Forms.TextBox();
            this.txbRb = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxLoaiThep = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxLoaiBeTong = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.AutoSize = true;
            this.btnOk.Location = new System.Drawing.Point(679, 154);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 27);
            this.btnOk.TabIndex = 68;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.Location = new System.Drawing.Point(770, 154);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 27);
            this.btnCancel.TabIndex = 67;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txbCeR
            // 
            this.txbCeR.Location = new System.Drawing.Point(550, 111);
            this.txbCeR.Name = "txbCeR";
            this.txbCeR.ReadOnly = true;
            this.txbCeR.Size = new System.Drawing.Size(100, 22);
            this.txbCeR.TabIndex = 66;
            this.txbCeR.Text = "0.0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(513, 114);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 17);
            this.label10.TabIndex = 65;
            this.label10.Text = "ξR:";
            // 
            // txbAlphaR
            // 
            this.txbAlphaR.Location = new System.Drawing.Point(359, 111);
            this.txbAlphaR.Name = "txbAlphaR";
            this.txbAlphaR.ReadOnly = true;
            this.txbAlphaR.Size = new System.Drawing.Size(100, 22);
            this.txbAlphaR.TabIndex = 64;
            this.txbAlphaR.Text = "0.0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(319, 114);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(30, 17);
            this.label11.TabIndex = 63;
            this.label11.Text = "αR:";
            // 
            // cbxHeSoLamViec
            // 
            this.cbxHeSoLamViec.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxHeSoLamViec.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxHeSoLamViec.FormattingEnabled = true;
            this.cbxHeSoLamViec.Items.AddRange(new object[] {
            "0.9",
            "1.0",
            "1.1"});
            this.cbxHeSoLamViec.Location = new System.Drawing.Point(131, 107);
            this.cbxHeSoLamViec.Name = "cbxHeSoLamViec";
            this.cbxHeSoLamViec.Size = new System.Drawing.Size(121, 24);
            this.cbxHeSoLamViec.TabIndex = 62;
            this.cbxHeSoLamViec.SelectedIndexChanged += new System.EventHandler(this.cbxHeSoLamViec_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 111);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(115, 17);
            this.label9.TabIndex = 61;
            this.label9.Text = "Hệ số làm việc γ:";
            // 
            // txbEs
            // 
            this.txbEs.Location = new System.Drawing.Point(744, 67);
            this.txbEs.Name = "txbEs";
            this.txbEs.ReadOnly = true;
            this.txbEs.Size = new System.Drawing.Size(100, 22);
            this.txbEs.TabIndex = 60;
            this.txbEs.Text = "0.0";
            // 
            // txbEb
            // 
            this.txbEb.Location = new System.Drawing.Point(745, 22);
            this.txbEb.Name = "txbEb";
            this.txbEb.ReadOnly = true;
            this.txbEb.Size = new System.Drawing.Size(100, 22);
            this.txbEb.TabIndex = 59;
            this.txbEb.Text = "0.0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(676, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 17);
            this.label7.TabIndex = 58;
            this.label7.Text = "Es(Mpa):";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(676, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 17);
            this.label8.TabIndex = 57;
            this.label8.Text = "Eb(Mpa):";
            // 
            // txbRsc
            // 
            this.txbRsc.Location = new System.Drawing.Point(550, 67);
            this.txbRsc.Name = "txbRsc";
            this.txbRsc.ReadOnly = true;
            this.txbRsc.Size = new System.Drawing.Size(100, 22);
            this.txbRsc.TabIndex = 56;
            this.txbRsc.Text = "0.0";
            // 
            // txbRbt
            // 
            this.txbRbt.Location = new System.Drawing.Point(550, 22);
            this.txbRbt.Name = "txbRbt";
            this.txbRbt.ReadOnly = true;
            this.txbRbt.Size = new System.Drawing.Size(100, 22);
            this.txbRbt.TabIndex = 55;
            this.txbRbt.Text = "0.0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(469, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 17);
            this.label5.TabIndex = 54;
            this.label5.Text = "Rsc(Mpa):";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(471, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 17);
            this.label6.TabIndex = 53;
            this.label6.Text = "Rbt(Mpa):";
            // 
            // txbRs
            // 
            this.txbRs.Location = new System.Drawing.Point(359, 67);
            this.txbRs.Name = "txbRs";
            this.txbRs.ReadOnly = true;
            this.txbRs.Size = new System.Drawing.Size(100, 22);
            this.txbRs.TabIndex = 52;
            this.txbRs.Text = "0.0";
            // 
            // txbRb
            // 
            this.txbRb.Location = new System.Drawing.Point(360, 22);
            this.txbRb.Name = "txbRb";
            this.txbRb.ReadOnly = true;
            this.txbRb.Size = new System.Drawing.Size(100, 22);
            this.txbRb.TabIndex = 51;
            this.txbRb.Text = "0.0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(283, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 17);
            this.label3.TabIndex = 50;
            this.label3.Text = "Rs(Mpa):";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(282, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 17);
            this.label4.TabIndex = 49;
            this.label4.Text = "Rb(Mpa):";
            // 
            // cbxLoaiThep
            // 
            this.cbxLoaiThep.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxLoaiThep.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxLoaiThep.FormattingEnabled = true;
            this.cbxLoaiThep.Items.AddRange(new object[] {
            "AI",
            "AII",
            "AIII"});
            this.cbxLoaiThep.Location = new System.Drawing.Point(131, 66);
            this.cbxLoaiThep.Name = "cbxLoaiThep";
            this.cbxLoaiThep.Size = new System.Drawing.Size(121, 24);
            this.cbxLoaiThep.TabIndex = 48;
            this.cbxLoaiThep.SelectedIndexChanged += new System.EventHandler(this.cbxLoaiThep_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 17);
            this.label2.TabIndex = 47;
            this.label2.Text = "Loại thép:";
            // 
            // cbxLoaiBeTong
            // 
            this.cbxLoaiBeTong.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxLoaiBeTong.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxLoaiBeTong.FormattingEnabled = true;
            this.cbxLoaiBeTong.Items.AddRange(new object[] {
            "B12.5",
            "B15",
            "B20",
            "B25",
            "B30"});
            this.cbxLoaiBeTong.Location = new System.Drawing.Point(131, 21);
            this.cbxLoaiBeTong.Name = "cbxLoaiBeTong";
            this.cbxLoaiBeTong.Size = new System.Drawing.Size(121, 24);
            this.cbxLoaiBeTong.TabIndex = 46;
            this.cbxLoaiBeTong.SelectedIndexChanged += new System.EventHandler(this.cbxLoaiBeTong_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 17);
            this.label1.TabIndex = 45;
            this.label1.Text = "Loại bê tông:";
            // 
            // FormThongSoThepVaBetong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 208);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txbCeR);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txbAlphaR);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cbxHeSoLamViec);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txbEs);
            this.Controls.Add(this.txbEb);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txbRsc);
            this.Controls.Add(this.txbRbt);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txbRs);
            this.Controls.Add(this.txbRb);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbxLoaiThep);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbxLoaiBeTong);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormThongSoThepVaBetong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông Số Thép Và Bê Tông";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txbCeR;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txbAlphaR;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbxHeSoLamViec;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txbEs;
        private System.Windows.Forms.TextBox txbEb;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txbRsc;
        private System.Windows.Forms.TextBox txbRbt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txbRs;
        private System.Windows.Forms.TextBox txbRb;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxLoaiThep;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxLoaiBeTong;
        private System.Windows.Forms.Label label1;
    }
}