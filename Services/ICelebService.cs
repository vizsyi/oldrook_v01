using Oldrook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oldrook.Services
{
    interface ICelebService
    {
        Celeb Find(int id);
        Task<Celeb> FindAsync(int id);
        IQueryable<Celeb> GetAll(int? count = null, int? page = null);
        Task<Celeb[]> GetAllAsync(int? count = null, int? page = null);
        Task SaveAsync(Celeb celeb);
        Task DeleteAsync(int id);
    }
}
