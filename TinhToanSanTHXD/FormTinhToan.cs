using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using Exception = System.Exception;

namespace TinhToanSanTHXD
{
    public partial class FormTinhToan : Form
    {
        public FormTinhToan()
        {
            InitializeComponent();
            dgvNhieuSan.DataSource = Global.dataTableChonNhieuSan.DefaultView;
            dgvDrawGrid.DataSource = Global.dataTableDrawGrid.DefaultView;
            dgvDrawMatBang.DataSource = Global.dataTableDrawMatBang.DefaultView;
            dgvDrawColumn.DataSource = Global.dataTableDrawColumn.DefaultView;
            dgvDrawBeam.DataSource = Global.dataTableDrawBeam.DefaultView;

            //cbbChonTang.DataSource = Global.dataTableDrawGrid.Columns[1].DefaultValue;
        }
        List<San> danhSachSan = new List<San>();
        List<San> danhSachSanBanKeLoaiDam = new List<San>();
        List<San> danhSachSanBanKe4Canh = new List<San>();

        double[,] arrBangTraBanKe4Canh;

        private void tsmiNhapFileAccess_Click(object sender, EventArgs e)
        {
            FormNhapFileAccess formFileAccess = new FormNhapFileAccess();
            formFileAccess.Show();
        }

        public static double[,] readFromFile(string path, int sohang, int socot)
        {

            StreamReader sr = new StreamReader(path);
            string s = "";
            double[,] a = new double[sohang, socot];
            int i = 0;

            while ((s = sr.ReadLine()) != null)
            {
                string[] content = s.Trim().Split('\t');

                for (int j = 0; j < content.Length; j++)
                {
                    a[i, j] = double.Parse(content[j]);
                }

                i++;
            }
            return a;
        }

        private void tsmiDefineThongSoThepVaBetong_Click(object sender, EventArgs e)
        {
            FormThongSoThepVaBetong formThongSoThepVaBetong = new FormThongSoThepVaBetong();
            formThongSoThepVaBetong.Show();
        }

