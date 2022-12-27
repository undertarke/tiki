using Microsoft.Extensions.Configuration;
using SoloDevApp.Repository.Infrastructure;
using SoloDevApp.Repository.Models;

namespace SoloDevApp.Repository.Repositories
{
    public interface INguoiDungRepository : IRepository<NguoiDung>
    {
    
    }

    public class NguoiDungRepository : RepositoryBase<NguoiDung>, INguoiDungRepository
    {
        public NguoiDungRepository(IConfiguration configuration)
            : base(configuration)
        {
        }

  
    }
}