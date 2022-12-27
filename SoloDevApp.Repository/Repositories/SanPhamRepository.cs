using Microsoft.Extensions.Configuration;
using SoloDevApp.Repository.Infrastructure;
using SoloDevApp.Repository.Models;

namespace SoloDevApp.Repository.Repositories
{
    public interface ISanPhamRepository : IRepository<SanPham>
    {
       // Task<IEnumerable<GiangDay>> GetByGiangVienId(string giangVienId);

    }

    public class SanPhamRepository : RepositoryBase<SanPham>, ISanPhamRepository
    {
        public SanPhamRepository(IConfiguration configuration)
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