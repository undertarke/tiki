namespace SoloDevApp.Repository.Models
{
    public class DonDatHang
    {
        public int Id { get; set; }
        public int SanPhamId { get; set; }
        public int NguoiDungId { get; set; }
        public int SoLuongDat { get; set; }
        public string NgayDat { get; set; }
    }
}