using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace TinhToanSanTHXD
{
    class Program
    {
    }

    static class Global
    {
        public static DataTable dataTableHienThiL1L2 = new DataTable("HienThiL1L2");
        public static DataTable dataTableChonNhieuSan = new DataTable("ChonNhieuSan");

        public static DataTable dataTableDrawGrid = new DataTable("VeGrid");
        public static DataTable dataTableDrawMatBang = new DataTable("VeOSan");
        public static DataTable dataTableDrawColumn = new DataTable("VeCot");
        public static DataTable dataTableDrawBeam = new DataTable("VeDam");

        public static double Rb;
        public static double Rbt;
        public static double Eb;

        public static double Rs;
        public static double Rsc;
        public static double Es;

        public static double AlphaR;
        public static double CeR;

        //public static double ChieuDaiODan;
        //public static double LopBeTongBaoVe;
        //public static double ChieuRongDaiBan;
        //public static double ChieuCaoLamViec;
        //public static double ChieuDaiL1;
        //public static double ChieuDaiL2;
        //public static string LoaiOSan;
        //public static double TinhTai;
        //public static double HoatTai;
        //public static double TaiTrongPhanBoDeu;


    }
}
