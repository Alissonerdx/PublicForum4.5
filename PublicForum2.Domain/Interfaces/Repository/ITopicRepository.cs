using PublicForum2.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicForum2.Domain.Interfaces.Repository
{
    public interface ITopicRepository : IBaseRepository<Topic>
    {
        Task<Topic> GetById(Guid id);
        Task<List<Topic>> GetPaginated(int page = 1, int pageSize = 30, List<Dictionary<string, string>> filters = null, string orderBy = "Id", bool ascending = true);
        Task<int> GetCountFiltered(List<Dictionary<string, string>> filters);
        Task<Topic> GetByIdAndOwnerId(Guid id, Guid ownerId);
    }
}
