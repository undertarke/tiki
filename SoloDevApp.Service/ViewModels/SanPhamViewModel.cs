namespace SoloDevApp.Service.ViewModels
{
    public class SanPhamViewModel
    {
        public int Id { get; set; }
        public string TenSanPham { get; set; }
        public int GiaTien { get; set; }
        public string MoTa { get; set; }
        public int CuaHangId { get; set; }
        public string BaoHanh { get; set; }
        public bool HoanTien { get; set; }
        public bool KiemTraHang { get; set; }
        public bool DoiTra { get; set; }
        public string LinkVideo { get; set; }
        public string HinhAnh { get; set; }
        public int MaDanhMucSanPham { get; set; }
        public int GiamGiaPhanTram { get; set; }

    }
}