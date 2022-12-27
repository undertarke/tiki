using Microsoft.Extensions.Configuration;
using SoloDevApp.Repository.Infrastructure;
using SoloDevApp.Repository.Models;

namespace SoloDevApp.Repository.Repositories
{
    public interface IBinhLuanRepository : IRepository<BinhLuan>
    {
    
    }

    public class BinhLuanRepository : RepositoryBase<BinhLuan>, IBinhLuanRepository
    {
        public BinhLuanRepository(IConfiguration configuration)
            : base(configuration)
        {
        }

  
    }
}