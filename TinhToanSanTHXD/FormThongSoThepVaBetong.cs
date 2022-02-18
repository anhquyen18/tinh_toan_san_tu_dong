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

namespace TinhToanSanTHXD
{
    public partial class FormThongSoThepVaBetong : Form
    {
        public FormThongSoThepVaBetong()
        {
            InitializeComponent();
        }

        #region Bắt Event combobox Loại Bê Tông thay đổi giá trị

        private double[] arrRb = new[] { 7.5, 8.5, 11.5, 14.5, 17 };
        private double[] arrRbt = new[] { 0.66, 0.75, 0.9, 1.05, 1.2 };
        private int[] arrEb = new[] { 21000, 23000, 27000, 30000, 32500 };
        private void cbxLoaiBeTong_SelectedIndexChanged(object sender, EventArgs e)
        {
            txbRb.Text = arrRb[cbxLoaiBeTong.SelectedIndex].ToString();
            txbRbt.Text = arrRbt[cbxLoaiBeTong.SelectedIndex].ToString();
            txbEb.Text = arrEb[cbxLoaiBeTong.SelectedIndex].ToString();

            Global.Rb = float.Parse(txbRb.Text);
            Global.Rbt = float.Parse(txbRbt.Text);
            Global.Eb = float.Parse(txbEb.Text);
        }
        #endregion

        #region Bắt Event combobox Loại thép thay đổi giá trị
        private int[] arrRs = new[] { 225, 280, 365 };
        private int[] arrRsc = new[] { 225, 280, 365 };
        private int[] arrEs = new[] { 210000, 210000, 200000 };
        private void cbxLoaiThep_SelectedIndexChanged(object sender, EventArgs e)
        {
            txbRs.Text = arrRs[cbxLoaiThep.SelectedIndex].ToString();
            txbRsc.Text = arrRsc[cbxLoaiThep.SelectedIndex].ToString();
            txbEs.Text = arrEs[cbxLoaiThep.SelectedIndex].ToString();

            Global.Rs = float.Parse(txbRs.Text);
            Global.Rsc = float.Parse(txbRsc.Text);
            Global.Es = float.Parse(txbEs.Text);
        }
        #endregion

        public static float[,] readFromFile(string path, int socot, int sohang)
        {

            StreamReader sr = new StreamReader(path);
            string s = "";
            float[,] a = new float[sohang, socot];
            int i = 0;

            while ((s = sr.ReadLine()) != null)
            {
                string[] content = s.Trim().Split('\t');

                for (int j = 0; j < content.Length; j++)
                {
                    a[i, j] = float.Parse(content[j]);
                }

                i++;
            }
            return a;
        }

        private void cbxHeSoLamViec_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Load bảng tra alpha R
            string path1 = @"Bang_Tra_Alpha_R_Dua_Vao_He_So_Lam_Viec.txt";
            float[,] arrBangTraAlphaR = readFromFile(path1, 5, 18);

            if (cbxLoaiThep.Text == "AIII" & cbxHeSoLamViec.Text == "0.9")
            {
                switch (cbxLoaiBeTong.Text)
                {
                    case "B12.5":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[1, 0].ToString();
                            txbCeR.Text = arrBangTraAlphaR[0, 0].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }

                    case "B15":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[1, 1].ToString();
                            txbCeR.Text = arrBangTraAlphaR[0, 1].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }

                    case "B20":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[1, 2].ToString();
                            txbCeR.Text = arrBangTraAlphaR[0, 2].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }

