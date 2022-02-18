using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;

namespace TinhToanSanTHXD
{
    public class LoadDaTaTTL
    {
        [CommandMethod("THXDLOADDATA")]

        static public void cmdLoadData()

        {
            // Load 2 cái kiểu Linetype vào Autocad
            // nét gạch gạch _ _ _ _ _ _
            LoadLinetype("DASHED");
            // nét gạch chấm _._._._.
            LoadLinetype("DASHDOT");

            // tạo biến lưu cái id của Linetype để sử dụng
            ObjectId ltIdDASHED = ObjectId.Null;
            ObjectId ltIdDASHDOT = ObjectId.Null;

            // Cách lấy về
            //ltIdDASHED = GetLinestyleID("DASHED");


            // Tạo 1 textstyle có tên, tên font trong location của autocad,có góc là bằng 0,
            // có width factor là 1,
            // 1 là check annotative or 0 là kh check annotative
            CreateTextStyle("THXD_TieuDe", "arial.ttf", 0, 1, 1);

            CreateTextStyle("THXD_Dim", "arial.ttf", 0, 1, 1);

            // tạo biến lưu cái id của textstyle để sử dụng
            ObjectId tsIdTieuDe = ObjectId.Null;
            ObjectId tsIdDim = ObjectId.Null;
            // Cách lấy về
            //tsIdTieuDe = GetTextStyleId("THXD_TieuDe");


            //Tạo DimStyle
            CreateModifyDimStyle("THXD_1-30", 0.3);
            CreateModifyDimStyle("THXD_1-50", 0.5);
            CreateModifyDimStyle("THXD_1-100", 1);

            // trang web màu cho autocad https://gohtx.com/acadcolors.php
            // trang web set value for line weight
            // https://help.autodesk.com/view/OARX/2019/ENU/?guid=OREFNET-Autodesk_AutoCAD_DatabaseServices_LineWeight
            // Tạo các Layer
            AddLayer("THXD-Dim", -1, 8, "Continuous");
            AddLayer("THXD-Thep", -1, 1, "Continuous");
            AddLayer("THXD-Duong_Bao", -1, 4, "Continuous");
            AddLayer("THXD-Net_manh", 20, 3, "Continuous");
            AddLayer("THXD-Truc", -1, 8, "DASHED");
            AddLayer("THXD-Cot", -1, 1, "Continuous");
            AddLayer("THXD-Dam", -1, 4, "Continuous");

        }







        //--------------------------- CÁC HÀM -------------------------//
        // Hàm load các Linetype
        public static void LoadLinetype(string name)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            using (Transaction Tx = db.TransactionManager.StartTransaction())

            {
                LinetypeTable tbl = (LinetypeTable)Tx.GetObject(db.LinetypeTableId, OpenMode.ForRead);

                if (!tbl.Has(name))

                {
                    db.LoadLineTypeFile(name, "acad.lin");
                }

                Tx.Commit();

            }
        }

        // Hàm tạo các Text style
        public static void CreateTextStyle(string TextStyleName, string FontName, double ObliqueAngle, double WidthFactor, int Annotative_1or0)
        {

            using (Transaction transaction = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.TransactionManager.StartTransaction())
            {
                Database db = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Database;
                TextStyleTable tst1 = (TextStyleTable)transaction.GetObject(db.TextStyleTableId, OpenMode.ForWrite, true, true);

                if (!tst1.Has(TextStyleName))
                {
                    TextStyleTableRecord tstr1 = new TextStyleTableRecord();
                    // Tên của Text style
                    tstr1.Name = TextStyleName;
                    // Font của TextStyle
                    string filename = @"C:\Program Files\Autodesk\AutoCAD 2021\Fonts\" + FontName;
                    tstr1.FileName = filename;
                    // thuộc cái Width Factor
                    tstr1.XScale = WidthFactor;
                    // thuộc Oblique Angle
                    tstr1.ObliquingAngle = ObliqueAngle; // ??????? có thay đổi chỗ này
                                                         // Annotative
                    tstr1.Annotative = (AnnotativeStates)Annotative_1or0;
                    tst1.Add(tstr1);
                    transaction.TransactionManager.AddNewlyCreatedDBObject(tstr1, true);
                    transaction.Commit();
                }
                //RmTSid = tstr1.ObjectId;
                //return true;

            }

        }

