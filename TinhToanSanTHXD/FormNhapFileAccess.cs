using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TinhToanSanTHXD
{
    public partial class FormNhapFileAccess : Form
    {
        // Giờ mà muốn truyền thêm giá trị nào thì sửa "btnEtabsOK_Click" và "LoadData" bên FormNhapSoLieu.cs
        public delegate void TruyenChoFormDuLieu(string l1, string l2, string loaiBeTong, string chieuDaySan, string tinhTai, string hoatTai, string lopBeTongBaoVe);
        public TruyenChoFormDuLieu truyenData;
        DataGridViewCheckBoxColumn dgvcb = new DataGridViewCheckBoxColumn();
        public FormNhapFileAccess()
        {
            InitializeComponent();

            // Tạo cột check box trong datagridview
            dgvcb.HeaderText = "Chọn";
            dgvcb.Width = 30;

            dgvEtabs.Columns.Insert(0, dgvcb);

            dgvEtabs.DataSource = Global.dataTableHienThiL1L2.DefaultView;
        }
        // Hàm phụ trợ để đọc dữ liệu từ Etabs
        public DataTable ReadAccessDB(string filePath, string TenBang, DataTable dataTable)
        {
            string connectString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath;
            string query = "Select * from [" + TenBang + "]";
            OleDbConnection connect = new OleDbConnection(connectString);
            OleDbCommand command = new OleDbCommand(query, connect);
            connect.Open();
            OleDbDataReader dataReader = command.ExecuteReader();
            dataTable.Load(dataReader);
            return dataTable;
        }
        private void btnLoadData_Click(object sender, EventArgs e)
        {
            
            string filePath;
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "MS Access|*.mdb|All|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {

                filePath = fileDialog.FileName;
                //đọc bảng 
                DataTable dataTableFloorObject = new DataTable();
                DataTable dataTablePointObject = new DataTable();
                DataTable dataTableSlabProperty = new DataTable();
                DataTable dataTableAreaAssignmentsSection = new DataTable();
                DataTable dataTableAreaLoadAssignment = new DataTable();
                DataTable dataTableLoadPatternDefinitions = new DataTable();
                DataTable dataTableConcreteSlabDesignPreferences = new DataTable();

                dataTableFloorObject = ReadAccessDB(filePath, "Floor Object Connectivity", dataTableFloorObject);
                dataTablePointObject = ReadAccessDB(filePath, "Point Object Connectivity", dataTablePointObject);
                dataTableAreaAssignmentsSection = ReadAccessDB(filePath, "Area Assignments - Section Properties", dataTableAreaAssignmentsSection);
                dataTableSlabProperty = ReadAccessDB(filePath, "Slab Property Definitions", dataTableSlabProperty);
                dataTableAreaLoadAssignment = ReadAccessDB(filePath, "Area Load Assignments - Uniform", dataTableAreaLoadAssignment);
                dataTableLoadPatternDefinitions = ReadAccessDB(filePath, "Load Pattern Definitions", dataTableLoadPatternDefinitions);
                dataTableConcreteSlabDesignPreferences = ReadAccessDB(filePath, "Concrete Slab Design Preferences - ACI 318-14", dataTableConcreteSlabDesignPreferences);

                // Tạo các cột để hiển thị 
                DataColumn workCol1 = Global.dataTableHienThiL1L2.Columns.Add("Story", typeof(string));
                DataColumn workCol2 = Global.dataTableHienThiL1L2.Columns.Add("Tên sàn", typeof(string));
                DataColumn workCol3 = Global.dataTableHienThiL1L2.Columns.Add("Unique Name", typeof(string));
                DataColumn workCol4 = Global.dataTableHienThiL1L2.Columns.Add("L1 (m)", typeof(string));
                DataColumn workCol5 = Global.dataTableHienThiL1L2.Columns.Add("L2 (m)", typeof(string));
                DataColumn workCol6 = Global.dataTableHienThiL1L2.Columns.Add("Vật liệu", typeof(string));
                DataColumn workCol7 = Global.dataTableHienThiL1L2.Columns.Add("Bề dày sàn (m)", typeof(string));
                DataColumn workCol8 = Global.dataTableHienThiL1L2.Columns.Add("Tĩnh Tải (Kn/m2)", typeof(string));
                DataColumn workCol9 = Global.dataTableHienThiL1L2.Columns.Add("Hoạt Tải (Kn/m2)", typeof(string));
                DataColumn workCol10 = Global.dataTableHienThiL1L2.Columns.Add("bề dày bê tông bảo vệ(m)", typeof(string));

                #region lấy thông tin để vẽ
                // cái mới làm thêm
                System.Data.DataTable dataTableGridDefinitionsGridLines = new System.Data.DataTable();
                dataTableGridDefinitionsGridLines = ReadAccessDB(filePath, "Grid Definitions - Grid Lines", dataTableGridDefinitionsGridLines);
                System.Data.DataTable dataTableCollumnObjectConnectivity = new System.Data.DataTable();
                dataTableCollumnObjectConnectivity = ReadAccessDB(filePath, "Column Object Connectivity", dataTableCollumnObjectConnectivity);
                System.Data.DataTable dataTableFrameAssignmentsSectionProperties = new System.Data.DataTable();
                dataTableFrameAssignmentsSectionProperties = ReadAccessDB(filePath, "Frame Assignments - Section Properties", dataTableFrameAssignmentsSectionProperties);
                System.Data.DataTable dataTableFrameSectionPropertyDefinitionsConcreteRectangular = new System.Data.DataTable();
                dataTableFrameSectionPropertyDefinitionsConcreteRectangular = ReadAccessDB(filePath, "Frame Section Property Definitions - Concrete Rectangular", dataTableFrameSectionPropertyDefinitionsConcreteRectangular);
                System.Data.DataTable dataTableBeamObjectConnectivity = new System.Data.DataTable();
                dataTableBeamObjectConnectivity = ReadAccessDB(filePath, "Beam Object Connectivity", dataTableBeamObjectConnectivity);




                // Tạo các cột để hiển thị Bảng Grid
                System.Data.DataColumn bangGrid1 = Global.dataTableDrawGrid.Columns.Add("Trục", typeof(string));
                System.Data.DataColumn bangGrid2 = Global.dataTableDrawGrid.Columns.Add("Kí hiệu", typeof(string));
                System.Data.DataColumn bangGrid3 = Global.dataTableDrawGrid.Columns.Add("Khoảng cách", typeof(string));
                System.Data.DataColumn bangGrid4 = Global.dataTableDrawGrid.Columns.Add("Visible", typeof(string));

                // Tạo các cột để hiển thị Bảng Vẽ Mặt Bằng
                System.Data.DataColumn bangVeMatBang1 = Global.dataTableDrawMatBang.Columns.Add("Tầng", typeof(string));
                System.Data.DataColumn bangVeMatBang2 = Global.dataTableDrawMatBang.Columns.Add("Tên Ô sàn", typeof(string));
                System.Data.DataColumn bangVeMatBang3 = Global.dataTableDrawMatBang.Columns.Add("X1", typeof(string));
                System.Data.DataColumn bangVeMatBang4 = Global.dataTableDrawMatBang.Columns.Add("Y1", typeof(string));
                System.Data.DataColumn bangVeMatBang5 = Global.dataTableDrawMatBang.Columns.Add("X2", typeof(string));
                System.Data.DataColumn bangVeMatBang6 = Global.dataTableDrawMatBang.Columns.Add("Y2", typeof(string));
                System.Data.DataColumn bangVeMatBang7 = Global.dataTableDrawMatBang.Columns.Add("X3", typeof(string));
                System.Data.DataColumn bangVeMatBang8 = Global.dataTableDrawMatBang.Columns.Add("Y3", typeof(string));
                System.Data.DataColumn bangVeMatBang9 = Global.dataTableDrawMatBang.Columns.Add("X4", typeof(string));
                System.Data.DataColumn bangVeMatBang10 = Global.dataTableDrawMatBang.Columns.Add("Y4", typeof(string));

                // Tạo các cột để hiển thị Bảng Vẽ Cột
                System.Data.DataColumn bangVeCot1 = Global.dataTableDrawColumn.Columns.Add("Tầng", typeof(string));
                System.Data.DataColumn bangVeCot2 = Global.dataTableDrawColumn.Columns.Add("Tên Cột", typeof(string));
                System.Data.DataColumn bangVeCot3 = Global.dataTableDrawColumn.Columns.Add("T/độ x tâm", typeof(string));
                System.Data.DataColumn bangVeCot4 = Global.dataTableDrawColumn.Columns.Add("T/độ y tâm", typeof(string));
                System.Data.DataColumn bangVeCot5 = Global.dataTableDrawColumn.Columns.Add("Chiều cao", typeof(string));
                System.Data.DataColumn bangVeCot6 = Global.dataTableDrawColumn.Columns.Add("Chiều rộng", typeof(string));

                // Tạo các cột để hiển thị Bảng Vẽ Dầm
                System.Data.DataColumn bangVeDam1 = Global.dataTableDrawBeam.Columns.Add("Tầng", typeof(string));
                System.Data.DataColumn bangVeDam2 = Global.dataTableDrawBeam.Columns.Add("Tên Dầm", typeof(string));
                System.Data.DataColumn bangVeDam3 = Global.dataTableDrawBeam.Columns.Add("X đầu 1", typeof(string));
                System.Data.DataColumn bangVeDam4 = Global.dataTableDrawBeam.Columns.Add("Y đầu 1", typeof(string));
                System.Data.DataColumn bangVeDam5 = Global.dataTableDrawBeam.Columns.Add("X đầu 2", typeof(string));
                System.Data.DataColumn bangVeDam6 = Global.dataTableDrawBeam.Columns.Add("Y đầu 2", typeof(string));
                System.Data.DataColumn bangVeDam7 = Global.dataTableDrawBeam.Columns.Add("Chiều cao", typeof(string));
                System.Data.DataColumn bangVeDam8 = Global.dataTableDrawBeam.Columns.Add("Chiều rộng", typeof(string));

                // lấy số liệu vẽ lưới 
                for (int i = 0; i <= dataTableGridDefinitionsGridLines.Rows.Count - 1; i++)
                {
                    DataRow dr = Global.dataTableDrawGrid.NewRow();
                    dr[0] = dataTableGridDefinitionsGridLines.Rows[i][1].ToString(); // Grid Line Type
                    dr[1] = dataTableGridDefinitionsGridLines.Rows[i][2].ToString(); // ID
                    dr[2] = dataTableGridDefinitionsGridLines.Rows[i][3].ToString(); // Khoảng cách
                    dr[3] = dataTableGridDefinitionsGridLines.Rows[i][5].ToString(); // Visible
                    Global.dataTableDrawGrid.Rows.Add(dr);

                }

                // lấy số liệu vẽ cột
                for (int i = 0; i <= dataTableCollumnObjectConnectivity.Rows.Count - 1; i++)
                {
                    DataRow dr = Global.dataTableDrawColumn.NewRow();
                    dr[0] = dataTableCollumnObjectConnectivity.Rows[i][1].ToString(); //  Story
                    dr[1] = dataTableCollumnObjectConnectivity.Rows[i][2].ToString(); //  Tên cột

                    int toado1 = 0;
                    // Tìm X và Y của tọa độ điểm tâm
                    while (dataTablePointObject.Rows[toado1][0] != null)
                    {
                        if (dataTablePointObject.Rows[toado1][0].ToString() == dataTableCollumnObjectConnectivity.Rows[i][4].ToString())
                        {
                            break;
                        }
                        toado1++;
                    }
                    dr[2] = dataTablePointObject.Rows[toado1][5].ToString(); //  X tâm
                    dr[3] = dataTablePointObject.Rows[toado1][6].ToString(); //  Y tâm

                    int toado2 = 0;
                    // Tìm Height và Width
                    while (dataTableFrameAssignmentsSectionProperties.Rows[toado2][0] != null)
                    {
                        if (dataTableFrameAssignmentsSectionProperties.Rows[toado2][0].ToString() == dataTableCollumnObjectConnectivity.Rows[i][1].ToString()
                            && dataTableFrameAssignmentsSectionProperties.Rows[toado2][1].ToString() == dataTableCollumnObjectConnectivity.Rows[i][2].ToString())
                        {
                            break;
                        }
                        toado2++;
                    }

                    int toado3 = 0;
                    while (dataTableFrameAssignmentsSectionProperties.Rows[toado3][0] != null)
                    {
                        if (dataTableFrameSectionPropertyDefinitionsConcreteRectangular.Rows[toado3][0].ToString() == dataTableFrameAssignmentsSectionProperties.Rows[toado2][5].ToString())
                        {
                            break;
                        }
                        toado3++;
                    }

                    dr[4] = dataTableFrameSectionPropertyDefinitionsConcreteRectangular.Rows[toado3][3].ToString(); //  Height
                    dr[5] = dataTableFrameSectionPropertyDefinitionsConcreteRectangular.Rows[toado3][4].ToString(); //  Width

                    Global.dataTableDrawColumn.Rows.Add(dr);

                }


                // lấy số liệu vẽ dầm
                for (int i = 0; i <= dataTableBeamObjectConnectivity.Rows.Count - 1; i++)
                {
                    DataRow dr = Global.dataTableDrawBeam.NewRow();
                    dr[0] = dataTableBeamObjectConnectivity.Rows[i][1].ToString(); //  Story
                    dr[1] = dataTableBeamObjectConnectivity.Rows[i][2].ToString(); //  Tên dầm

                    int toado1 = 0;
                    // Tìm X và Y của tọa độ điểm tâm
                    while (dataTablePointObject.Rows[toado1][0] != null)
                    {
                        if (dataTablePointObject.Rows[toado1][0].ToString() == dataTableBeamObjectConnectivity.Rows[i][3].ToString())
                        {
                            break;
                        }
                        toado1++;
                    }
                    dr[2] = dataTablePointObject.Rows[toado1][5].ToString(); //  X đầu 1
                    dr[3] = dataTablePointObject.Rows[toado1][6].ToString(); //  Y đầu 1

                    int toado2 = 0;
                    // Tìm X và Y của tọa độ điểm tâm
                    while (dataTablePointObject.Rows[toado2][0] != null)
                    {
                        if (dataTablePointObject.Rows[toado2][0].ToString() == dataTableBeamObjectConnectivity.Rows[i][4].ToString())
                        {
                            break;
                        }
                        toado2++;
                    }
                    dr[4] = dataTablePointObject.Rows[toado2][5].ToString(); //  X đầu 2
                    dr[5] = dataTablePointObject.Rows[toado2][6].ToString(); //  Y đầu 2

                    int toado3 = 0;
                    // Tìm Height và Width
                    try
                    {
                        while (dataTableFrameAssignmentsSectionProperties.Rows[toado3][0] != null)
                        {
                            if (dataTableFrameAssignmentsSectionProperties.Rows[toado3][0].ToString() == dataTableBeamObjectConnectivity.Rows[i][1].ToString()
                                && dataTableFrameAssignmentsSectionProperties.Rows[toado3][1].ToString() == dataTableBeamObjectConnectivity.Rows[i][2].ToString()
                                && dataTableFrameAssignmentsSectionProperties.Rows[toado3][3].ToString() == "Concrete Rectangular")
                            {
                                break;
                            }
                            toado3++;
                        }
                    }
                    catch (System.Exception)
                    {


                    }


                    int toado4 = 0;
                    try
                    {
                        while (dataTableFrameAssignmentsSectionProperties.Rows[toado4][0] != null)
                        {
                            if (dataTableFrameSectionPropertyDefinitionsConcreteRectangular.Rows[toado4][0].ToString() == dataTableFrameAssignmentsSectionProperties.Rows[toado3][5].ToString())
                            {
                                break;
                            }
                            toado4++;
                        }
                    }
                    catch (System.Exception)
                    {


                    }


                    dr[6] = dataTableFrameSectionPropertyDefinitionsConcreteRectangular.Rows[toado4][3].ToString(); //  Height
                    dr[7] = dataTableFrameSectionPropertyDefinitionsConcreteRectangular.Rows[toado4][4].ToString(); //  Width

                    Global.dataTableDrawBeam.Rows.Add(dr);

                }

                // lấy số liệu vẽ ô sàn
                for (int i = 0; i <= dataTableFloorObject.Rows.Count - 1; i++)
                {
                    int toado1 = 0;
                    int toado2 = 0;
                    int toado3 = 0;
                    int toado4 = 0;

                    // Tìm X và Y của tọa độ điểm 1
                    while (dataTablePointObject.Rows[toado1][0] != null)
                    {
                        if (dataTablePointObject.Rows[toado1][0].ToString() == dataTableFloorObject.Rows[i][3].ToString())
                        {
                            break;
                        }
                        toado1++;
                    }
                    // Tìm X và Y của tọa độ điểm 2
                    while (dataTablePointObject.Rows[toado2][0] != null)
                    {
                        if (dataTablePointObject.Rows[toado2][0].ToString() == dataTableFloorObject.Rows[i][4].ToString())
                        {
                            break;
                        }
                        toado2++;

                    }
                    // Tìm X và Y của tọa độ điểm 3
                    while (dataTablePointObject.Rows[toado3][0] != null)
                    {
                        if (dataTablePointObject.Rows[toado3][0].ToString() == dataTableFloorObject.Rows[i][5].ToString())
                        {
                            break;
                        }
                        toado3++;
                    }
                    // Tìm X và Y của tọa độ điểm 4
                    while (dataTablePointObject.Rows[toado4][0] != null)
                    {
                        if (dataTablePointObject.Rows[toado4][0].ToString() == dataTableFloorObject.Rows[i][6].ToString())
                        {
                            break;
                        }
                        toado4++;

                    }

                    DataRow dr = Global.dataTableDrawMatBang.NewRow();
                    dr[0] = dataTableFloorObject.Rows[i][1].ToString();
                    dr[1] = dataTableFloorObject.Rows[i][2].ToString();

                    dr[2] = dataTablePointObject.Rows[toado1][5].ToString();
                    dr[3] = dataTablePointObject.Rows[toado1][6].ToString();
                    dr[4] = dataTablePointObject.Rows[toado2][5].ToString();
                    dr[5] = dataTablePointObject.Rows[toado2][6].ToString();
                    dr[6] = dataTablePointObject.Rows[toado3][5].ToString();
                    dr[7] = dataTablePointObject.Rows[toado3][6].ToString();
                    dr[8] = dataTablePointObject.Rows[toado4][5].ToString();
                    dr[9] = dataTablePointObject.Rows[toado4][6].ToString();
                    Global.dataTableDrawMatBang.Rows.Add(dr);

                }

                #endregion



                // Duyệt qua bảng Load Pattern Definitions để lấy về tên của hoạt tải và tĩnh tải -- có thể bỏ bên ngoài vòng for
                string TenTinhTai = null;
                string TenHoatTai = null;
                for (int f = 0; f < dataTableLoadPatternDefinitions.Rows.Count; f++)
                {
                    if (dataTableLoadPatternDefinitions.Rows[f][2].ToString() == "Dead")
                    {
                        TenTinhTai = dataTableLoadPatternDefinitions.Rows[f][0].ToString();
                    }

                    if (dataTableLoadPatternDefinitions.Rows[f][2].ToString() == "Live")
                    {
                        TenHoatTai = dataTableLoadPatternDefinitions.Rows[f][0].ToString();
                    }
                }



                // Các biến a,b,c,d,.. được dùng bên đưới để lưu giá trị thôi
                for (int i = 0; i <= dataTableFloorObject.Rows.Count - 1; i++)
                {
                    // lấy về hàng chứa các giá trị để tính l1 , l2
                    int a = 0;
                    int b = 0;
                    int c = 0;
                    double l1;
                    double l2;
                    // duyệt qua các cột trong file Etabs xuất ra 
                    while (dataTablePointObject.Rows[a][0] != null)
                    {
                        if (dataTablePointObject.Rows[a][0].ToString() == dataTableFloorObject.Rows[i][3].ToString())
                        {
                            break;
                        }
                        a++;
                    }

                    while (dataTablePointObject.Rows[b][0] != null)
                    {
                        if (dataTablePointObject.Rows[b][0].ToString() == dataTableFloorObject.Rows[i][4].ToString())
                        {
                            break;
                        }
                        b++;
                    }

                    while (dataTablePointObject.Rows[c][0] != null)
                    {
                        if (dataTablePointObject.Rows[c][0].ToString() == dataTableFloorObject.Rows[i][6].ToString())
                        {
                            break;
                        }
                        c++;
                    }
                    l1 = Math.Round(Math.Abs(double.Parse(dataTablePointObject.Rows[b][5].ToString())
                        - double.Parse(dataTablePointObject.Rows[a][5].ToString())),3);
                    l2 = Math.Round(Math.Abs(double.Parse(dataTablePointObject.Rows[c][6].ToString())
                        - double.Parse(dataTablePointObject.Rows[a][6].ToString())),3);

                    // Hoán đổi L1, L2 để xem cái nào lớn hơn
                    double tam;
                    if (l1 > l2)
                    {
                        tam = l2;
                        l2 = l1;
                        l1 = tam;
                    }

                    // lấy về vị trị hàng chứa Loại bê tông và chiều dày của sàn
                    int d = 0;
                    while (dataTableSlabProperty.Rows[d][0] != null)
                    {
                        if (dataTableSlabProperty.Rows[d][0].ToString() == dataTableAreaAssignmentsSection.Rows[i][3].ToString())
                        {
                            break;
                        }
                        d++;
                    }



                    //  duyệt qua bảng Area Load Assignment để lấy về hàng chứa tĩnh tải
                    int g = 0;
                    while (dataTableAreaLoadAssignment.Rows[g][0] != null)
                    {
                        if ((dataTableAreaLoadAssignment.Rows[g][2].ToString() == dataTableFloorObject.Rows[i][0].ToString()) && (dataTableAreaLoadAssignment.Rows[g][3].ToString() == TenTinhTai))
                        {
                            break;
                        }
                        g++;
                    }

                    //  duyệt qua bảng Area Load Assignment để lấy về hàng chứa hoạt tải
                    int h = 0;
                    while (dataTableAreaLoadAssignment.Rows[h][0] != null)
                    {
                        if ((dataTableAreaLoadAssignment.Rows[h][2].ToString() == dataTableFloorObject.Rows[i][0].ToString()) && (dataTableAreaLoadAssignment.Rows[h][3].ToString() == TenHoatTai))
                        {
                            break;
                        }
                        h++;
                    }

                    // duyệt qua bảng Conrete Slab Design để lấy về lớp bê tông bảo vệ
                    double LopBeTongBaoVe;
                    double CoverTop = double.Parse(dataTableConcreteSlabDesignPreferences.Rows[0][3].ToString());
                    double CoverBottom = double.Parse(dataTableConcreteSlabDesignPreferences.Rows[0][4].ToString());

                    if (CoverTop >= CoverBottom)
                    {
                        LopBeTongBaoVe = CoverTop;
                    }
                    else
                    {
                        LopBeTongBaoVe = CoverBottom;
                    }


                    // Nhập vào dataTable Hiển thị trong Form Etabs
                    DataRow dr = Global.dataTableHienThiL1L2.NewRow();
                    dr[0] = dataTableFloorObject.Rows[i][1].ToString(); // story
                    dr[1] = dataTableAreaAssignmentsSection.Rows[i][3].ToString(); // tên sàn
                    dr[2] = dataTableFloorObject.Rows[i][0].ToString(); // Unique Name
                    dr[3] = l1.ToString();
                    dr[4] = l2.ToString();
                    dr[5] = dataTableSlabProperty.Rows[d][3].ToString(); // vật liệu
                    dr[6] = dataTableSlabProperty.Rows[d][4].ToString(); // chiều dày sàn
                    dr[7] = dataTableAreaLoadAssignment.Rows[g][5].ToString(); // Tĩnh tải
                    dr[8] = dataTableAreaLoadAssignment.Rows[h][5].ToString(); // Tĩnh tải
                    dr[9] = LopBeTongBaoVe.ToString(); // Lớp bê tông bảo vệ

                    Global.dataTableHienThiL1L2.Rows.Add(dr);

                }
                Global.dataTableHienThiL1L2 = new DataTable();

            }

        }

        private void btnEtabsOK_Click(object sender, EventArgs e)
        {
            // đếm xem thử người dùng chọn bao nhiêu hàng
            int count = 0;
            foreach (DataGridViewRow item in dgvEtabs.Rows)
            {
                if (Convert.ToBoolean(item.Cells[dgvcb.Name].Value) == true)
                {
                    count++;
                }
            }

            if (count == 0)
            {
                Global.dataTableHienThiL1L2 = new DataTable();
                this.Close();
            }
            else if (count == 1) // Truyền dữ liệu qua Form nhập liệu
            {
                foreach (DataGridViewRow row in dgvEtabs.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[dgvcb.Name].Value) == true)
                    {
                        if (truyenData != null)
                        {
                            truyenData(row.Cells[4].Value.ToString(), row.Cells[5].Value.ToString()
                                , row.Cells[6].Value.ToString(), row.Cells[7].Value.ToString(), row.Cells[8].Value.ToString()
                                , row.Cells[9].Value.ToString(), row.Cells[10].Value.ToString());
                        }
                    }
                }

                Global.dataTableHienThiL1L2 = new DataTable();
                this.Close();
            }
            else
            {
                DataColumn workCol1 = Global.dataTableChonNhieuSan.Columns.Add("Story", typeof(string));
                DataColumn workCol2 = Global.dataTableChonNhieuSan.Columns.Add("Tên sàn", typeof(string));
                DataColumn workCol3 = Global.dataTableChonNhieuSan.Columns.Add("Unique Name", typeof(string));
                DataColumn workCol4 = Global.dataTableChonNhieuSan.Columns.Add("L1 (m)", typeof(string));
                DataColumn workCol5 = Global.dataTableChonNhieuSan.Columns.Add("L2 (m)", typeof(string));
                DataColumn workCol6 = Global.dataTableChonNhieuSan.Columns.Add("Vật liệu", typeof(string));
                DataColumn workCol7 = Global.dataTableChonNhieuSan.Columns.Add("Bề dày sàn (m)", typeof(string));
                DataColumn workCol8 = Global.dataTableChonNhieuSan.Columns.Add("Tĩnh Tải (Kn/m2)", typeof(string));
                DataColumn workCol9 = Global.dataTableChonNhieuSan.Columns.Add("Hoạt Tải (Kn/m2)", typeof(string));
                DataColumn workCol10 = Global.dataTableChonNhieuSan.Columns.Add("Bề dày bê tông bảo vệ(m)", typeof(string));
                foreach (DataGridViewRow row in dgvEtabs.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[dgvcb.Name].Value) == true)
                    {
                        DataRow dr = Global.dataTableChonNhieuSan.NewRow();
                        dr[0] = row.Cells[1].Value.ToString();// story
                        dr[1] = row.Cells[2].Value.ToString(); // ltên sàn
                        dr[2] = row.Cells[3].Value.ToString(); // uniqur
                        dr[3] = row.Cells[4].Value.ToString(); // l1
                        dr[4] = row.Cells[5].Value.ToString(); // l2
                        dr[5] = row.Cells[6].Value.ToString(); // vật liệu
                        dr[6] = row.Cells[7].Value.ToString(); //bề dày sàn
                        dr[7] = row.Cells[8].Value.ToString(); // tĩnh tải
                        dr[8] = row.Cells[9].Value.ToString(); // hoat tải
                        dr[9] = row.Cells[10].Value.ToString(); // bề dày lớp bê tông bảo vệ
                        Global.dataTableChonNhieuSan.Rows.Add(dr);

                    }
                }

                Global.dataTableHienThiL1L2 = new DataTable();
                Global.dataTableChonNhieuSan = new DataTable();
                Global.dataTableDrawBeam = new DataTable();
                Global.dataTableDrawColumn = new DataTable();
                Global.dataTableDrawGrid = new DataTable();
                Global.dataTableDrawMatBang = new DataTable();
                this.Close();
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Global.dataTableHienThiL1L2 = new DataTable();
            this.Close();
        }

        private void tsmiChonTatCaSan_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvEtabs.SelectedRows.Count; i++)
            {
                dgvEtabs.SelectedRows[i].Cells[0].Value = true;
            }
        }

        private void tsmiBoChonTatCaSan_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvEtabs.SelectedRows.Count; i++)
            {
                dgvEtabs.SelectedRows[i].Cells[0].Value = false;
            }
        }
    }
}
