using Oldrook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oldrook.Services
{
    public class CelebService : ICelebService
    {
        public Celeb Find(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Celeb> FindAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Celeb> GetAll(int? count = null, int? page = null)
        {
            throw new NotImplementedException();
        }

        public Task<Celeb[]> GetAllAsync(int? count = null, int? page = null)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync(Celeb celeb)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