        private void tsbPhanLoaiSan_Click(object sender, EventArgs e)
        {
            danhSachSan.Clear();
            danhSachSanBanKe4Canh.Clear();
            danhSachSanBanKeLoaiDam.Clear();

            dgvBanLoaiDam.Rows.Clear();
            dgvBanKe4Canh.Rows.Clear();


            for (int i = 0; i < dgvNhieuSan.Rows.Count; i++)
            {
                if (dgvNhieuSan.Rows[i].Cells[3].Value.ToString() == "0"
                    || dgvNhieuSan.Rows[i].Cells[3].Value.ToString() == "0")
                {
                    continue;
                }

                danhSachSan.Add(new San()
                {
                    Tang = dgvNhieuSan.Rows[i].Cells[0].Value + "",
                    TenSan = dgvNhieuSan.Rows[i].Cells[1].Value + "",
                    UniqueName = int.Parse(dgvNhieuSan.Rows[i].Cells[2].Value + ""),
                    L1 = double.Parse(dgvNhieuSan.Rows[i].Cells[3].Value + ""),
                    L2 = double.Parse(dgvNhieuSan.Rows[i].Cells[4].Value + ""),
                    LoaiBeTong = dgvNhieuSan.Rows[i].Cells[5].Value + "",
                    ChieuDayH = double.Parse(dgvNhieuSan.Rows[i].Cells[6].Value + ""),
                    TinhTai = double.Parse(dgvNhieuSan.Rows[i].Cells[7].Value + ""),
                    HoatTai = double.Parse(dgvNhieuSan.Rows[i].Cells[8].Value + ""),
                    LopBTBV = double.Parse(dgvNhieuSan.Rows[i].Cells[9].Value + ""),
                });
            }
            int x = 0;
            int y = 0;
            foreach (San sans in danhSachSan)
            {
                if (sans.LoaiSan.Equals("Bản kê 4 cạnh"))
                {
                    danhSachSanBanKe4Canh.Add(sans);
                    dgvBanKe4Canh.Rows.Add(sans.Tang, sans.TenSan,
                        sans.UniqueName, sans.LoaiBeTong, sans.ChieuDayH,
                        string.Format("{0:f3}", sans.TySoL1L2), string.Format("{0:f3}", sans.TinhTai),
                        string.Format("{0:f3}", sans.HoatTai), string.Format("{0:f3}", sans.TaiPhanBo),
                        sans.LopBTBV, sans.ChieuDayH0, "1");
                    dgvBanKe4Canh.Rows[x].Cells[12].Value = "Ø" + 6;
                    x++;
                }
                else
                {
                    danhSachSanBanKeLoaiDam.Add(sans);
                    dgvBanLoaiDam.Rows.Add(sans.Tang, sans.TenSan,
                        sans.UniqueName, sans.LoaiBeTong, sans.ChieuDayH,
                        string.Format("{0:f3}", sans.TySoL1L2), string.Format("{0:f3}", sans.TinhTai),
                        string.Format("{0:f3}", sans.HoatTai), string.Format("{0:f3}", sans.TaiPhanBo),
                        sans.LopBTBV, sans.ChieuDayH0, "1");
                    dgvBanLoaiDam.Rows[y].Cells[12].Value = "Ø" + 6;
                    y++;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(dgvNhieuSan.Rows[0].Cells[3].Value.ToString());
            for (int i = 0; i < 5; i++)
            {
                if (dgvNhieuSan.Rows[i].Cells[3].Value.ToString() == "0"
                   || dgvNhieuSan.Rows[i].Cells[3].Value.ToString() == "0")
                {
                    continue;
                }
            }

        }

        private void tsbTinhToanTatCa_Click(object sender, EventArgs e)
        {
            if (Global.AlphaR == 0)
            {
                MessageBox.Show("Bạn chưa chọn thông số vật liệu."
                    , "Cảnh báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }
            arrBangTraBanKe4Canh = readFromFile(@"Bang_Tra_Ban_Ke_4_Canh.txt", 21, 31);
            tspbTinhToan.Maximum = dgvBanKe4Canh.Rows.Count + dgvBanLoaiDam.Rows.Count;
            tspbTinhToan.Value = 0;
            tspbTinhToan.Visible = true;
            tsslTinhToan.Visible = true;
            ssMain.Visible = true;
            ssMain.Update();
            ssMain.Refresh();
            // Tính toán bản kê 4 cạnh
            for (int x = 0; x < dgvBanKe4Canh.Rows.Count; x++)
            {
                double chieuDayH = double.Parse(dgvBanKe4Canh.Rows[x].Cells[4].Value + "");
                double tySoL1L2 = double.Parse(dgvBanKe4Canh.Rows[x].Cells[5].Value + "");
                double tinhTai = double.Parse(dgvBanKe4Canh.Rows[x].Cells[6].Value + "");
                double hoatTai = double.Parse(dgvBanKe4Canh.Rows[x].Cells[7].Value + "");
                double taiPhanBo = double.Parse(dgvBanKe4Canh.Rows[x].Cells[8].Value + "");
                double chieuDayH0 = double.Parse(dgvBanKe4Canh.Rows[x].Cells[10].Value + "");
                #region Chọn loại sơ đồ
                double alpha1 = 0, alpha2 = 0, beta1 = 0, beta2 = 0;
                switch (dgvBanKe4Canh.Rows[x].Cells[11].Value + "")
                {
                    case "9":
                        for (int a = 0; a <= arrBangTraBanKe4Canh.GetLength(0) - 1; a++)
                            if (arrBangTraBanKe4Canh[a, 0] == tySoL1L2)
                            {
                                alpha1 = Math.Round(arrBangTraBanKe4Canh[a, 1], 3);
                                alpha2 = Math.Round(arrBangTraBanKe4Canh[a, 2], 3);
                                beta1 = Math.Round(arrBangTraBanKe4Canh[a, 3], 3);
                                beta2 = Math.Round(arrBangTraBanKe4Canh[a, 4], 3);
                            }
                        for (int i = 0; i < arrBangTraBanKe4Canh.GetLength(0) - 1; i++)
                            if (arrBangTraBanKe4Canh[i, 0] < tySoL1L2)
                                if (arrBangTraBanKe4Canh[i + 1, 0] > tySoL1L2)
                                {
                                    alpha1 = Math.Round((arrBangTraBanKe4Canh[i + 1, 1]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 1]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);

                                    alpha2 = Math.Round((arrBangTraBanKe4Canh[i + 1, 2]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 2]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);

                                    beta1 = Math.Round((arrBangTraBanKe4Canh[i + 1, 3]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 3]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);

                                    beta2 = Math.Round((arrBangTraBanKe4Canh[i + 1, 4]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 4]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);
                                }

                        break;
                    case "8":
                        for (int a = 0; a <= arrBangTraBanKe4Canh.GetLength(0) - 1; a++)
                            if (arrBangTraBanKe4Canh[a, 0] == tySoL1L2)
                            {
                                alpha1 = Math.Round(arrBangTraBanKe4Canh[a, 5], 3);
                                alpha2 = Math.Round(arrBangTraBanKe4Canh[a, 6], 3);
                                beta1 = Math.Round(arrBangTraBanKe4Canh[a, 7], 3);
                                beta2 = Math.Round(arrBangTraBanKe4Canh[a, 8], 3);
                            }
                        for (int i = 0; i < arrBangTraBanKe4Canh.GetLength(0) - 1; i++)
                            if (arrBangTraBanKe4Canh[i, 0] < tySoL1L2)
                                if (arrBangTraBanKe4Canh[i + 1, 0] > tySoL1L2)
                                {
                                    alpha1 = Math.Round((arrBangTraBanKe4Canh[i + 1, 5]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 5]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);

                                    alpha2 = Math.Round((arrBangTraBanKe4Canh[i + 1, 6]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 6]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);

                                    beta1 = Math.Round((arrBangTraBanKe4Canh[i + 1, 7]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 7]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);

                                    beta2 = Math.Round((arrBangTraBanKe4Canh[i + 1, 8]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 8]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);
                                }

                        break;
                    case "7":
                        for (int a = 0; a <= arrBangTraBanKe4Canh.GetLength(0) - 1; a++)
                            if (arrBangTraBanKe4Canh[a, 0] == tySoL1L2)
                            {
                                alpha1 = Math.Round(arrBangTraBanKe4Canh[a, 9], 3);
                                alpha2 = Math.Round(arrBangTraBanKe4Canh[a, 10], 3);
                                beta1 = Math.Round(arrBangTraBanKe4Canh[a, 11], 3);
                                beta2 = Math.Round(arrBangTraBanKe4Canh[a, 12], 3);
                            }
                        for (int i = 0; i < arrBangTraBanKe4Canh.GetLength(0) - 1; i++)
                            if (arrBangTraBanKe4Canh[i, 0] < tySoL1L2)
                                if (arrBangTraBanKe4Canh[i + 1, 0] > tySoL1L2)
                                {
                                    alpha1 = Math.Round((arrBangTraBanKe4Canh[i + 1, 9]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 9]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);

                                    alpha2 = Math.Round((arrBangTraBanKe4Canh[i + 1, 10]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 10]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);

                                    beta1 = Math.Round((arrBangTraBanKe4Canh[i + 1, 11]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 11]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);

                                    beta2 = Math.Round((arrBangTraBanKe4Canh[i + 1, 12]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 12]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);
                                }

                        break;
                    case "6":
                        for (int a = 0; a <= arrBangTraBanKe4Canh.GetLength(0) - 1; a++)
                            if (arrBangTraBanKe4Canh[a, 0] == tySoL1L2)
                            {
                                alpha1 = Math.Round(arrBangTraBanKe4Canh[a, 13], 3);
                                alpha2 = Math.Round(arrBangTraBanKe4Canh[a, 14], 3);
                                beta1 = Math.Round(arrBangTraBanKe4Canh[a, 15], 3);
                                beta2 = Math.Round(arrBangTraBanKe4Canh[a, 16], 3);
                            }
                        for (int i = 0; i < arrBangTraBanKe4Canh.GetLength(0) - 1; i++)
                            if (arrBangTraBanKe4Canh[i, 0] < tySoL1L2)
                                if (arrBangTraBanKe4Canh[i + 1, 0] > tySoL1L2)
                                {
                                    alpha1 = Math.Round((arrBangTraBanKe4Canh[i + 1, 13]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 13]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);

                                    alpha2 = Math.Round((arrBangTraBanKe4Canh[i + 1, 14]
                                          * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                          + arrBangTraBanKe4Canh[i, 14]
                                          * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                          / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);

                                    beta1 = Math.Round((arrBangTraBanKe4Canh[i + 1, 15]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 15]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);

                                    beta2 = Math.Round((arrBangTraBanKe4Canh[i + 1, 16]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 6])
                                         + arrBangTraBanKe4Canh[i, 16]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);
                                }

                        break;
                    case "5":
                        for (int a = 0; a <= arrBangTraBanKe4Canh.GetLength(0) - 1; a++)
                            if (arrBangTraBanKe4Canh[a, 0] == tySoL1L2)
                            {
                                alpha1 = Math.Round(arrBangTraBanKe4Canh[a, 17], 3);
                                alpha2 = Math.Round(arrBangTraBanKe4Canh[a, 18], 3);
                                beta1 = Math.Round(arrBangTraBanKe4Canh[a, 19], 3);
                            }
                        for (int i = 0; i < arrBangTraBanKe4Canh.GetLength(0) - 1; i++)
                            if (arrBangTraBanKe4Canh[i, 0] < tySoL1L2)
                                if (arrBangTraBanKe4Canh[i + 1, 0] > tySoL1L2)
                                {
                                    alpha1 = Math.Round((arrBangTraBanKe4Canh[i + 1, 17]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 17]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);

                                    alpha2 = Math.Round((arrBangTraBanKe4Canh[i + 1, 18]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 18]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);

                                    beta1 = Math.Round((arrBangTraBanKe4Canh[i + 1, 19]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 19]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);
                                }
                        break;
                    case "4":
                        for (int a = 0; a <= arrBangTraBanKe4Canh.GetLength(0) - 1; a++)
                            if (arrBangTraBanKe4Canh[a, 0] == tySoL1L2)
                            {
                                alpha1 = Math.Round(arrBangTraBanKe4Canh[a, 20], 3);
                                alpha2 = Math.Round(arrBangTraBanKe4Canh[a, 21], 3);
                                beta1 = Math.Round(arrBangTraBanKe4Canh[a, 22], 3);
                            }
                        for (int i = 0; i < arrBangTraBanKe4Canh.GetLength(0) - 1; i++)
                            if (arrBangTraBanKe4Canh[i, 0] < tySoL1L2)
                                if (arrBangTraBanKe4Canh[i + 1, 0] > tySoL1L2)
                                {
                                    alpha1 = Math.Round((arrBangTraBanKe4Canh[i + 1, 20]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 20]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);

                                    alpha2 = Math.Round((arrBangTraBanKe4Canh[i + 1, 21]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 21]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);

                                    beta1 = Math.Round((arrBangTraBanKe4Canh[i + 1, 22]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 22]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);
                                }
                        break;
                    case "3":
                        for (int a = 0; a <= arrBangTraBanKe4Canh.GetLength(0) - 1; a++)
                            if (arrBangTraBanKe4Canh[a, 0] == tySoL1L2)
                            {
                                alpha1 = Math.Round(arrBangTraBanKe4Canh[a, 23], 3);
                                alpha2 = Math.Round(arrBangTraBanKe4Canh[a, 24], 3);
                                beta2 = Math.Round(arrBangTraBanKe4Canh[a, 25], 3);
                            }
                        for (int i = 0; i < arrBangTraBanKe4Canh.GetLength(0) - 1; i++)
                            if (arrBangTraBanKe4Canh[i, 0] < tySoL1L2)
                                if (arrBangTraBanKe4Canh[i + 1, 0] > tySoL1L2)
                                {
                                    alpha1 = Math.Round((arrBangTraBanKe4Canh[i + 1, 23]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 23]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);

                                    alpha2 = Math.Round((arrBangTraBanKe4Canh[i + 1, 24]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 24]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);

                                    beta2 = Math.Round((arrBangTraBanKe4Canh[i + 1, 25]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 25]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);
                                }
                        break;
                    case "2":
                        for (int a = 0; a <= arrBangTraBanKe4Canh.GetLength(0) - 1; a++)
                            if (arrBangTraBanKe4Canh[a, 0] == tySoL1L2)
                            {
                                alpha1 = Math.Round(arrBangTraBanKe4Canh[a, 26], 3);
                                alpha2 = Math.Round(arrBangTraBanKe4Canh[a, 27], 3);
                                beta1 = Math.Round(arrBangTraBanKe4Canh[a, 28], 3);
                            }
                        for (int i = 0; i < arrBangTraBanKe4Canh.GetLength(0) - 1; i++)
                            if (arrBangTraBanKe4Canh[i, 0] < tySoL1L2)
                                if (arrBangTraBanKe4Canh[i + 1, 0] > tySoL1L2)
                                {
                                    alpha1 = Math.Round((arrBangTraBanKe4Canh[i + 1, 26]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 26]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);

                                    alpha2 = Math.Round((arrBangTraBanKe4Canh[i + 1, 27]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 27]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);

                                    beta1 = Math.Round((arrBangTraBanKe4Canh[i + 1, 28]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 28]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);
                                }
                        break;
                    case "1":
                        for (int a = 0; a <= arrBangTraBanKe4Canh.GetLength(0) - 1; a++)
                            if (arrBangTraBanKe4Canh[a, 0] == tySoL1L2)
                            {
                                alpha1 = Math.Round(arrBangTraBanKe4Canh[a, 29], 3);
                                alpha2 = Math.Round(arrBangTraBanKe4Canh[a, 30], 3);
                            }
                        for (int i = 0; i < arrBangTraBanKe4Canh.GetLength(0) - 1; i++)
                            if (arrBangTraBanKe4Canh[i, 0] < tySoL1L2)
                                if (arrBangTraBanKe4Canh[i + 1, 0] > tySoL1L2)
                                {
                                    alpha1 = Math.Round((arrBangTraBanKe4Canh[i + 1, 29]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 29]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);

                                    alpha2 = Math.Round((arrBangTraBanKe4Canh[i + 1, 30]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 30]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);
                                }
                        break;
                }
                #endregion

                #region Tính Moment
                double M1, M2, MI, MII;
                double L1 = danhSachSanBanKe4Canh[x].L1;
                double L2 = danhSachSanBanKe4Canh[x].L2;
                // sửa alpha beta ở đây
                M1 = alpha1 * taiPhanBo * L1 * L2;
                M2 = alpha2 * taiPhanBo * L1 * L2;
                MI = -beta1 * taiPhanBo * L1 * L2;
                MII = -beta2 * taiPhanBo * L1 * L2;

                dgvBanKe4Canh.Rows[x].Cells[21].Value = string.Format("{0:f3}", M1);
                dgvBanKe4Canh.Rows[x].Cells[22].Value = string.Format("{0:f3}", M2);
                dgvBanKe4Canh.Rows[x].Cells[23].Value = string.Format("{0:f3}", MI);
                dgvBanKe4Canh.Rows[x].Cells[24].Value = string.Format("{0:f3}", MII);
                #endregion

                #region Tính am
                double amM1, amM2, amMI, amMII;

                amM1 = (M1 * Math.Pow(10, 6)) /
                    (danhSachSanBanKe4Canh[x].Rb
                    * danhSachSanBanKe4Canh[x].ChieuRongDaiBan
                    * Math.Pow(chieuDayH0, 2));
                amM2 = (M2 * Math.Pow(10, 6)) /
                    (danhSachSanBanKe4Canh[x].Rb
                    * danhSachSanBanKe4Canh[x].ChieuRongDaiBan
                    * Math.Pow(chieuDayH0, 2));
                amMI = (MI * Math.Pow(10, 6)) /
                    (danhSachSanBanKe4Canh[x].Rb
                    * danhSachSanBanKe4Canh[x].ChieuRongDaiBan
                    * Math.Pow(chieuDayH0, 2));
                amMII = (MII * Math.Pow(10, 6)) /
                    (danhSachSanBanKe4Canh[x].Rb
                    * danhSachSanBanKe4Canh[x].ChieuRongDaiBan
                    * Math.Pow(chieuDayH0, 2));

                #endregion

                #region Tính diện tích cốt thép
                double SiM1, SiM2, SiMI, SiMII;
                double AsM1, AsM2, AsMI, AsMII;
                if (amM1 < Global.AlphaR)
                {
                    SiM1 = Math.Abs(0.5 * (1 + Math.Sqrt(1 - 2 * amM1)));
                    AsM1 = Math.Abs(M1 * Math.Pow(10, 6)
                        / (Global.Rs * SiM1 * chieuDayH0));
                }
                else
                {
                    MessageBox.Show("Hệ số sàn " + dgvBanLoaiDam.Rows[x].Cells[2].Value.ToString() +
                    " không thỏa điều kiện");
                    return;
                }
                if (amM2 < Global.AlphaR)
                {
                    SiM2 = Math.Abs(0.5 * (1 + Math.Sqrt(1 - 2 * amM2)));
                    AsM2 = Math.Abs(M2 * Math.Pow(10, 6))
                        / (Global.Rs * SiM2 * chieuDayH0);
                }
                else
                {
                    MessageBox.Show("Hệ số sàn " + dgvBanLoaiDam.Rows[x].Cells[2].Value.ToString() +
                    " không thỏa điều kiện");
                    return;
                }
                if (amMI < Global.AlphaR)
                {
                    SiMI = Math.Abs(0.5 * (1 + Math.Sqrt(1 - 2 * amMI)));
                    AsMI = Math.Abs(MI * Math.Pow(10, 6))
                        / (Global.Rs * SiMI * chieuDayH0);
                }
                else
                {
                    MessageBox.Show("Hệ số sàn " + dgvBanLoaiDam.Rows[x].Cells[2].Value.ToString() +
                    " không thỏa điều kiện");
                    return;
                }
                if (amMII < Global.AlphaR)
                {
                    SiMII = Math.Abs(0.5 * (1 + Math.Sqrt(1 - 2 * amM2)));
                    AsMII = Math.Abs((MII * Math.Pow(10, 6))
                        / (Global.Rs * SiMII * chieuDayH0));
                }
                else
                {
                    MessageBox.Show("Hệ số sàn " + dgvBanLoaiDam.Rows[x].Cells[2].Value.ToString() +
                    " không thỏa điều kiện");
                    return;
                }
                dgvBanKe4Canh.Rows[x].Cells[25].Value = string.Format("{0:f3}", AsM1);
                dgvBanKe4Canh.Rows[x].Cells[26].Value = string.Format("{0:f3}", AsM2);
                dgvBanKe4Canh.Rows[x].Cells[27].Value = string.Format("{0:f3}", AsMI);
                dgvBanKe4Canh.Rows[x].Cells[28].Value = string.Format("{0:f3}", AsMII);
                #endregion

                #region Tính hàm lượng cốt thép
                double hamLuongCotThepM1, hamLuongCotThepM2, hamLuongCotThepMI, hamLuongCotThepMII;
                hamLuongCotThepM1 = AsM1
                    / (danhSachSanBanKe4Canh[0].ChieuRongDaiBan * chieuDayH0) * 100;
                hamLuongCotThepM2 = AsM2
                   / (danhSachSanBanKe4Canh[0].ChieuRongDaiBan * chieuDayH0) * 100;
                hamLuongCotThepMI = AsMI
                   / (danhSachSanBanKe4Canh[0].ChieuRongDaiBan * chieuDayH0) * 100;
                hamLuongCotThepMII = (AsMII
                   / (danhSachSanBanKe4Canh[0].ChieuRongDaiBan * chieuDayH0)) * 100;
                dgvBanKe4Canh.Rows[x].Cells[17].Value = string.Format("{0:f3}", hamLuongCotThepM1);
                dgvBanKe4Canh.Rows[x].Cells[18].Value = string.Format("{0:f3}", hamLuongCotThepM2);
                dgvBanKe4Canh.Rows[x].Cells[19].Value = string.Format("{0:f3}", hamLuongCotThepMI);
                dgvBanKe4Canh.Rows[x].Cells[20].Value = string.Format("{0:f3}", hamLuongCotThepMII);
                #endregion

                #region Tính khoảng cách cốt thép


                string stLoaiThep = dgvBanKe4Canh.Rows[x].Cells[12].Value.ToString();
                int loaiThep = int.Parse(stLoaiThep.Trim('Ø'));
                double dienTichThanhThep = loaiThep * loaiThep * Math.PI / 4;
                double khoangCachThepM1, khoangCachThepM2, khoangCachThepMI, khoangCachThepMII;

                khoangCachThepM1 = ((int)(((danhSachSanBanKe4Canh[x].ChieuRongDaiBan
                * dienTichThanhThep) / AsM1) / 10) * 10);
                khoangCachThepM2 = ((int)(((danhSachSanBanKe4Canh[x].ChieuRongDaiBan
                * dienTichThanhThep) / AsM2) / 10) * 10);
                khoangCachThepMI = ((int)(((danhSachSanBanKe4Canh[x].ChieuRongDaiBan
                * dienTichThanhThep) / AsMI) / 10) * 10);
                khoangCachThepMII = ((int)(((danhSachSanBanKe4Canh[x].ChieuRongDaiBan
                * dienTichThanhThep) / AsMII) / 10) * 10);

                dgvBanKe4Canh.Rows[x].Cells[13].Value = "Ø" + loaiThep + "a" + khoangCachThepM1;
                dgvBanKe4Canh.Rows[x].Cells[14].Value = "Ø" + loaiThep + "a" + khoangCachThepM2;
                if (dgvBanKe4Canh.Rows[x].Cells[27].Value.ToString().Equals("0.000"))
                {
                    dgvBanKe4Canh.Rows[x].Cells[15].Value = "x";
                }
                else
                {
                    dgvBanKe4Canh.Rows[x].Cells[15].Value = "Ø" + loaiThep + "a" + khoangCachThepMI;
                }

                if (dgvBanKe4Canh.Rows[x].Cells[28].Value.ToString().Equals("0.000"))
                {
                    dgvBanKe4Canh.Rows[x].Cells[16].Value = "x";
                }
                else
                {
                    dgvBanKe4Canh.Rows[x].Cells[16].Value = "Ø" + loaiThep + "a" + khoangCachThepMII;
                }

                #endregion

                tspbTinhToan.Value += 1;
            }


            //--------------------------------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------------------------------
            // Tính toán bản kê loại dầm



            for (int x = 0; x < dgvBanLoaiDam.Rows.Count; x++)
            {
                double chieuDayH = double.Parse(dgvBanLoaiDam.Rows[x].Cells[4].Value + "");
                double tySoL1L2 = double.Parse(dgvBanLoaiDam.Rows[x].Cells[5].Value + "");
                double tinhTai = double.Parse(dgvBanLoaiDam.Rows[x].Cells[6].Value + "");
                double hoatTai = double.Parse(dgvBanLoaiDam.Rows[x].Cells[7].Value + "");
                double taiPhanBo = double.Parse(dgvBanLoaiDam.Rows[x].Cells[8].Value + "");
                double chieuDayH0 = double.Parse(dgvBanLoaiDam.Rows[x].Cells[10].Value + "");
                double L1 = danhSachSanBanKeLoaiDam[x].L1;
                double L2 = danhSachSanBanKeLoaiDam[x].L2;
                double Mgoi = 0, Mnhip = 0;
                string loaiSoDo = dgvBanLoaiDam.Rows[x].Cells[11].Value + "";

                #region Chọn loại sơ đồ
                switch (dgvBanLoaiDam.Rows[x].Cells[11].Value + "")
                {
                    case "1":
                        Mgoi = (-1 / 8.0) * taiPhanBo * Math.Pow(L1, 2);
                        Mnhip = (9 / 128.0) * (tinhTai + (hoatTai / 2)) * Math.Pow(L1, 2)
                            + (1 / 8.0) * (hoatTai / 2) * Math.Pow(L1, 2);
                        break;
                    case "2":
                        Mgoi = (-1 / 12.0) * taiPhanBo * Math.Pow(L1, 2);
                        Mnhip = (1 / 12.0) * taiPhanBo * Math.Pow(L1, 2);
                        break;
                }

                dgvBanLoaiDam.Rows[x].Cells[17].Value = string.Format("{0:f3}", Mgoi);
                dgvBanLoaiDam.Rows[x].Cells[18].Value = string.Format("{0:f3}", Mnhip);
                #endregion

                #region Tính diện tích cốt thép
                double alphaGoi, alphaNhip;

                alphaGoi = Math.Abs(Mgoi * Math.Pow(10, 6))
                    / (danhSachSanBanKeLoaiDam[x].Rb
                    * danhSachSanBanKe4Canh[x].ChieuRongDaiBan * Math.Pow(chieuDayH0, 2));
                alphaNhip = Math.Abs(Mnhip * Math.Pow(10, 6))
                    / (danhSachSanBanKeLoaiDam[x].Rb
                    * danhSachSanBanKe4Canh[x].ChieuRongDaiBan * Math.Pow(chieuDayH0, 2));

                // Tính diện tích cốt thép
                double cosiGoi, cosiNhip = 0, Asgoi = 0, Asnhip = 0;
                if (alphaGoi <= Global.AlphaR)
                {
                    cosiGoi = 0.5 * (1 + Math.Sqrt(1 - 2 * (alphaGoi)));
                    Asgoi = Math.Abs(Mgoi * Math.Pow(10, 6)) / (Global.Rs * cosiGoi * chieuDayH0);
                }
                else
                {
                    MessageBox.Show("Hệ số sàn " + dgvBanLoaiDam.Rows[x].Cells[2].Value.ToString() +
                        " không thỏa điều kiện");
                    return;
                }
                if (alphaNhip <= Global.AlphaR)
                {
                    cosiNhip = 0.5 * (1 + Math.Sqrt(1 - 2 * (alphaNhip)));
                    Asnhip = (Math.Abs(Mnhip * Math.Pow(10, 6)) / (Global.Rs * cosiNhip * chieuDayH0));
                }
                else
                {
                    MessageBox.Show("Hệ số sàn " + dgvBanLoaiDam.Rows[x].Cells[2].Value.ToString() +
                        " không thỏa điều kiện");
                    return;
                }

                dgvBanLoaiDam.Rows[x].Cells[19].Value = Math.Round(Asgoi, 3);
                dgvBanLoaiDam.Rows[x].Cells[20].Value = Math.Round(Asnhip, 3);

                double hamLuongCotThepGoi, hamLuongCotThepNhip;
                hamLuongCotThepGoi = (Asgoi
                    / (danhSachSanBanKeLoaiDam[x].ChieuRongDaiBan * chieuDayH0) * 100);
                hamLuongCotThepNhip = (Asnhip
                / (danhSachSanBanKeLoaiDam[x].ChieuRongDaiBan * chieuDayH0) * 100);

                dgvBanLoaiDam.Rows[x].Cells[15].Value = string.Format("{0:f3}", hamLuongCotThepGoi);
                dgvBanLoaiDam.Rows[x].Cells[16].Value = string.Format("{0:f3}", hamLuongCotThepNhip);
                #endregion

                #region Tính khoảng cách cốt thép

                string stLoaiThep = dgvBanLoaiDam.Rows[x].Cells[12].Value.ToString();
                int loaiThep = int.Parse(stLoaiThep.Trim('Ø'));
                double dienTichThanhThep = loaiThep * loaiThep * Math.PI / 4;
                double khoangCachThepMgoi, khoangCachThepMnhip;

                khoangCachThepMgoi = ((int)(((danhSachSanBanKeLoaiDam[x].ChieuRongDaiBan
                * dienTichThanhThep) / Asgoi) / 10) * 10);
                khoangCachThepMnhip = ((int)(((danhSachSanBanKeLoaiDam[x].ChieuRongDaiBan
                * dienTichThanhThep) / Asnhip) / 10) * 10);

                dgvBanLoaiDam.Rows[x].Cells[13].Value = "Ø" + loaiThep + "a" + khoangCachThepMgoi;
                dgvBanLoaiDam.Rows[x].Cells[14].Value = "Ø" + loaiThep + "a" + khoangCachThepMnhip;
                #endregion

                tspbTinhToan.Value += 1;
            }
            MessageBox.Show("Quá trình tính toán được thực hiện thành công!", ""
                , MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            tspbTinhToan.Visible = false;
            tsslTinhToan.Visible = false;
            ssMain.Visible = false;
        }

        private void tsmiFileThoat_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Bạn có chắc muốn thoát chương trình?"
                , "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
                this.Close();
        }

        private void tsbXoaDuLieu_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Bạn có chắc muốn xóa dữ liệu bảng này?"
                , "Xóa", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (res == DialogResult.OK)
            {
                if (tcMain.SelectedTab == tp4Canh)
                {
                    dgvBanKe4Canh.Rows.Clear();
                    danhSachSanBanKe4Canh.Clear();
                }
                else if (tcMain.SelectedTab == tpLoaiDam)
                {
                    dgvBanLoaiDam.Rows.Clear();
                    danhSachSanBanKeLoaiDam.Clear();
                }
                else if (tcMain.SelectedTab == tpDanhSachSan)
                {
                    dgvNhieuSan.Rows.Clear();
                    danhSachSan.Clear();
                }
            }
        }

        private void tsmiXoaTatCa_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Bạn có muốn xóa tất cả dữ liệu hiện có?"
                , "Xóa tất cả", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (res == DialogResult.Yes)
            {
                dgvBanKe4Canh.Rows.Clear();
                danhSachSanBanKe4Canh.Clear();
                dgvBanLoaiDam.Rows.Clear();
                danhSachSanBanKeLoaiDam.Clear();
                dgvNhieuSan.Rows.Clear();
                danhSachSan.Clear();
            }
        }

        private void tsmiTinhToan4Canh_Click(object sender, EventArgs e)
        {
            if (Global.AlphaR == 0)
            {
                MessageBox.Show("Bạn chưa chọn thông số vật liệu."
                    , "Cảnh báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }
            arrBangTraBanKe4Canh = readFromFile(@"Bang_Tra_Ban_Ke_4_Canh.txt", 21, 31);
            tspbTinhToan.Maximum = dgvBanKe4Canh.Rows.Count;
            tspbTinhToan.Value = 0;
            ssMain.Visible = true;
            tspbTinhToan.Visible = true;
            tsslTinhToan.Visible = true;

            ssMain.Update();
            ssMain.Refresh();
            // Tính toán bản kê 4 cạnh
            for (int x = 0; x < dgvBanKe4Canh.Rows.Count; x++)
            {
                double chieuDayH = double.Parse(dgvBanKe4Canh.Rows[x].Cells[4].Value + "");
                double tySoL1L2 = double.Parse(dgvBanKe4Canh.Rows[x].Cells[5].Value + "");
                double tinhTai = double.Parse(dgvBanKe4Canh.Rows[x].Cells[6].Value + "");
                double hoatTai = double.Parse(dgvBanKe4Canh.Rows[x].Cells[7].Value + "");
                double taiPhanBo = double.Parse(dgvBanKe4Canh.Rows[x].Cells[8].Value + "");
                double chieuDayH0 = double.Parse(dgvBanKe4Canh.Rows[x].Cells[10].Value + "");
                #region Chọn loại sơ đồ
                double alpha1 = 0, alpha2 = 0, beta1 = 0, beta2 = 0;
                switch (dgvBanKe4Canh.Rows[x].Cells[11].Value + "")
                {
                    case "9":
                        for (int a = 0; a <= arrBangTraBanKe4Canh.GetLength(0) - 1; a++)
                            if (arrBangTraBanKe4Canh[a, 0] == tySoL1L2)
                            {
                                alpha1 = Math.Round(arrBangTraBanKe4Canh[a, 1], 3);
                                alpha2 = Math.Round(arrBangTraBanKe4Canh[a, 2], 3);
                                beta1 = Math.Round(arrBangTraBanKe4Canh[a, 3], 3);
                                beta2 = Math.Round(arrBangTraBanKe4Canh[a, 4], 3);
                            }
                        for (int i = 0; i < arrBangTraBanKe4Canh.GetLength(0) - 1; i++)
                            if (arrBangTraBanKe4Canh[i, 0] < tySoL1L2)
                                if (arrBangTraBanKe4Canh[i + 1, 0] > tySoL1L2)
                                {
                                    alpha1 = Math.Round((arrBangTraBanKe4Canh[i + 1, 1]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 1]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);

                                    alpha2 = Math.Round((arrBangTraBanKe4Canh[i + 1, 2]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 2]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);

                                    beta1 = Math.Round((arrBangTraBanKe4Canh[i + 1, 3]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 3]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);

                                    beta2 = Math.Round((arrBangTraBanKe4Canh[i + 1, 4]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 4]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);
                                }

                        break;
                    case "8":
                        for (int a = 0; a <= arrBangTraBanKe4Canh.GetLength(0) - 1; a++)
                            if (arrBangTraBanKe4Canh[a, 0] == tySoL1L2)
                            {
                                alpha1 = Math.Round(arrBangTraBanKe4Canh[a, 5], 3);
                                alpha2 = Math.Round(arrBangTraBanKe4Canh[a, 6], 3);
                                beta1 = Math.Round(arrBangTraBanKe4Canh[a, 7], 3);
                                beta2 = Math.Round(arrBangTraBanKe4Canh[a, 8], 3);
                            }
                        for (int i = 0; i < arrBangTraBanKe4Canh.GetLength(0) - 1; i++)
                            if (arrBangTraBanKe4Canh[i, 0] < tySoL1L2)
                                if (arrBangTraBanKe4Canh[i + 1, 0] > tySoL1L2)
                                {
                                    alpha1 = Math.Round((arrBangTraBanKe4Canh[i + 1, 5]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 5]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);

                                    alpha2 = Math.Round((arrBangTraBanKe4Canh[i + 1, 6]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 6]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);

                                    beta1 = Math.Round((arrBangTraBanKe4Canh[i + 1, 7]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 7]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);

                                    beta2 = Math.Round((arrBangTraBanKe4Canh[i + 1, 8]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 8]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);
                                }

                        break;
                    case "7":
                        for (int a = 0; a <= arrBangTraBanKe4Canh.GetLength(0) - 1; a++)
                            if (arrBangTraBanKe4Canh[a, 0] == tySoL1L2)
                            {
                                alpha1 = Math.Round(arrBangTraBanKe4Canh[a, 9], 3);
                                alpha2 = Math.Round(arrBangTraBanKe4Canh[a, 10], 3);
                                beta1 = Math.Round(arrBangTraBanKe4Canh[a, 11], 3);
                                beta2 = Math.Round(arrBangTraBanKe4Canh[a, 12], 3);
                            }
                        for (int i = 0; i < arrBangTraBanKe4Canh.GetLength(0) - 1; i++)
                            if (arrBangTraBanKe4Canh[i, 0] < tySoL1L2)
                                if (arrBangTraBanKe4Canh[i + 1, 0] > tySoL1L2)
                                {
                                    alpha1 = Math.Round((arrBangTraBanKe4Canh[i + 1, 9]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 9]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);

                                    alpha2 = Math.Round((arrBangTraBanKe4Canh[i + 1, 10]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 10]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);

                                    beta1 = Math.Round((arrBangTraBanKe4Canh[i + 1, 11]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 11]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);

                                    beta2 = Math.Round((arrBangTraBanKe4Canh[i + 1, 12]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 12]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);
                                }

                        break;
                    case "6":
                        for (int a = 0; a <= arrBangTraBanKe4Canh.GetLength(0) - 1; a++)
                            if (arrBangTraBanKe4Canh[a, 0] == tySoL1L2)
                            {
                                alpha1 = Math.Round(arrBangTraBanKe4Canh[a, 13], 3);
                                alpha2 = Math.Round(arrBangTraBanKe4Canh[a, 14], 3);
                                beta1 = Math.Round(arrBangTraBanKe4Canh[a, 15], 3);
                                beta2 = Math.Round(arrBangTraBanKe4Canh[a, 16], 3);
                            }
                        for (int i = 0; i < arrBangTraBanKe4Canh.GetLength(0) - 1; i++)
                            if (arrBangTraBanKe4Canh[i, 0] < tySoL1L2)
                                if (arrBangTraBanKe4Canh[i + 1, 0] > tySoL1L2)
                                {
                                    alpha1 = Math.Round((arrBangTraBanKe4Canh[i + 1, 13]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 13]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);

                                    alpha2 = Math.Round((arrBangTraBanKe4Canh[i + 1, 14]
                                          * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                          + arrBangTraBanKe4Canh[i, 14]
                                          * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                          / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);

                                    beta1 = Math.Round((arrBangTraBanKe4Canh[i + 1, 15]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 15]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);

                                    beta2 = Math.Round((arrBangTraBanKe4Canh[i + 1, 16]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 6])
                                         + arrBangTraBanKe4Canh[i, 16]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);
                                }

                        break;
                    case "5":
                        for (int a = 0; a <= arrBangTraBanKe4Canh.GetLength(0) - 1; a++)
                            if (arrBangTraBanKe4Canh[a, 0] == tySoL1L2)
                            {
                                alpha1 = Math.Round(arrBangTraBanKe4Canh[a, 17], 3);
                                alpha2 = Math.Round(arrBangTraBanKe4Canh[a, 18], 3);
                                beta1 = Math.Round(arrBangTraBanKe4Canh[a, 19], 3);
                            }
                        for (int i = 0; i < arrBangTraBanKe4Canh.GetLength(0) - 1; i++)
                            if (arrBangTraBanKe4Canh[i, 0] < tySoL1L2)
                                if (arrBangTraBanKe4Canh[i + 1, 0] > tySoL1L2)
                                {
                                    alpha1 = Math.Round((arrBangTraBanKe4Canh[i + 1, 17]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 17]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);

                                    alpha2 = Math.Round((arrBangTraBanKe4Canh[i + 1, 18]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 18]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);

                                    beta1 = Math.Round((arrBangTraBanKe4Canh[i + 1, 19]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 19]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);
                                }
                        break;
                    case "4":
                        for (int a = 0; a <= arrBangTraBanKe4Canh.GetLength(0) - 1; a++)
                            if (arrBangTraBanKe4Canh[a, 0] == tySoL1L2)
                            {
                                alpha1 = Math.Round(arrBangTraBanKe4Canh[a, 20], 3);
                                alpha2 = Math.Round(arrBangTraBanKe4Canh[a, 21], 3);
                                beta1 = Math.Round(arrBangTraBanKe4Canh[a, 22], 3);
                            }
                        for (int i = 0; i < arrBangTraBanKe4Canh.GetLength(0) - 1; i++)
                            if (arrBangTraBanKe4Canh[i, 0] < tySoL1L2)
                                if (arrBangTraBanKe4Canh[i + 1, 0] > tySoL1L2)
                                {
                                    alpha1 = Math.Round((arrBangTraBanKe4Canh[i + 1, 20]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 20]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);

                                    alpha2 = Math.Round((arrBangTraBanKe4Canh[i + 1, 21]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 21]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);

                                    beta1 = Math.Round((arrBangTraBanKe4Canh[i + 1, 22]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 22]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);
                                }
                        break;
                    case "3":
                        for (int a = 0; a <= arrBangTraBanKe4Canh.GetLength(0) - 1; a++)
                            if (arrBangTraBanKe4Canh[a, 0] == tySoL1L2)
                            {
                                alpha1 = Math.Round(arrBangTraBanKe4Canh[a, 23], 3);
                                alpha2 = Math.Round(arrBangTraBanKe4Canh[a, 24], 3);
                                beta2 = Math.Round(arrBangTraBanKe4Canh[a, 25], 3);
                            }
                        for (int i = 0; i < arrBangTraBanKe4Canh.GetLength(0) - 1; i++)
                            if (arrBangTraBanKe4Canh[i, 0] < tySoL1L2)
                                if (arrBangTraBanKe4Canh[i + 1, 0] > tySoL1L2)
                                {
                                    alpha1 = Math.Round((arrBangTraBanKe4Canh[i + 1, 23]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 23]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);

                                    alpha2 = Math.Round((arrBangTraBanKe4Canh[i + 1, 24]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 24]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);

                                    beta2 = Math.Round((arrBangTraBanKe4Canh[i + 1, 25]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 25]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);
                                }
                        break;
                    case "2":
                        for (int a = 0; a <= arrBangTraBanKe4Canh.GetLength(0) - 1; a++)
                            if (arrBangTraBanKe4Canh[a, 0] == tySoL1L2)
                            {
                                alpha1 = Math.Round(arrBangTraBanKe4Canh[a, 26], 3);
                                alpha2 = Math.Round(arrBangTraBanKe4Canh[a, 27], 3);
                                beta1 = Math.Round(arrBangTraBanKe4Canh[a, 28], 3);
                            }
                        for (int i = 0; i < arrBangTraBanKe4Canh.GetLength(0) - 1; i++)
                            if (arrBangTraBanKe4Canh[i, 0] < tySoL1L2)
                                if (arrBangTraBanKe4Canh[i + 1, 0] > tySoL1L2)
                                {
                                    alpha1 = Math.Round((arrBangTraBanKe4Canh[i + 1, 26]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 26]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);

                                    alpha2 = Math.Round((arrBangTraBanKe4Canh[i + 1, 27]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 27]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);

                                    beta1 = Math.Round((arrBangTraBanKe4Canh[i + 1, 28]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 28]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);
                                }
                        break;
                    case "1":
                        for (int a = 0; a <= arrBangTraBanKe4Canh.GetLength(0) - 1; a++)
                            if (arrBangTraBanKe4Canh[a, 0] == tySoL1L2)
                            {
                                alpha1 = Math.Round(arrBangTraBanKe4Canh[a, 29], 3);
                                alpha2 = Math.Round(arrBangTraBanKe4Canh[a, 30], 3);
                            }
                        for (int i = 0; i < arrBangTraBanKe4Canh.GetLength(0) - 1; i++)
                            if (arrBangTraBanKe4Canh[i, 0] < tySoL1L2)
                                if (arrBangTraBanKe4Canh[i + 1, 0] > tySoL1L2)
                                {
                                    alpha1 = Math.Round((arrBangTraBanKe4Canh[i + 1, 29]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 29]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);

                                    alpha2 = Math.Round((arrBangTraBanKe4Canh[i + 1, 30]
                                         * (tySoL1L2 - arrBangTraBanKe4Canh[i, 0])
                                         + arrBangTraBanKe4Canh[i, 30]
                                         * (arrBangTraBanKe4Canh[i + 1, 0] - tySoL1L2))
                                         / (arrBangTraBanKe4Canh[i + 1, 0] - arrBangTraBanKe4Canh[i, 0]), 3);
                                }
                        break;
                }
                #endregion

                #region Tính Moment
                double M1, M2, MI, MII;
                double L1 = danhSachSanBanKe4Canh[x].L1;
                double L2 = danhSachSanBanKe4Canh[x].L2;
                // sửa alpha beta ở đây
                M1 = alpha1 * taiPhanBo * L1 * L2;
                M2 = alpha2 * taiPhanBo * L1 * L2;
                MI = -beta1 * taiPhanBo * L1 * L2;
                MII = -beta2 * taiPhanBo * L1 * L2;

                dgvBanKe4Canh.Rows[x].Cells[21].Value = string.Format("{0:f3}", M1);
                dgvBanKe4Canh.Rows[x].Cells[22].Value = string.Format("{0:f3}", M2);
                dgvBanKe4Canh.Rows[x].Cells[23].Value = string.Format("{0:f3}", MI);
                dgvBanKe4Canh.Rows[x].Cells[24].Value = string.Format("{0:f3}", MII);
                #endregion

                #region Tính am
                double amM1, amM2, amMI, amMII;

                amM1 = (M1 * Math.Pow(10, 6)) /
                    (danhSachSanBanKe4Canh[x].Rb
                    * danhSachSanBanKe4Canh[x].ChieuRongDaiBan
                    * Math.Pow(chieuDayH0, 2));
                amM2 = (M2 * Math.Pow(10, 6)) /
                    (danhSachSanBanKe4Canh[x].Rb
                    * danhSachSanBanKe4Canh[x].ChieuRongDaiBan
                    * Math.Pow(chieuDayH0, 2));
                amMI = (MI * Math.Pow(10, 6)) /
                    (danhSachSanBanKe4Canh[x].Rb
                    * danhSachSanBanKe4Canh[x].ChieuRongDaiBan
                    * Math.Pow(chieuDayH0, 2));
                amMII = (MII * Math.Pow(10, 6)) /
                    (danhSachSanBanKe4Canh[x].Rb
                    * danhSachSanBanKe4Canh[x].ChieuRongDaiBan
                    * Math.Pow(chieuDayH0, 2));

                #endregion

                #region Tính diện tích cốt thép
                double SiM1, SiM2, SiMI, SiMII;
                double AsM1, AsM2, AsMI, AsMII;
                if (amM1 < Global.AlphaR)
                {
                    SiM1 = Math.Abs(0.5 * (1 + Math.Sqrt(1 - 2 * amM1)));
                    AsM1 = Math.Abs(M1 * Math.Pow(10, 6)
                        / (Global.Rs * SiM1 * chieuDayH0));
                }
                else
                {
                    MessageBox.Show("Hệ số sàn " + dgvBanLoaiDam.Rows[x].Cells[2].Value.ToString() +
                    " không thỏa điều kiện");
                    return;
                }
                if (amM2 < Global.AlphaR)
                {
                    SiM2 = Math.Abs(0.5 * (1 + Math.Sqrt(1 - 2 * amM2)));
                    AsM2 = Math.Abs(M2 * Math.Pow(10, 6))
                        / (Global.Rs * SiM2 * chieuDayH0);
                }
                else
                {
                    MessageBox.Show("Hệ số sàn " + dgvBanLoaiDam.Rows[x].Cells[2].Value.ToString() +
                    " không thỏa điều kiện");
                    return;
                }
                if (amMI < Global.AlphaR)
                {
                    SiMI = Math.Abs(0.5 * (1 + Math.Sqrt(1 - 2 * amMI)));
                    AsMI = Math.Abs(MI * Math.Pow(10, 6))
                        / (Global.Rs * SiMI * chieuDayH0);
                }
                else
                {
                    MessageBox.Show("Hệ số sàn " + dgvBanLoaiDam.Rows[x].Cells[2].Value.ToString() +
                    " không thỏa điều kiện");
                    return;
                }
                if (amMII < Global.AlphaR)
                {
                    SiMII = Math.Abs(0.5 * (1 + Math.Sqrt(1 - 2 * amM2)));
                    AsMII = Math.Abs((MII * Math.Pow(10, 6))
                        / (Global.Rs * SiMII * chieuDayH0));
                }
                else
                {
                    MessageBox.Show("Hệ số sàn " + dgvBanLoaiDam.Rows[x].Cells[2].Value.ToString() +
                    " không thỏa điều kiện");
                    return;
                }
                dgvBanKe4Canh.Rows[x].Cells[25].Value = string.Format("{0:f3}", AsM1);
                dgvBanKe4Canh.Rows[x].Cells[26].Value = string.Format("{0:f3}", AsM2);
                dgvBanKe4Canh.Rows[x].Cells[27].Value = string.Format("{0:f3}", AsMI);
                dgvBanKe4Canh.Rows[x].Cells[28].Value = string.Format("{0:f3}", AsMII);
                #endregion

                #region Tính hàm lượng cốt thép
                double hamLuongCotThepM1, hamLuongCotThepM2, hamLuongCotThepMI, hamLuongCotThepMII;
                hamLuongCotThepM1 = AsM1
                    / (danhSachSanBanKe4Canh[0].ChieuRongDaiBan * chieuDayH0) * 100;
                hamLuongCotThepM2 = AsM2
                   / (danhSachSanBanKe4Canh[0].ChieuRongDaiBan * chieuDayH0) * 100;
                hamLuongCotThepMI = AsMI
                   / (danhSachSanBanKe4Canh[0].ChieuRongDaiBan * chieuDayH0) * 100;
                hamLuongCotThepMII = (AsMII
                   / (danhSachSanBanKe4Canh[0].ChieuRongDaiBan * chieuDayH0)) * 100;
                dgvBanKe4Canh.Rows[x].Cells[17].Value = string.Format("{0:f3}", hamLuongCotThepM1);
                dgvBanKe4Canh.Rows[x].Cells[18].Value = string.Format("{0:f3}", hamLuongCotThepM2);
                dgvBanKe4Canh.Rows[x].Cells[19].Value = string.Format("{0:f3}", hamLuongCotThepMI);
                dgvBanKe4Canh.Rows[x].Cells[20].Value = string.Format("{0:f3}", hamLuongCotThepMII);
                #endregion

                #region Tính khoảng cách cốt thép


                string stLoaiThep = dgvBanKe4Canh.Rows[x].Cells[12].Value.ToString();
                int loaiThep = int.Parse(stLoaiThep.Trim('Ø'));
                double dienTichThanhThep = loaiThep * loaiThep * Math.PI / 4;
                double khoangCachThepM1, khoangCachThepM2, khoangCachThepMI, khoangCachThepMII;

                khoangCachThepM1 = ((int)(((danhSachSanBanKe4Canh[x].ChieuRongDaiBan
                * dienTichThanhThep) / AsM1) / 10) * 10);
                khoangCachThepM2 = ((int)(((danhSachSanBanKe4Canh[x].ChieuRongDaiBan
                * dienTichThanhThep) / AsM2) / 10) * 10);
                khoangCachThepMI = ((int)(((danhSachSanBanKe4Canh[x].ChieuRongDaiBan
                * dienTichThanhThep) / AsMI) / 10) * 10);
                khoangCachThepMII = ((int)(((danhSachSanBanKe4Canh[x].ChieuRongDaiBan
                * dienTichThanhThep) / AsMII) / 10) * 10);

                dgvBanKe4Canh.Rows[x].Cells[13].Value = "Ø" + loaiThep + "a" + khoangCachThepM1;
                dgvBanKe4Canh.Rows[x].Cells[14].Value = "Ø" + loaiThep + "a" + khoangCachThepM2;
                if (dgvBanKe4Canh.Rows[x].Cells[27].Value.ToString().Equals("0.000"))
                {
                    dgvBanKe4Canh.Rows[x].Cells[15].Value = "x";
                }
                else
                {
                    dgvBanKe4Canh.Rows[x].Cells[15].Value = "Ø" + loaiThep + "a" + khoangCachThepMI;
                }

                if (dgvBanKe4Canh.Rows[x].Cells[28].Value.ToString().Equals("0.000"))
                {
                    dgvBanKe4Canh.Rows[x].Cells[16].Value = "x";
                }
                else
                {
                    dgvBanKe4Canh.Rows[x].Cells[16].Value = "Ø" + loaiThep + "a" + khoangCachThepMII;
                }

                #endregion

                tspbTinhToan.Value += 1;
            }
            tspbTinhToan.Visible = false;
            tsslTinhToan.Visible = false;
            ssMain.Visible = false;
        }

        private void tsmiTinhToanTatCa_Click(object sender, EventArgs e)
        {

            tsbTinhToanTatCa.PerformClick();
        }

        private void tsmiTinhToanLoaiDam_Click(object sender, EventArgs e)
        {
            if (Global.AlphaR == 0)
            {
                MessageBox.Show("Bạn chưa chọn thông số vật liệu."
                    , "Cảnh báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }

            tspbTinhToan.Maximum = dgvBanKe4Canh.Rows.Count;
            tspbTinhToan.Value = 0;
            ssMain.Visible = true;
            tspbTinhToan.Visible = true;
            tsslTinhToan.Visible = true;
            ssMain.Update();
            ssMain.Refresh();

            for (int x = 0; x < dgvBanLoaiDam.Rows.Count; x++)
            {
                double chieuDayH = double.Parse(dgvBanLoaiDam.Rows[x].Cells[4].Value + "");
                double tySoL1L2 = double.Parse(dgvBanLoaiDam.Rows[x].Cells[5].Value + "");
                double tinhTai = double.Parse(dgvBanLoaiDam.Rows[x].Cells[6].Value + "");
                double hoatTai = double.Parse(dgvBanLoaiDam.Rows[x].Cells[7].Value + "");
                double taiPhanBo = double.Parse(dgvBanLoaiDam.Rows[x].Cells[8].Value + "");
                double chieuDayH0 = double.Parse(dgvBanLoaiDam.Rows[x].Cells[10].Value + "");
                double L1 = danhSachSanBanKeLoaiDam[x].L1;
                double L2 = danhSachSanBanKeLoaiDam[x].L2;
                double Mgoi = 0, Mnhip = 0;
                string loaiSoDo = dgvBanLoaiDam.Rows[x].Cells[11].Value + "";

                #region Chọn loại sơ đồ
                switch (dgvBanLoaiDam.Rows[x].Cells[11].Value + "")
                {
                    case "1":
                        Mgoi = (-1 / 8.0) * taiPhanBo * Math.Pow(L1, 2);
                        Mnhip = (9 / 128.0) * (tinhTai + (hoatTai / 2)) * Math.Pow(L1, 2)
                            + (1 / 8.0) * (hoatTai / 2) * Math.Pow(L1, 2);
                        break;
                    case "2":
                        Mgoi = (-1 / 12.0) * taiPhanBo * Math.Pow(L1, 2);
                        Mnhip = (1 / 12.0) * taiPhanBo * Math.Pow(L1, 2);
                        break;
                }

                dgvBanLoaiDam.Rows[x].Cells[17].Value = string.Format("{0:f3}", Mgoi);
                dgvBanLoaiDam.Rows[x].Cells[18].Value = string.Format("{0:f3}", Mnhip);
                #endregion

                #region Tính diện tích cốt thép
                double alphaGoi, alphaNhip;

                alphaGoi = Math.Abs(Mgoi * Math.Pow(10, 6))
                    / (danhSachSanBanKeLoaiDam[x].Rb
                    * danhSachSanBanKe4Canh[x].ChieuRongDaiBan * Math.Pow(chieuDayH0, 2));
                alphaNhip = Math.Abs(Mnhip * Math.Pow(10, 6))
                    / (danhSachSanBanKeLoaiDam[x].Rb
                    * danhSachSanBanKe4Canh[x].ChieuRongDaiBan * Math.Pow(chieuDayH0, 2));

                // Tính diện tích cốt thép
                double cosiGoi, cosiNhip = 0, Asgoi = 0, Asnhip = 0;
                if (alphaGoi <= Global.AlphaR)
                {
                    cosiGoi = 0.5 * (1 + Math.Sqrt(1 - 2 * (alphaGoi)));
                    Asgoi = Math.Abs(Mgoi * Math.Pow(10, 6)) / (Global.Rs * cosiGoi * chieuDayH0);
                }
                else
                {
                    MessageBox.Show("Hệ số sàn " + dgvBanLoaiDam.Rows[x].Cells[2].Value.ToString() +
                        " không thỏa điều kiện");
                    return;
                }
                if (alphaNhip <= Global.AlphaR)
                {
                    cosiNhip = 0.5 * (1 + Math.Sqrt(1 - 2 * (alphaNhip)));
                    Asnhip = (Math.Abs(Mnhip * Math.Pow(10, 6)) / (Global.Rs * cosiNhip * chieuDayH0));
                }
                else
                {
                    MessageBox.Show("Hệ số sàn " + dgvBanLoaiDam.Rows[x].Cells[2].Value.ToString() +
                        " không thỏa điều kiện");
                    return;
                }

                dgvBanLoaiDam.Rows[x].Cells[19].Value = Math.Round(Asgoi, 3);
                dgvBanLoaiDam.Rows[x].Cells[20].Value = Math.Round(Asnhip, 3);

                double hamLuongCotThepGoi, hamLuongCotThepNhip;
                hamLuongCotThepGoi = (Asgoi
                    / (danhSachSanBanKeLoaiDam[x].ChieuRongDaiBan * chieuDayH0) * 100);
                hamLuongCotThepNhip = (Asnhip
                / (danhSachSanBanKeLoaiDam[x].ChieuRongDaiBan * chieuDayH0) * 100);

                dgvBanLoaiDam.Rows[x].Cells[15].Value = string.Format("{0:f3}", hamLuongCotThepGoi);
                dgvBanLoaiDam.Rows[x].Cells[16].Value = string.Format("{0:f3}", hamLuongCotThepNhip);
                #endregion

                #region Tính khoảng cách cốt thép

                string stLoaiThep = dgvBanLoaiDam.Rows[x].Cells[12].Value.ToString();
                int loaiThep = int.Parse(stLoaiThep.Trim('Ø'));
                double dienTichThanhThep = loaiThep * loaiThep * Math.PI / 4;
                double khoangCachThepMgoi, khoangCachThepMnhip;

                khoangCachThepMgoi = ((int)(((danhSachSanBanKeLoaiDam[x].ChieuRongDaiBan
                * dienTichThanhThep) / Asgoi) / 10) * 10);
                khoangCachThepMnhip = ((int)(((danhSachSanBanKeLoaiDam[x].ChieuRongDaiBan
                * dienTichThanhThep) / Asnhip) / 10) * 10);

                dgvBanLoaiDam.Rows[x].Cells[13].Value = "Ø" + loaiThep + "a" + khoangCachThepMgoi;
                dgvBanLoaiDam.Rows[x].Cells[14].Value = "Ø" + loaiThep + "a" + khoangCachThepMnhip;
                #endregion

                tspbTinhToan.Value += 1;
            }
            tspbTinhToan.Visible = false;
            tsslTinhToan.Visible = false;
            ssMain.Visible = false;
        }


        private void tsmiXoaHang4Canh_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvBanKe4Canh.SelectedRows.Count; i++)
            {
                dgvBanKe4Canh.Rows.Remove(dgvBanKe4Canh.SelectedRows[i]);
            }
        }

        #region Chọn loại sơ đồ 4 cạnh
        private void tsmiLoaiSoDo4Canh1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvBanKe4Canh.SelectedRows.Count; i++)
            {
                dgvBanKe4Canh.SelectedRows[i].Cells[11].Value = 1;
            }
        }

        private void tsmiLoaiSoDo4Canh2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvBanKe4Canh.SelectedRows.Count; i++)
            {
                dgvBanKe4Canh.SelectedRows[i].Cells[11].Value = 2;
            }
        }

