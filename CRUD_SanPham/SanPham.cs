using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using static CRUD_SanPham.Bai6;

namespace CRUD_SanPham
{
    internal class Bai6
    {
        static void Main(string[] args)
        {
        }

        public Product CRUD;
        public SanPham _sp;

        [SetUp]
        public void Setup()
        {
            CRUD = new Product();
        }
        [Test]
        [TestCase("1", "SP1", "Oto", 1000.6f, "Black", "3m", 10)]//Mã chứa SP
        [TestCase("2", "2", "Abc", 1000.6f, "Black", "3m", 10)]//Không có SP
        [TestCase("3", "SP3", "", 0.0f, "", "",0)]//Rỗng
        [TestCase("10", "SP10", "XeMay", float.MaxValue, "Trắng", "3m", int.MaxValue)]//Biên
        public void ThemSP(string id, string maSanPham, string tenSanPham, float gia, string mauSac, string kichThuoc, int soluong)
        {
            _sp = new SanPham(id, maSanPham, tenSanPham, gia, mauSac, kichThuoc, soluong);
            CRUD.Them(_sp);
            Assert.That(CRUD.DanhSach().Contains(_sp), Is.True);
        }
        [Test]
        [TestCase("5", "SP5", "XeDap", 100.6f, "Đỏ", "3m", 20)]//Mã duy nhất
        public void ThemSP_Maduynhat(string id, string maSanPham, string tenSanPham, float gia, string mauSac, string kichThuoc, int soluong)
        {
            _sp = new SanPham("6", "SP5", "XYZ", 100.6f, "Đỏ", "3m", 20);
            CRUD.Them(_sp);
            Assert.Throws<Exception>(() => CRUD.Them(_sp));
        }
        [Test]
        [TestCase("1", "SP1", "Oto", 1000.6f, "Black", "3m", 10)]
        [TestCase("2", "2", "Abc", 1000.6f, "Black", "3m", 10)]//Không có SP
        public void SuaSP(string id, string newMaSanPham, string newTenSanPham, float newGia, string newMauSac, string newKichThuoc, int newSoLuong)
        {
            _sp = new SanPham("1", "SP3", "Abc", 1100.0f, "Red", "Medium", 20);
            CRUD.Them(_sp);
            var suaSP = new SanPham(id, newMaSanPham, newTenSanPham, newGia, newMauSac, newKichThuoc, newSoLuong);
            CRUD.Sua(id, suaSP);
            Assert.That(CRUD.DanhSach().Contains(_sp), Is.True);
        }
        [Test]
        [TestCase("SP3")]
        [TestCase("1")]
        public void XoaSP(string masp)
        {
            //_sp = new SanPham("1", masp, "Samsung", 1000.0f, "Black", "Large", 10);
            _sp = new SanPham("1", "SP3", "Samsung", 1000.0f, "Black", "Large", 10);
            CRUD.Them(_sp);
            CRUD.Xoa(masp);
            Assert.That(CRUD.DanhSach().Contains(_sp), Is.False);
        }
        public class SanPham
        {
            public string ID { get; set; }
            public string MaSanPham { get; set; }
            public string TenSanPham { get; set; }
            public float Gia { get; set; }
            public string MauSac { get; set; }
            public string KichThuoc { get; set; }
            public int SoLuong { get; set; }

            public SanPham(string id, string maSanPham, string tenSanPham, float gia, string mauSac, string kichThuoc, int soLuong)
            {
                ID = id;
                MaSanPham = maSanPham;
                TenSanPham = tenSanPham;
                Gia = gia;
                MauSac = mauSac;
                KichThuoc = kichThuoc;
                SoLuong = soLuong;
            }
        }

        public class Product
        {
            private List<SanPham> _sp = new List<SanPham>();

            public void Them(SanPham sanPham)
            {
                if (string.IsNullOrEmpty(sanPham.TenSanPham)|| string.IsNullOrEmpty(sanPham.MaSanPham) || sanPham.Gia <= 0 || sanPham.SoLuong < 0)
                {
                    throw new Exception("Thông tin sản phẩm không hợp lệ.");
                }            
                if (sanPham.SoLuong > 100 || sanPham.SoLuong < 0)
                {
                    throw new Exception("Số lượng phải là số nguyên dương và nhỏ hơn 100");
                }
                if (string.IsNullOrEmpty(sanPham.MaSanPham) || !sanPham.MaSanPham.StartsWith("SP"))
                {
                    throw new Exception("MaSanPham phải bắt đầu bằng 'SP'.");
                }
                if (_sp.Any(e => e.MaSanPham == sanPham.MaSanPham))
                {
                    throw new Exception("MaSanPham phải là duy nhất.");
                }
                _sp.Add(sanPham);
            }

            public void Sua(string id, SanPham suaSanPham)
            {
                var sanPham = _sp.Find(e => e.ID == id);
                if (sanPham != null)
                {
                    sanPham.MaSanPham = suaSanPham.MaSanPham;
                    sanPham.TenSanPham = suaSanPham.TenSanPham;
                    sanPham.Gia = suaSanPham.Gia;
                    sanPham.MauSac = suaSanPham.MauSac;
                    sanPham.KichThuoc = suaSanPham.KichThuoc;
                    sanPham.SoLuong = suaSanPham.SoLuong;
                }
                else
                {
                    throw new Exception($"Không tìm thấy sản phẩm có ID {id}");
                }
            }

            public void Xoa(string id)
            {
                var sanPham = _sp.Find(e => e.ID == id);
                if (sanPham != null)
                {
                    _sp.Remove(sanPham);
                }
                else
                {
                    throw new Exception($"Không tìm thấy sản phẩm có ID {id}");
                }
            }

            public List<SanPham> DanhSach()
            {
                return _sp;
            }
        }
    }
}