                    case "B25":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[1, 3].ToString();
                            txbCeR.Text = arrBangTraAlphaR[0, 3].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }

                    case "B30":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[1, 4].ToString();
                            txbCeR.Text = arrBangTraAlphaR[0, 4].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }
                }
            }
            else if (cbxLoaiThep.Text == "AII" & cbxHeSoLamViec.Text == "0.9")
            {
                switch (cbxLoaiBeTong.Text)
                {
                    case "B12.5":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[3, 0].ToString();
                            txbCeR.Text = arrBangTraAlphaR[2, 0].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }

                    case "B15":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[3, 1].ToString();
                            txbCeR.Text = arrBangTraAlphaR[2, 1].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }

                    case "B20":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[3, 2].ToString();
                            txbCeR.Text = arrBangTraAlphaR[2, 2].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }

                    case "B25":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[3, 3].ToString();
                            txbCeR.Text = arrBangTraAlphaR[2, 3].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }

                    case "B30":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[3, 4].ToString();
                            txbCeR.Text = arrBangTraAlphaR[2, 4].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }
                }
            }
            else if (cbxLoaiThep.Text == "AI" & cbxHeSoLamViec.Text == "0.9")
            {
                switch (cbxLoaiBeTong.Text)
                {
                    case "B12.5":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[5, 0].ToString();
                            txbCeR.Text = arrBangTraAlphaR[4, 0].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }

                    case "B15":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[5, 1].ToString();
                            txbCeR.Text = arrBangTraAlphaR[4, 1].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }

                    case "B20":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[5, 2].ToString();
                            txbCeR.Text = arrBangTraAlphaR[4, 2].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }

                    case "B25":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[5, 3].ToString();
                            txbCeR.Text = arrBangTraAlphaR[4, 3].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }

                    case "B30":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[5, 4].ToString();
                            txbCeR.Text = arrBangTraAlphaR[4, 4].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }
                }
            }
            else if (cbxLoaiThep.Text == "AIII" & cbxHeSoLamViec.Text == "1.0")
            {
                switch (cbxLoaiBeTong.Text)
                {
                    case "B12.5":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[7, 0].ToString();
                            txbCeR.Text = arrBangTraAlphaR[6, 0].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }

                    case "B15":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[7, 1].ToString();
                            txbCeR.Text = arrBangTraAlphaR[6, 1].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }

                    case "B20":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[7, 2].ToString();
                            txbCeR.Text = arrBangTraAlphaR[6, 2].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }

                    case "B25":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[7, 3].ToString();
                            txbCeR.Text = arrBangTraAlphaR[6, 3].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }

                    case "B30":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[7, 4].ToString();
                            txbCeR.Text = arrBangTraAlphaR[6, 4].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }
                }
            }
            else if (cbxLoaiThep.Text == "AII" & cbxHeSoLamViec.Text == "1.0")
            {
                switch (cbxLoaiBeTong.Text)
                {
                    case "B12.5":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[9, 0].ToString();
                            txbCeR.Text = arrBangTraAlphaR[8, 0].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }

                    case "B15":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[9, 1].ToString();
                            txbCeR.Text = arrBangTraAlphaR[8, 1].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }

                    case "B20":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[9, 2].ToString();
                            txbCeR.Text = arrBangTraAlphaR[8, 2].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }

                    case "B25":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[9, 3].ToString();
                            txbCeR.Text = arrBangTraAlphaR[8, 3].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }

                    case "B30":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[9, 4].ToString();
                            txbCeR.Text = arrBangTraAlphaR[8, 4].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }
                }
            }
            else if (cbxLoaiThep.Text == "AI" & cbxHeSoLamViec.Text == "1.0")
            {
                switch (cbxLoaiBeTong.Text)
                {
                    case "B12.5":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[11, 0].ToString();
                            txbCeR.Text = arrBangTraAlphaR[10, 0].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }

                    case "B15":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[11, 1].ToString();
                            txbCeR.Text = arrBangTraAlphaR[10, 1].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }

                    case "B20":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[11, 2].ToString();
                            txbCeR.Text = arrBangTraAlphaR[10, 2].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }

                    case "B25":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[11, 3].ToString();
                            txbCeR.Text = arrBangTraAlphaR[10, 3].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }

                    case "B30":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[11, 4].ToString();
                            txbCeR.Text = arrBangTraAlphaR[10, 4].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }
                }
            }
            else if (cbxLoaiThep.Text == "AIII" & cbxHeSoLamViec.Text == "1.1")
            {
                switch (cbxLoaiBeTong.Text)
                {
                    case "B12.5":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[13, 0].ToString();
                            txbCeR.Text = arrBangTraAlphaR[12, 0].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }

                    case "B15":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[13, 1].ToString();
                            txbCeR.Text = arrBangTraAlphaR[12, 1].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }

                    case "B20":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[13, 2].ToString();
                            txbCeR.Text = arrBangTraAlphaR[12, 2].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }

                    case "B25":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[13, 3].ToString();
                            txbCeR.Text = arrBangTraAlphaR[12, 3].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }

                    case "B30":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[13, 4].ToString();
                            txbCeR.Text = arrBangTraAlphaR[12, 4].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }
                }
            }
            else if (cbxLoaiThep.Text == "AII" & cbxHeSoLamViec.Text == "1.1")
            {
                switch (cbxLoaiBeTong.Text)
                {
                    case "B12.5":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[15, 0].ToString();
                            txbCeR.Text = arrBangTraAlphaR[14, 0].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }

                    case "B15":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[15, 1].ToString();
                            txbCeR.Text = arrBangTraAlphaR[14, 1].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }

                    case "B20":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[15, 2].ToString();
                            txbCeR.Text = arrBangTraAlphaR[14, 2].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }

                    case "B25":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[15, 3].ToString();
                            txbCeR.Text = arrBangTraAlphaR[14, 3].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }

                    case "B30":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[15, 4].ToString();
                            txbCeR.Text = arrBangTraAlphaR[14, 4].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }
                }
            }
            else if (cbxLoaiThep.Text == "AI" & cbxHeSoLamViec.Text == "1.1")
            {
                switch (cbxLoaiBeTong.Text)
                {
                    case "B12.5":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[17, 0].ToString();
                            txbCeR.Text = arrBangTraAlphaR[16, 0].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }

                    case "B15":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[17, 1].ToString();
                            txbCeR.Text = arrBangTraAlphaR[16, 1].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }

                    case "B20":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[17, 2].ToString();
                            txbCeR.Text = arrBangTraAlphaR[16, 2].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }

                    case "B25":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[17, 3].ToString();
                            txbCeR.Text = arrBangTraAlphaR[16, 3].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }

                    case "B30":
                        {
                            txbAlphaR.Text = arrBangTraAlphaR[17, 4].ToString();
                            txbCeR.Text = arrBangTraAlphaR[16, 4].ToString();
                            Global.AlphaR = float.Parse(txbAlphaR.Text);
                            Global.CeR = float.Parse(txbCeR.Text);
                            break;
                        }
                }
            }

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
