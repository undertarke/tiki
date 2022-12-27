using Microsoft.Extensions.Configuration;
using SoloDevApp.Repository.Infrastructure;
using SoloDevApp.Repository.Models;

namespace SoloDevApp.Repository.Repositories
{
    public interface IDanhMucSanPhamRepository : IRepository<DanhMucSanPham>
    {
       // Task<IEnumerable<GiangDay>> GetByGiangVienId(string giangVienId);

    }

    public class DanhMucSanPhamRepository : RepositoryBase<DanhMucSanPham>, IDanhMucSanPhamRepository
    {
        public DanhMucSanPhamRepository(IConfiguration configuration)
            : base(configuration)
        {
        }

      /*  public async Task<IEnumerable<GiangDay>> GetByGiangVienId(string giangVienId)
        {
            string query = $"SELECT * FROM {_table} WHERE GiangVienId = '{giangVienId}';";
            using (var conn = CreateConnection())
            {
                try
                {
                    return await conn.QueryAsync<GiangDay>(query, null, null, null, CommandType.Text);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }*/
    }
}