        // hàm lầy vế cái Id của 1 TextStyle để sử dụng về sau
        public static ObjectId GetTextStyleId(string TextStyleName)
        {
            ObjectId result = ObjectId.Null;

            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Transaction tr = db.TransactionManager.StartTransaction();
            using (tr)
            {
                TextStyleTable ltt = (TextStyleTable)tr.GetObject(db.TextStyleTableId, OpenMode.ForRead);
                result = ltt[TextStyleName];
                tr.Commit();
            }

            return result;
        }

        //Hàm lầy vế cái Id của 1 Linetype để sử dụng về sau

        public static ObjectId GetLinestyleID(string LineStyleName)
        {
            ObjectId result = ObjectId.Null;

            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Transaction tr = db.TransactionManager.StartTransaction();
            using (tr)
            {
                LinetypeTable ltt = (LinetypeTable)tr.GetObject(db.LinetypeTableId, OpenMode.ForRead);
                result = ltt[LineStyleName];
                tr.Commit();
            }

            return result;
        }

        //Hàm tạo các Layers
        public static void AddLayer(string name, double lineweight, int color, string Linetype)
        {
            var doc = Application.DocumentManager.MdiActiveDocument;
            var db = doc.Database;
            ObjectId idx;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                var layerTable = (LayerTable)tr.GetObject(db.LayerTableId, OpenMode.ForRead);
                if (layerTable.Has(name))
                {
                    idx = layerTable[name];
                }
                else
                {
                    var layer = new LayerTableRecord();

                    layer.Color = Color.FromColorIndex(ColorMethod.ByAci, (short)color);

                    try { layer.LineWeight = (LineWeight)(int)(lineweight); }
                    catch { layer.LineWeight = LineWeight.ByLayer; }

                    layer.Name = name;

                    ObjectId ltId = GetLinestyleID(Linetype);
                    layer.LinetypeObjectId = ltId;
                    layerTable.UpgradeOpen();
                    idx = layerTable.Add(layer);
                    tr.AddNewlyCreatedDBObject(layer, true);
                }
                db.Clayer = idx;
                tr.Commit();

            }
        }

        // Hàm tạo các DIMSTYLE
        public static void CreateModifyDimStyle(string DimStyleName, double TiLe)
        {

            using (Transaction tr = Application.DocumentManager.MdiActiveDocument.TransactionManager.StartTransaction())
            {
                Database db = Application.DocumentManager.MdiActiveDocument.Database;
                // lấy về danh sách các DimStyle
                DimStyleTable dst = (DimStyleTable)tr.GetObject(db.DimStyleTableId, OpenMode.ForWrite, true);

                // Initialise a DimStyleTableRecord
                DimStyleTableRecord dstr = null;
                // If the required dimension style exists
                if (dst.Has(DimStyleName))
                {
                    // get the dimension style table record open for writing
                    dstr = (DimStyleTableRecord)tr.GetObject(dst[DimStyleName], OpenMode.ForWrite);
                }
                else
                    // Initialise as a new dimension style table record
                    dstr = new DimStyleTableRecord();

                // Set all the available dimension style properties
                // Most/all of these match the variables in AutoCAD.
                // Tên của DímStyle
                dstr.Name = DimStyleName;
                // Set Annotative

                // ý nghĩa các từ bên dưới
                // https://topic.alibabacloud.com/a/list-of-properties-for-autocad-net-api-dimstyletablerecord_8_8_30858547.html

                //**Lines
                //---Dimension lines:
                //Color
                dstr.Dimclrd = Autodesk.AutoCAD.Colors.Color.FromColorIndex(ColorMethod.ByAci, 7);
                //Linetype
                ObjectId ltId = GetLinestyleID("Continuous"); // Wow
                dstr.Dimltype = ltId;
                // Line weight
                dstr.Dimlwd = LineWeight.LineWeight025;
                //Base line spacing
                dstr.Dimdli = 7;
                //Suppress
                dstr.Dimsd1 = false;
                dstr.Dimsd2 = false;

                //-- Extension lines
                //Color:
                dstr.Dimclre = Autodesk.AutoCAD.Colors.Color.FromColorIndex(ColorMethod.ByAci, 7);

                //Linetype ext line 1
                dstr.Dimltex1 = ltId;
                //Linetype ext line 2               
                dstr.Dimltex2 = ltId;
                //Extend beyond dim lines
                dstr.Dimexe = 1;
                //offset from origin
                dstr.Dimexo = 2;
                //fixed length extension line
                dstr.DimfxlenOn = false;
                // Suppress: Ext line 1
                dstr.Dimse1 = false;
                // Suppress: Ext line 2            
                dstr.Dimse2 = false;

                //**Symbols and Arrows
                //-- Arrowheads
                //First
                dstr.Dimblk1 = ObjectId.Null;
                //Second
                dstr.Dimblk2 = ObjectId.Null;
                //Leader
                dstr.Dimldrblk = ObjectId.Null;
                //Arrow size
                dstr.Dimasz = 3.5;
                //--Center marks
                dstr.Dimcen = 0.09;
                //--Dimension break
                //không thấy có
                //--Arc length symbol
                dstr.Dimarcsym = 0;
                //-- Radius joy dimension
                dstr.Dimjogang = 0;
                //--Linear joy dimension

                //**Text
                //--Text appearance
                //Textstyle
                ObjectId tsId = ObjectId.Null;
                tsId = GetTextStyleId("THXD_Dim");
                dstr.Dimtxsty = tsId;
                //Text color
                dstr.Dimclrt = Autodesk.AutoCAD.Colors.Color.FromColorIndex(ColorMethod.ByAci, 2);
                //Fill color
                dstr.Dimtfill = 0;
                dstr.Dimtfillclr = Autodesk.AutoCAD.Colors.Color.FromColorIndex(ColorMethod.ByAci, 0);
                //text Height
                dstr.Dimtxt = 3.5;
                //Fraction height scale

                //Draw frame around text
                dstr.Dimgap = 1;
                //--text placement
                //Vertical
                dstr.Dimtad = 2;
                //Horizontal
                dstr.Dimjust = 0;
                //View Direction
                dstr.Dimtxtdirection = false;
                //Offset from dim line

                // text alignment

                //** Fit
                //-- Fit options
                // 5 cái ở trên
                dstr.Dimatfit = 3;
                //Suppress arrows if they ...
                dstr.Dimsoxd = false;
                //Text placement
                dstr.Dimtmove = 0;
                //--Scale for dimension features
                //Annotative
                dstr.Annotative = AnnotativeStates.False;
                //Scale dimension
                dstr.Dimscale = 1;
                //--Fine tuning
                //Place text manually
                dstr.Dimupt = false;
                //Draw dim line between ext lines
                dstr.Dimtofl = false;

                //**Primary Units
                //--Linear dimensions
                //Unit format
                dstr.Dimlunit = 2;
                //precision
                dstr.Dimdec = 4;
                //Fraction format

                //Decimal separator
                dstr.Dimdsep = Convert.ToChar(",");
                //round off
                dstr.Dimrnd = 0;
                //prefix //suffix
                dstr.Dimpost = "";
                //--Measurement scale
                dstr.Dimlfac = TiLe;
                //--Zero suppression

                //--Angular dimensions
                // unit format
                dstr.Dimaunit = 0;
                //precision
                dstr.Dimadec = 2;
                //--zero suppression


                // Kiểm soát xem có hiển thị 0 của giá trị chú thích Đơn vị chuyển đổi hay không
                dstr.Dimalt = false;
                // Kiểm soát số chữ số thập phân trong đơn vị thay thế
                dstr.Dimaltd = 3;
                //Kiểm soát hệ số tỷ lệ trong các đơn vị thay thế
                dstr.Dimaltf = 25.4;
                //Xác định làm tròn cho các đơn vị thay thế
                dstr.Dimaltrnd = 0;
                // Đặt số chữ số để chuyển đổi kích thước Đơn vị Dung sai Giá trị vị trí thập phân
                dstr.Dimalttd = 2;
                // Kiểm soát xem giá trị dung sai có được xử lý bằng 0 hay không
                dstr.Dimalttz = 0;
                // Đặt định dạng đơn vị cho tất cả các họ kiểu thứ nguyên (ngoại trừ chú thích góc) các đơn vị thay thế
                dstr.Dimaltu = 2;
                //Kiểm soát xem giá trị chú thích của đơn vị chuyển đổi có được xử lý không.
                dstr.Dimaltz = 0;
                //Chỉ định tiền tố hoặc hậu tố văn bản (hoặc cả hai) cho các phép đo kích thước chuyển đổi
                //cho tất cả các loại kích thước (ngoại trừ kích thước góc)
                dstr.Dimapost = "TTL";
                // 0 Xử lý chú thích góc
                dstr.Dimazin = 2;
                // Đặt các khối mũi tên xuất hiện ở cuối đường kích thước hoặc đường dẫn
                dstr.Dimblk = ObjectId.Null;
                // Đặt khoảng cách của đường kích thước ngoài đường nhân chứng khi sử dụng dấu gạch chéo nhỏ thay vì mũi tên để ghi nhãn
                dstr.Dimdle = 0;
                // Kiểm soát khoảng cách của các đường kích thước trong chú thích đường cơ sở
                dstr.Dimdli = 7;
                // Đặt định dạng phân số khi Dimlunit được đặt thành 4 (tòa nhà) hoặc 5 (phân số)
                dstr.Dimfrac = 0;
                // Đặt giá trị của phần mở rộng cố định
                dstr.Dimfxlen = 0.18;
                // Tạo thứ nguyên giới hạn làm văn bản mặc định
                dstr.Dimlim = false;

                // Chỉ định độ rộng dòng cho dòng nhân chứng
                dstr.Dimlwe = LineWeight.LineWeight025;
                // Kiểm soát việc hiển thị các khối mũi tên đường kích thước
                dstr.Dimsah = false;
                //Hiển thị kiểu thứ nguyên hiện tại
                dstr.Dimtdec = 2;
                //Kiểm soát vị trí dọc của văn bản so với đường kích thước
                dstr.Dimtfac = 1;
                //Kiểm soát vị trí của văn bản kích thước trong đường nhân chứng
                //cho tất cả các loại thứ nguyên ngoại trừ nhãn tọa độ
                dstr.Dimtih = false;
                //Vẽ văn bản giữa các dòng nhân chứng
                dstr.Dimtix = false;
                //Đặt độ lệch xuống tối đa cho văn bản thứ nguyên khi Dimtol hoặc Dimlim đang mở
                dstr.Dimtm = 0;
                //Kiểm soát vị trí văn bản thứ nguyên nằm ngoài dòng nhân chứng
                dstr.Dimtoh = false;
                //Thêm dung sai vào văn bản kích thước
                dstr.Dimtol = false;
                //Để đặt giá trị dung sai cho căn chỉnh dọc của văn bản kích thước danh nghĩa
                dstr.Dimtolj = 1;
                //Đặt độ lệch trên tối đa cho văn bản thứ nguyên khi Dimtol hoặc Dimlim được bật
                dstr.Dimtp = 0;
                // Chỉ định kích thước dấu gạch chéo nhỏ cho kích thước tuyến tính,
                // chú thích bán kính và mũi tên thay thế trong chú thích đường kính
                dstr.Dimtsz = 0;
                //Kiểm soát vị trí dọc của văn bản kích thước bên trên hoặc bên dưới đường kích thước
                dstr.Dimtvp = 0;

                // Test for the text style to be used

                //Kiểm soát xem giá trị dung sai có được xử lý bằng 0 hay không
                dstr.Dimtzin = 0;
                //Kiểm soát xem giá trị đơn vị chính là 0 có được xử lý hay không
                dstr.Dimzin = 0;

                // If the dimension style doesn't exist
                if (!dst.Has(DimStyleName))
                {
                    // Add it to the dimension style table and collect its Id
                    Object dsId = dst.Add(dstr);
                    // Add the new dimension style table record to the document
                    tr.AddNewlyCreatedDBObject(dstr, true);
                }


                DimStyleTableRecord DimTabbRecaord = (DimStyleTableRecord)tr.GetObject(dst[DimStyleName], OpenMode.ForRead);

                if (DimTabbRecaord.ObjectId != db.Dimstyle)

                {
                    db.Dimstyle = DimTabbRecaord.ObjectId;
                    // Set Dimstyle làm Current
                    db.SetDimstyleData(DimTabbRecaord);

                }


                // Commit the changes.
                tr.Commit();
            }
        }
    }
}
