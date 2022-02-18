using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinhToanSanTHXD
{
	class San
	{
		private int uniqueName;
		private string tenSan;
		private double l1;
		private double l2;
		private double hoatTai;
		private double tinhTai;
		private double chieuDayH;
		private double chieuRongDaiBan = 1000;
		private double lopBTBV;
		private double chieuDayH0;
		private string loaiSan;
		private double taiPhanBo;
		private string tang;
		private string loaiBeTong;
		private string thep;
		public string TenSan
		{
			get
			{
				return tenSan;
			}
			set
			{
				tenSan = value;
			}
		}
		public double TaiPhanBo
		{
			get
			{
				return TinhTai + HoatTai;
			}
			set
			{
				taiPhanBo = value;
			}
		}

		public double ChieuDayH0
		{
			get
			{
				return ChieuDayH - LopBTBV;
			}
			set
			{
				chieuDayH0 = value;
			}
		}
		public string LoaiSan
		{
			get
			{
				if (l2 / L1 < 2)
					return "Bản kê 4 cạnh";
				else
					return "Bản loại dầm";
			}
			set
			{
				loaiSan = value;
			}
		}
		public double TySoL1L2
		{
			get
			{
				return Math.Round(l2 / L1, 3);
			}
		}

		public double L1 { get => l1; set => l1 = value; }
		public double L2 { get => l2; set => l2 = value; }
		public double HoatTai { get => hoatTai; set => hoatTai = value; }
		public double TinhTai { get => tinhTai; set => tinhTai = value; }
		public double ChieuDayH { get => chieuDayH * 1000; set => chieuDayH = value; }
		public double ChieuRongDaiBan { get => chieuRongDaiBan; set => chieuRongDaiBan = value; }
		public double LopBTBV { get => lopBTBV * 1000; set => lopBTBV = value; }
		public string Tang { get => tang; set => tang = value; }
		public string LoaiBeTong { get => loaiBeTong; set => loaiBeTong = value; }
		public int UniqueName { get => uniqueName; set => uniqueName = value; }
		public string Thep { get => thep; set => thep = value; }

		// PHÂN LOẠI BÊ TÔNG
		// 12.5 15 20 22.5 25 30
		private double[] arrRb = new[] { 7.5, 8.5, 11.5, 13, 14.5, 17 };
		private double[] arrRbt = new[] { 0.66, 0.75, 0.9, 0.975, 1.2 };
		private int[] arrEb = new[] { 21000, 23000, 27000, 28500, 30000, 32500 };
		public double Rb
		{
			get
			{
				if (loaiBeTong.Equals("B12.5") || loaiBeTong.Equals("M150"))
					return arrRb[0];
				if (loaiBeTong.Equals("B15") || loaiBeTong.Equals("M200"))
					return arrRb[1];
				if (loaiBeTong.Equals("B20") || loaiBeTong.Equals("M250"))
					return arrRb[2];
				if (loaiBeTong.Equals("B22.5") || loaiBeTong.Equals("M300"))
					return arrRb[3];
				if (loaiBeTong.Equals("B25"))
					return arrRb[4];
				if (loaiBeTong.Equals("B30") || loaiBeTong.Equals("M400"))
					return arrRb[5];
				return 0.0;
			}
		}
		public double Rbt
		{
			get
			{
				if (loaiBeTong.Equals("B12.5") || loaiBeTong.Equals("M150"))
					return arrRbt[0];
				if (loaiBeTong.Equals("B15") || loaiBeTong.Equals("M200"))
					return arrRbt[1];
				if (loaiBeTong.Equals("B20") || loaiBeTong.Equals("M250"))
					return arrRbt[2];
				if (loaiBeTong.Equals("B22.5") || loaiBeTong.Equals("M300"))
					return arrRbt[3];
				if (loaiBeTong.Equals("B25"))
					return arrRbt[4];
				if (loaiBeTong.Equals("B30") || loaiBeTong.Equals("M400"))
					return arrRbt[5];
				return 0.0;
			}
		}

		public double Eb
		{
			get
			{
				if (loaiBeTong.Equals("B12.5") || loaiBeTong.Equals("M150"))
					return arrEb[0];
				if (loaiBeTong.Equals("B15") || loaiBeTong.Equals("M200"))
					return arrEb[1];
				if (loaiBeTong.Equals("B20") || loaiBeTong.Equals("M250"))
					return arrEb[2];
				if (loaiBeTong.Equals("B22.5") || loaiBeTong.Equals("M300"))
					return arrEb[3];
				if (loaiBeTong.Equals("B25"))
					return arrEb[4];
				if (loaiBeTong.Equals("B30") || loaiBeTong.Equals("M400"))
					return arrEb[5];
				return 0.0;
			}
		}


	}
}
