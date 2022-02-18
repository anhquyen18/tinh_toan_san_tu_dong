using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;

namespace TinhToanSanTHXD
{

    public class LinkAutoCad
    {
        [CommandMethod("THXDTinhToanSan")]
        public void cmdShowForm()
        {
            FormTinhToan formTest = new FormTinhToan();
            formTest.ShowDialog();
        }

        [CommandMethod("THXDKhungTen")]
        public void cmdKhungTen()
        {
            // Get the current document and database
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;
            Editor editor = acDoc.Editor;
            // Get position of a point when user click
            var ppo = new PromptPointOptions("\nChọn vị trí đặt khung tên: ");
            var ppr = editor.GetPoint(ppo);
            if (ppr.Status != PromptStatus.OK)
                return;
            var basePt = ppr.Value;
            double x = double.Parse(basePt.X.ToString());
            double y = double.Parse(basePt.Y.ToString());
            // Start a transaction
            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                // Open the Block table for read
                BlockTable acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;

                // Open the Block table record Model space for write
                BlockTableRecord acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                // Vẽ hình chữ nhật bao bên ngoài cho khung tên
                using (Polyline acPoly = new Polyline())
                {
                    acPoly.AddVertexAt(0, new Point2d(x, y), 0, 0, 0);
                    acPoly.AddVertexAt(1, new Point2d(x, y + 297 * 200), 0, 0, 0);
                    acPoly.AddVertexAt(2, new Point2d(x + 420 * 200, y + 297 * 200), 0, 0, 0);
                    acPoly.AddVertexAt(3, new Point2d(x + 420 * 200, y), 0, 0, 0);
                    acPoly.AddVertexAt(4, new Point2d(x, y), 0, 0, 0);
                    // set color is red for polyline
                    acPoly.ColorIndex = 7;

                    // Add the new object to the block table record and the transaction
                    acBlkTblRec.AppendEntity(acPoly);
                    acTrans.AddNewlyCreatedDBObject(acPoly, true);

                }
                // Vẽ một Hình chữ nhật bao bên trong cho khung tên
                using (Polyline acPoly = new Polyline())
                {
                    acPoly.AddVertexAt(0, new Point2d(x + 5 * 200, y + 5 * 200), 0, 0, 0);
                    acPoly.AddVertexAt(1, new Point2d(x + 5 * 200, y + 292 * 200), 0, 0, 0);
                    acPoly.AddVertexAt(2, new Point2d(x + 415 * 200, y + 292 * 200), 0, 0, 0);
                    acPoly.AddVertexAt(3, new Point2d(x + 415 * 200, y + 5 * 200), 0, 0, 0);
                    acPoly.AddVertexAt(4, new Point2d(x + 5 * 200, y + 5 * 200), 0, 0, 0);

                    // chỉnh màu đỏ cho polyline
                    acPoly.ColorIndex = 2; // màu trắng

                    // Add the new object to the block table record and the transaction
                    acBlkTblRec.AppendEntity(acPoly);
                    acTrans.AddNewlyCreatedDBObject(acPoly, true);
                }

                // VẼ LINE KHUNG TÊN
                DrawLine(new Point3d(x + 365 * 200, y + 292 * 200, 0), new Point3d(x + 365 * 200, y + 5 * 200, 0), acBlkTblRec, acTrans, 2);
                DrawLine(new Point3d(x + 365 * 200, y + 272 * 200, 0), new Point3d(x + 415 * 200, y + 272 * 200, 0), acBlkTblRec, acTrans, 7);
                DrawLine(new Point3d(x + 365 * 200, y + 272 * 200 - 100 * 200, 0), new Point3d(x + 415 * 200, y + 272 * 200 - 100 * 200, 0), acBlkTblRec, acTrans, 7);

                double bientam1 = y + 272 * 200 - 100 * 200;
                for (int i = 0; i < 8; i++)
                {
                    bientam1 -= 15 * 200;
                    DrawLine(new Point3d(x + 365 * 200, bientam1, 0), new Point3d(x + 415 * 200, bientam1, 0), acBlkTblRec, acTrans, 7);
                    switch (i)
                    {
                        case 0:
                            CreateMText(new Point3d(x + 365 * 200 + 2 * 200, bientam1 + 2 * 200, 0), 90, "LẦN SỬA", 2 * 150, acBlkTblRec, acTrans);
                            break;
                        case 1:
                            CreateMText(new Point3d(x + 365 * 200 + 2 * 200, bientam1 + 14 * 200, 0), 0, "THIẾT KẾ, VẼ:", 2 * 150, acBlkTblRec, acTrans);
                            break;
                        case 2:
                            CreateMText(new Point3d(x + 365 * 200 + 2 * 200, bientam1 + 14 * 200, 0), 0, "KIỂM TRA:", 2 * 150, acBlkTblRec, acTrans);
                            break;
                        case 3:
                            CreateMText(new Point3d(x + 365 * 200 + 2 * 200, bientam1 + 14 * 200, 0), 0, "DỰ ÁN:", 2 * 150, acBlkTblRec, acTrans);
                            break;
                        case 4:
                            CreateMText(new Point3d(x + 365 * 200 + 2 * 200, bientam1 + 14 * 200, 0), 0, "BẢN VẼ:", 2 * 150, acBlkTblRec, acTrans);
                            break;
                        case 5:
                            CreateMText(new Point3d(x + 365 * 200 + 2 * 200, bientam1 + 14 * 200, 0), 0, "KÝ HIỆU BẢN VẼ:", 2 * 150, acBlkTblRec, acTrans);
                            break;
                        case 6:
                            CreateMText(new Point3d(x + 365 * 200 + 2 * 200, bientam1 + 14 * 200, 0), 0, "NGÀY PHÁT HÀNH:", 2 * 150, acBlkTblRec, acTrans);
                            break;
                        case 7:
                            CreateMText(new Point3d(x + 365 * 200 + 2 * 200, bientam1 + 14 * 200, 0), 0, "TỈ LỆ:", 2 * 150, acBlkTblRec, acTrans);
                            break;

                    }
                }


                DrawLine(new Point3d(x + 371.25 * 200, y + 157 * 200, 0), new Point3d(x + 371.25 * 200, y + 292 * 200, 0), acBlkTblRec, acTrans, 7);
                double bientam2 = x + 371.25 * 200;
                for (int i = 0; i < 7; i++)
                {
                    bientam2 += 6.25 * 200;
                    DrawLine(new Point3d(bientam2, y + 157 * 200, 0), new Point3d(bientam2, y + 292 * 200, 0), acBlkTblRec, acTrans, 7);
                }
                CreateMText(new Point3d(x + 365 * 200 + 2 * 200, y + 272 * 200 + 6 * 200, 0), 90, "NGÀY", 2 * 150, acBlkTblRec, acTrans);

                CreateMText(new Point3d(x + 365 * 200 + 2 * 200, y + 272 * 200 - 65 * 200, 0), 90, "NỘI DUNG SỬA CHỮA", 2 * 150, acBlkTblRec, acTrans);

                // Save the new object to the database
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
            
            acMText.TextHeight = textHeight;
            acMText.Rotation = rotation * (Math.PI / 180); // máy hiểu theo radian nên mình phải chuyển về độ
            acMText.Contents = content;
            acBlkTblRec.AppendEntity(acMText);
            acTrans.AddNewlyCreatedDBObject(acMText, true);
        }
    }
}