        private void tsmiLoaiSoDo4Canh3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvBanKe4Canh.SelectedRows.Count; i++)
            {
                dgvBanKe4Canh.SelectedRows[i].Cells[11].Value = 3;
            }
        }

        private void tsmiLoaiSoDo4Canh4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvBanKe4Canh.SelectedRows.Count; i++)
            {
                dgvBanKe4Canh.SelectedRows[i].Cells[11].Value = 4;
            }
        }

        private void tsmiLoaiSoDo4Canh5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvBanKe4Canh.SelectedRows.Count; i++)
            {
                dgvBanKe4Canh.SelectedRows[i].Cells[11].Value = 5;
            }
        }

        private void tsmiLoaiSoDo4Canh6_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvBanKe4Canh.SelectedRows.Count; i++)
            {
                dgvBanKe4Canh.SelectedRows[i].Cells[11].Value = 6;
            }
        }

        private void tsmiLoaiSoDo4Canh7_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvBanKe4Canh.SelectedRows.Count; i++)
            {
                dgvBanKe4Canh.SelectedRows[i].Cells[11].Value = 7;
            }
        }

        private void tsmiLoaiSoDo4Canh8_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvBanKe4Canh.SelectedRows.Count; i++)
            {
                dgvBanKe4Canh.SelectedRows[i].Cells[11].Value = 8;
            }
        }

        private void tsmiLoaiSoDo4Canh9_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvBanKe4Canh.SelectedRows.Count; i++)
            {
                dgvBanKe4Canh.SelectedRows[i].Cells[11].Value = 9;
            }
        }
        #endregion

        #region Chọn loại sơ đồ loại dầm
        private void tsmiLoaiSoDoLoaiDam1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvBanLoaiDam.SelectedRows.Count; i++)
            {
                dgvBanLoaiDam.SelectedRows[i].Cells[11].Value = 1;
            }
        }

        private void tsmiLoaiSoDoLoaiDam2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvBanLoaiDam.SelectedRows.Count; i++)
            {
                dgvBanLoaiDam.SelectedRows[i].Cells[11].Value = 2;
            }
        }
        #endregion

        #region Chọn loại thép sơ đồ 4 cạnh
        private void tsmiPhi6BonCanh_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvBanKe4Canh.SelectedRows.Count; i++)
            {
                dgvBanKe4Canh.SelectedRows[i].Cells[12].Value = "Ø" + 6;
            }
        }

        private void tsmiPhi8BonCanh_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvBanKe4Canh.SelectedRows.Count; i++)
            {
                dgvBanKe4Canh.SelectedRows[i].Cells[12].Value = "Ø" + 8;
            }
        }

        private void tsmiPhi10BonCanh_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvBanKe4Canh.SelectedRows.Count; i++)
            {
                dgvBanKe4Canh.SelectedRows[i].Cells[12].Value = "Ø" + 10;
            }
        }
        #endregion

        #region Chọn loại thép sơ đồ loại dầm
        private void tsmiPhi6LoaiDam_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvBanLoaiDam.SelectedRows.Count; i++)
            {
                dgvBanLoaiDam.SelectedRows[i].Cells[12].Value = "Ø" + 6;
            }
        }

        private void tsmiPhi8LoaiDam_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvBanLoaiDam.SelectedRows.Count; i++)
            {
                dgvBanLoaiDam.SelectedRows[i].Cells[12].Value = "Ø" + 8;
            }
        }

        private void tsmiPhi10LoaiDam_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvBanLoaiDam.SelectedRows.Count; i++)
            {
                dgvBanLoaiDam.SelectedRows[i].Cells[12].Value = "Ø" + 10;
            }
        }
        #endregion

        private void tsmiXoaHangLoaiDam_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvBanLoaiDam.SelectedRows.Count; i++)
            {
                dgvBanLoaiDam.Rows.Remove(dgvBanLoaiDam.SelectedRows[i]);
            }
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            // Get the current document and database
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;
            Editor editor = acDoc.Editor;
            // Set the global linetype scale
            acCurDb.Ltscale = 1000;
            // Get position of a point when user click
            var ppo = new PromptPointOptions("\nChọn vị trí đặt khung tên: ");
            var ppr = editor.GetPoint(ppo);
            if (ppr.Status != PromptStatus.OK)
                return;
            var basePt = ppr.Value;
            double x = double.Parse(basePt.X.ToString());
            double y = double.Parse(basePt.Y.ToString());
            int bientam = 0;

            // Lấy về danh sách các Tầng
            List<string> danhSachTang = new List<string>();
            danhSachTang.Add(dgvDrawMatBang.Rows[0].Cells[0].Value + "");
            for (int i = 0; i < dgvDrawMatBang.Rows.Count - 1; i++)
            {
                string tenTang = dgvDrawMatBang.Rows[i].Cells[0].Value.ToString();
                if (dgvDrawMatBang.Rows[i + 1].Cells[0].Value.ToString().Equals(tenTang))
                {
                    continue;
                }
                else
                {
                    danhSachTang.Add(dgvDrawMatBang.Rows[i + 1].Cells[0].Value.ToString());
                }
            }


            double ChieuDaiLuoiX = 0;
            //lấy chiều dài của lưới
            for (int i = 0; i < dgvDrawGrid.Rows.Count - 1; i++)
            {

                if (dgvDrawGrid.Rows[i].Cells[0].Value.ToString() == "Y (Cartesian)")
                {
                    ChieuDaiLuoiX = double.Parse(dgvDrawGrid.Rows[i].Cells[2].Value.ToString()) * 1000;
                }

            }

            double Khoangcach = 0;
            //Vẽ các mặt bằng
            for (int i = 0; i < danhSachTang.Count; i++)
            {
               DrawMatBang(x, y + Khoangcach, acCurDb, acDoc, danhSachTang[i].ToString());
                Khoangcach = Khoangcach + ChieuDaiLuoiX + 10000;
            }
           




        }

        public void DrawMatBang(double x, double y, Database acCurDb, Document acDoc, string Ten)
        {
            // Start a transaction
            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                // Open the Block table for read
                BlockTable acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;

                // Open the Block table record Model space for write
                BlockTableRecord acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
               
                
                // Vẽ Ô sàn
                for (int i = 0; i < dgvDrawMatBang.Rows.Count - 1; i++)
                {
                    if (dgvDrawMatBang.Rows[i].Cells[0].Value.ToString() == Ten)
                    {
                        using (Polyline acPoly = new Polyline())
                        {
                            acPoly.AddVertexAt(0, new Point2d(x + double.Parse(dgvDrawMatBang.Rows[i].Cells[2].Value.ToString()) * 1000
                                , y + double.Parse(dgvDrawMatBang.Rows[i].Cells[3].Value.ToString()) * 1000), 0, 0, 0);
                            acPoly.AddVertexAt(1, new Point2d(x + double.Parse(dgvDrawMatBang.Rows[i].Cells[4].Value.ToString()) * 1000
                                , y + double.Parse(dgvDrawMatBang.Rows[i].Cells[5].Value.ToString()) * 1000), 0, 0, 0);
                            acPoly.AddVertexAt(2, new Point2d(x + double.Parse(dgvDrawMatBang.Rows[i].Cells[6].Value.ToString()) * 1000
                                , y + double.Parse(dgvDrawMatBang.Rows[i].Cells[7].Value.ToString()) * 1000), 0, 0, 0);
                            acPoly.AddVertexAt(3, new Point2d(x + double.Parse(dgvDrawMatBang.Rows[i].Cells[8].Value.ToString()) * 1000
                                , y + double.Parse(dgvDrawMatBang.Rows[i].Cells[9].Value.ToString()) * 1000), 0, 0, 0);
                            acPoly.AddVertexAt(4, new Point2d(x + double.Parse(dgvDrawMatBang.Rows[i].Cells[2].Value.ToString()) * 1000
                                , y + double.Parse(dgvDrawMatBang.Rows[i].Cells[3].Value.ToString()) * 1000), 0, 0, 0);
                            // set color is red for polyline
                            acPoly.Layer = "THXD-Duong_Bao";

                            // Vẽ thép dọc
                            // vẽ thép nằm dọc
                            Line ln1 = new Line(new Point3d(x + ((double.Parse(dgvDrawMatBang.Rows[i].Cells[4].Value.ToString()) * 1000 -
                                double.Parse(dgvDrawMatBang.Rows[i].Cells[2].Value.ToString()) * 1000) / 2+ double.Parse(dgvDrawMatBang.Rows[i].Cells[2].Value.ToString()) * 1000),
                                y + (double.Parse(dgvDrawMatBang.Rows[i].Cells[3].Value.ToString()) * 1000), 0),
                                new Point3d(x + ((double.Parse(dgvDrawMatBang.Rows[i].Cells[6].Value.ToString()) * 1000 +
                                double.Parse(dgvDrawMatBang.Rows[i].Cells[8].Value.ToString()) * 1000) / 2),
                                y + (double.Parse(dgvDrawMatBang.Rows[i].Cells[7].Value.ToString()) * 1000), 0));
                            ln1.Layer = "THXD-Thep";
                            acBlkTblRec.AppendEntity(ln1);
                            acTrans.AddNewlyCreatedDBObject(ln1, true);

                            //// vẽ thép nằm ngang
                            Line ln2 = new Line(new Point3d(x + (double.Parse(dgvDrawMatBang.Rows[i].Cells[2].Value.ToString()) * 1000),
                                y + ((double.Parse(dgvDrawMatBang.Rows[i].Cells[9].Value.ToString()) * 1000 -
                                double.Parse(dgvDrawMatBang.Rows[i].Cells[3].Value.ToString()) * 1000) / 2+ double.Parse(dgvDrawMatBang.Rows[i].Cells[3].Value.ToString())*1000), 0),
                                new Point3d(x + (double.Parse(dgvDrawMatBang.Rows[i].Cells[4].Value.ToString()) * 1000),
                                y + ((double.Parse(dgvDrawMatBang.Rows[i].Cells[7].Value.ToString()) * 1000 -
                                double.Parse(dgvDrawMatBang.Rows[i].Cells[5].Value.ToString()) * 1000) / 2+ double.Parse(dgvDrawMatBang.Rows[i].Cells[5].Value.ToString()) * 1000), 0));
                            ln2.Layer = "THXD-Thep";
                            acBlkTblRec.AppendEntity(ln2);
                            acTrans.AddNewlyCreatedDBObject(ln2, true);

                            // Add the new object to the block table record and the transaction
                            acBlkTblRec.AppendEntity(acPoly);
                            acTrans.AddNewlyCreatedDBObject(acPoly, true);

                        }
                    }
                }



                // Vẽ dầm
                for (int i = 0; i < dgvDrawBeam.Rows.Count - 1; i++)
                {
                    if (dgvDrawBeam.Rows[i].Cells[0].Value.ToString() == Ten)
                    {
                        // tọa độ của đầu 1 và 2
                        double Xdau1 = double.Parse(dgvDrawBeam.Rows[i].Cells[2].Value.ToString()) * 1000;
                        double Ydau1 = double.Parse(dgvDrawBeam.Rows[i].Cells[3].Value.ToString()) * 1000;
                        double Xdau2 = double.Parse(dgvDrawBeam.Rows[i].Cells[4].Value.ToString()) * 1000;
                        double Ydau2 = double.Parse(dgvDrawBeam.Rows[i].Cells[5].Value.ToString()) * 1000;

                        double Width = double.Parse(dgvDrawBeam.Rows[i].Cells[7].Value.ToString()) * 1000;

                        if (Xdau1 == Xdau2)
                        {
                            using (Polyline acPoly = new Polyline())
                            {
                                acPoly.AddVertexAt(0, new Point2d(x + Xdau1 - 0.5 * Width, y + Ydau1), 0, 0, 0);
                                acPoly.AddVertexAt(1, new Point2d(x + Xdau1 + 0.5 * Width, y + Ydau1), 0, 0, 0);
                                acPoly.AddVertexAt(2, new Point2d(x + Xdau2 + 0.5 * Width, y + Ydau2), 0, 0, 0);
                                acPoly.AddVertexAt(3, new Point2d(x + Xdau2 - 0.5 * Width, y + Ydau2), 0, 0, 0);
                                acPoly.AddVertexAt(4, new Point2d(x + Xdau1 - 0.5 * Width, y + Ydau1), 0, 0, 0);
                                // set color is red for polyline
                                acPoly.Layer = "THXD-Dam";

                                // Add the new object to the block table record and the transaction
                                acBlkTblRec.AppendEntity(acPoly);
                                acTrans.AddNewlyCreatedDBObject(acPoly, true);
                            }
                        }
                        else
                        {
                            using (Polyline acPoly = new Polyline())
                            {
                                acPoly.AddVertexAt(0, new Point2d(x + Xdau1, y + Ydau1 - 0.5 * Width), 0, 0, 0);
                                acPoly.AddVertexAt(1, new Point2d(x + Xdau1, y + Ydau1 + 0.5 * Width), 0, 0, 0);
                                acPoly.AddVertexAt(2, new Point2d(x + Xdau2, y + Ydau2 + 0.5 * Width), 0, 0, 0);
                                acPoly.AddVertexAt(3, new Point2d(x + Xdau2, y + Ydau2 - 0.5 * Width), 0, 0, 0);
                                acPoly.AddVertexAt(4, new Point2d(x + Xdau1, y + Ydau1 - 0.5 * Width), 0, 0, 0);
                                // set color is red for polyline
                                acPoly.Layer = "THXD-Dam";

                                // Add the new object to the block table record and the transaction
                                acBlkTblRec.AppendEntity(acPoly);
                                acTrans.AddNewlyCreatedDBObject(acPoly, true);
                            }
                        }

                    }

                }

                // Vẽ Lưới
                double ChieuDaiLuoiY = 0;
                double ChieuDaiLuoiX = 0;
                //lấy chiều dài của lưới
                for (int i = 0; i < dgvDrawGrid.Rows.Count - 1; i++)
                {
                    if (dgvDrawGrid.Rows[i].Cells[0].Value.ToString() == "X (Cartesian)")
                    {
                        ChieuDaiLuoiY = double.Parse(dgvDrawGrid.Rows[i].Cells[2].Value.ToString()) * 1000;
                    }
                    else if (dgvDrawGrid.Rows[i].Cells[0].Value.ToString() == "Y (Cartesian)")
                    {
                        ChieuDaiLuoiX = double.Parse(dgvDrawGrid.Rows[i].Cells[2].Value.ToString()) * 1000;
                    }

                }

                CreateMText(new Point3d(x+ 0.4*ChieuDaiLuoiY,y- 0.1*ChieuDaiLuoiX,0), 0, "MẶT BẰNG TẦNG " + Ten, 1000, acBlkTblRec, acTrans);
                // Vẽ lưới
                for (int i = 0; i < dgvDrawGrid.Rows.Count -1; i++)
                {
                    if (dgvDrawGrid.Rows[i].Cells[3].Value.ToString() == "Yes")
                    {
                        double KhoangCach = double.Parse(dgvDrawGrid.Rows[i].Cells[2].Value.ToString()) * 1000;
                        if (dgvDrawGrid.Rows[i].Cells[0].Value.ToString() == "X (Cartesian)")
                        {
                            Line ln1 = new Line(new Point3d(x + KhoangCach, y - 1000, 0), new Point3d(x + KhoangCach, y + ChieuDaiLuoiX + 1000, 0));
                            ln1.Layer = "THXD-Truc";
                            acBlkTblRec.AppendEntity(ln1);
                            acTrans.AddNewlyCreatedDBObject(ln1, true);
                            VeDauGrid(x + KhoangCach, y + ChieuDaiLuoiX + 3000, dgvDrawGrid.Rows[i].Cells[1].Value.ToString(), acDoc);
                        }
                        else if (dgvDrawGrid.Rows[i].Cells[0].Value.ToString() == "Y (Cartesian)")
                        {
                            Line ln1 = new Line(new Point3d(x - 1000, y + KhoangCach, 0), new Point3d(x + ChieuDaiLuoiY + 1000, y + KhoangCach, 0));
                            ln1.Layer = "THXD-Truc";
                            acBlkTblRec.AppendEntity(ln1);
                            acTrans.AddNewlyCreatedDBObject(ln1, true);
                            VeDauGrid(x + ChieuDaiLuoiY + 3000, y + KhoangCach, dgvDrawGrid.Rows[i].Cells[1].Value.ToString(), acDoc);
                        }
                    }


                }

                // Vẽ cột
                for (int i = 0; i < dgvDrawColumn.Rows.Count - 1; i++)
                {
                    if (dgvDrawColumn.Rows[i].Cells[0].Value.ToString() == Ten)
                    {
                        double Xtam = double.Parse(dgvDrawColumn.Rows[i].Cells[2].Value.ToString()) * 1000;
                        double Ytam = double.Parse(dgvDrawColumn.Rows[i].Cells[3].Value.ToString()) * 1000;
                        double Width = double.Parse(dgvDrawColumn.Rows[i].Cells[4].Value.ToString()) * 1000;
                        double Height = double.Parse(dgvDrawColumn.Rows[i].Cells[5].Value.ToString()) * 1000;

                        using (Polyline acPoly = new Polyline())
                        {
                            acPoly.AddVertexAt(0, new Point2d(x + Xtam - 0.5 * Width, y + Ytam - 0.5 * Height), 0, 0, 0);
                            acPoly.AddVertexAt(1, new Point2d(x + Xtam + 0.5 * Width, y + Ytam - 0.5 * Height), 0, 0, 0);
                            acPoly.AddVertexAt(2, new Point2d(x + Xtam + 0.5 * Width, y + Ytam + 0.5 * Height), 0, 0, 0);
                            acPoly.AddVertexAt(3, new Point2d(x + Xtam - 0.5 * Width, y + Ytam + 0.5 * Height), 0, 0, 0);
                            acPoly.AddVertexAt(4, new Point2d(x + Xtam - 0.5 * Width, y + Ytam - 0.5 * Height), 0, 0, 0);
                            // set color is red for polyline
                            acPoly.Layer = "THXD-Cot";

                            // Add the new object to the block table record and the transaction
                            acBlkTblRec.AppendEntity(acPoly);
                            acTrans.AddNewlyCreatedDBObject(acPoly, true);

                            // Adds the arc and line to an object id collection
                            ObjectIdCollection acObjIdColl = new ObjectIdCollection();
                            acObjIdColl.Add(acPoly.ObjectId);

                            // Create the hatch object and append it to the block table record
                            Hatch acHatch = new Hatch();
                            acHatch.Layer = "THXD-Cot";
                            acBlkTblRec.AppendEntity(acHatch);
                            acTrans.AddNewlyCreatedDBObject(acHatch, true);

                            // Set the properties of the hatch object
                            // Associative must be set after the hatch object is appended to the 
                            // block table record and before AppendLoop
                            acHatch.SetDatabaseDefaults();
                            acHatch.SetHatchPattern(HatchPatternType.PreDefined, "Solid");
                            acHatch.Associative = true;
                            acHatch.AppendLoop(HatchLoopTypes.Outermost, acObjIdColl);

                            // Evaluate the hatch
                            acHatch.EvaluateHatch(true);

                            // Increase the pattern scale by 2 and re-evaluate the hatch
                            acHatch.PatternScale = acHatch.PatternScale + 2;
                            acHatch.SetHatchPattern(acHatch.PatternType, acHatch.PatternName);
                            acHatch.EvaluateHatch(true);

                        }
                    }

                }

                // Vẽ thép lớp dưới


                acTrans.Commit();
            }
        }
        public void DrawLine(Point3d pt1, Point3d pt2, BlockTableRecord acBlkTblRec, Transaction acTrans, int mauColorIndex)
        {
            Line ln1 = new Line(pt1, pt2);

            ln1.ColorIndex = mauColorIndex;
            acBlkTblRec.AppendEntity(ln1);
            acTrans.AddNewlyCreatedDBObject(ln1, true);
        }
        public void CreateMText(Point3d location, double rotation, string content, double textHeight, BlockTableRecord acBlkTblRec, Transaction acTrans)
        {
            MText acMText = new MText();
            acMText.SetDatabaseDefaults();
            acMText.Location = location;
            acMText.Width = 100;
            acMText.TextHeight = textHeight;
            acMText.Rotation = rotation * (Math.PI / 180); // máy hiểu theo radian nên mình phải chuyển về độ
            acMText.Contents = content;
            acBlkTblRec.AppendEntity(acMText);
            acTrans.AddNewlyCreatedDBObject(acMText, true);
        }

        public void VeDauGrid(double x, double y, string content, Document acDoc)
        {
            // Get the current document and database

            Database acCurDb = acDoc.Database;
            Editor editor = acDoc.Editor;

            // Start a transaction
            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                // Open the Block table for read
                BlockTable acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;

                // Open the Block table record Model space for write
                BlockTableRecord acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                // Create a circle that is at 2,3 with a radius of 4.25
                Circle acCirc = new Circle();
                acCirc.SetDatabaseDefaults();
                acCirc.Center = new Point3d(x, y, 0);

                double radius = 1000;
                double doandai = 600;

                acCirc.Radius = radius;

                DrawLine(new Point3d(x + radius, y, 0), new Point3d(x + radius + doandai, y, 0), acBlkTblRec, acTrans, 7);
                DrawLine(new Point3d(x, y + radius, 0), new Point3d(x, y + radius + doandai, 0), acBlkTblRec, acTrans, 7);
                DrawLine(new Point3d(x - radius, y, 0), new Point3d(x - radius - doandai, y, 0), acBlkTblRec, acTrans, 7);
                DrawLine(new Point3d(x, y - radius, 0), new Point3d(x, y - radius - doandai, 0), acBlkTblRec, acTrans, 7);

                CreateMText(new Point3d(x - 400, y + 600, 0), 0, content, 1000, acBlkTblRec, acTrans);
                // Add the new object to the block table record and the transaction
                acBlkTblRec.AppendEntity(acCirc);
                acTrans.AddNewlyCreatedDBObject(acCirc, true);
                acTrans.Commit();
            }
        }

        private void btnDrawKhungTen_Click(object sender, EventArgs e)
        {
            
        }

        private void tsmiFileXuatFileExcel_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ToExcel(dgvBanLoaiDam,dgvBanKe4Canh, saveFileDialog1.FileName);
            }
        }

        private void ToExcel(DataGridView dgvData1,DataGridView dgvData2, string fileName)
        {
            Microsoft.Office.Interop.Excel.Application excel;
            Microsoft.Office.Interop.Excel.Workbook workbook;
            Microsoft.Office.Interop.Excel.Worksheet worksheet;
            try
            {
                excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = true;
                excel.DisplayAlerts = false;

                workbook = excel.Workbooks.Add(Type.Missing);
                workbook.Sheets.Add();

                // Bản kê loại dầm
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets["Sheet1"];
                worksheet.Name = "Du Lieu Ban Ke Loai Dam";

                // export header
                for (int i = 0; i < dgvData1.ColumnCount; i++)
                {
                    worksheet.Cells[1, i + 1] = dgvData1.Columns[i].HeaderText;
                }

                // export content
                for (int i = 0; i < dgvData1.RowCount; i++)
                {
                    for (int j = 0; j < dgvData1.ColumnCount; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dgvData1.Rows[i].Cells[j].Value.ToString();
                    }
                }


                //Bản kê 4 cạnh
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets["Sheet2"];
                worksheet.Name = "Du Lieu Ban Ke 4 Canh";

                // export header
                for (int i = 0; i < dgvData2.ColumnCount; i++)
                {
                    worksheet.Cells[1, i + 1] = dgvData2.Columns[i].HeaderText;
                }

                // export content
                for (int i = 0; i < dgvData2.RowCount; i++)
                {
                    for (int j = 0; j < dgvData2.ColumnCount; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dgvData2.Rows[i].Cells[j].Value.ToString();
                    }
                }
                // save workbook
                workbook.SaveAs(fileName);


                MessageBox.Show("Export successful.!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                workbook = null;
                worksheet = null;
            }



        }
    }
}
