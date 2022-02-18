
namespace TinhToanSanTHXD
{
    partial class FormNhapFileAccess
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNhapFileAccess));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnEtabsOK = new System.Windows.Forms.Button();
            this.dgvEtabs = new System.Windows.Forms.DataGridView();
            this.cmsData = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiChonTatCaSan = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBoChonTatCaSan = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLoadData = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEtabs)).BeginInit();
            this.cmsData.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.Location = new System.Drawing.Point(676, 400);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(105, 27);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnEtabsOK
            // 
            this.btnEtabsOK.AutoSize = true;
            this.btnEtabsOK.Location = new System.Drawing.Point(565, 400);
            this.btnEtabsOK.Name = "btnEtabsOK";
            this.btnEtabsOK.Size = new System.Drawing.Size(105, 27);
            this.btnEtabsOK.TabIndex = 15;
            this.btnEtabsOK.Text = "OK";
            this.btnEtabsOK.UseVisualStyleBackColor = true;
            this.btnEtabsOK.Click += new System.EventHandler(this.btnEtabsOK_Click);
            // 
            // dgvEtabs
            // 
            this.dgvEtabs.AllowUserToAddRows = false;
            this.dgvEtabs.AllowUserToDeleteRows = false;
            this.dgvEtabs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEtabs.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dgvEtabs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEtabs.ContextMenuStrip = this.cmsData;
            this.dgvEtabs.Location = new System.Drawing.Point(11, 57);
            this.dgvEtabs.Name = "dgvEtabs";
            this.dgvEtabs.ReadOnly = true;
            this.dgvEtabs.RowHeadersWidth = 51;
            this.dgvEtabs.RowTemplate.Height = 24;
            this.dgvEtabs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEtabs.Size = new System.Drawing.Size(770, 324);
            this.dgvEtabs.TabIndex = 14;
            // 
            // cmsData
            // 
            this.cmsData.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsData.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiChonTatCaSan,
            this.tsmiBoChonTatCaSan});
            this.cmsData.Name = "cmsData";
            this.cmsData.Size = new System.Drawing.Size(223, 60);
            // 
            // tsmiChonTatCaSan
            // 
            this.tsmiChonTatCaSan.Name = "tsmiChonTatCaSan";
            this.tsmiChonTatCaSan.Size = new System.Drawing.Size(222, 28);
            this.tsmiChonTatCaSan.Text = "Chọn tất cả sàn";
            this.tsmiChonTatCaSan.Click += new System.EventHandler(this.tsmiChonTatCaSan_Click);
            // 
            // tsmiBoChonTatCaSan
            // 
            this.tsmiBoChonTatCaSan.Name = "tsmiBoChonTatCaSan";
            this.tsmiBoChonTatCaSan.Size = new System.Drawing.Size(222, 28);
            this.tsmiBoChonTatCaSan.Text = "Bỏ chọn tất cả sàn";
            this.tsmiBoChonTatCaSan.Click += new System.EventHandler(this.tsmiBoChonTatCaSan_Click);
            // 
            // btnLoadData
            // 
            this.btnLoadData.AutoSize = true;
            this.btnLoadData.Location = new System.Drawing.Point(576, 24);
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(205, 27);
            this.btnLoadData.TabIndex = 17;
            this.btnLoadData.Text = "Load Data";
            this.btnLoadData.UseVisualStyleBackColor = true;
            this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
            // 
            // FormNhapFileAccess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 431);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnEtabsOK);
            this.Controls.Add(this.dgvEtabs);
            this.Controls.Add(this.btnLoadData);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormNhapFileAccess";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nhập Dữ Liệu Từ Etabs";
            ((System.ComponentModel.ISupportInitialize)(this.dgvEtabs)).EndInit();
            this.cmsData.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnEtabsOK;
        private System.Windows.Forms.DataGridView dgvEtabs;
        private System.Windows.Forms.Button btnLoadData;
        private System.Windows.Forms.ContextMenuStrip cmsData;
        private System.Windows.Forms.ToolStripMenuItem tsmiChonTatCaSan;
        private System.Windows.Forms.ToolStripMenuItem tsmiBoChonTatCaSan;
    }
}