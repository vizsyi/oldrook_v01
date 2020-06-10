using Microsoft.EntityFrameworkCore;
using Oldrook.Data;
using Oldrook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oldrook.Services
{
    public class CelebService : ICelebService
    {
        private readonly ApplicationDbContext db;

        public CelebService(ApplicationDbContext context) => db = context;

        public Celeb Find(int id)
        {
            return db.Celebs.Find(id);
        }

        public async Task<Celeb> FindAsync(int id)
        {
            return await db.Celebs.FindAsync(id);

        }

        public IQueryable<Celeb> GetAll(int? count = null, int? page = null)
        {
            return db.Celebs.AsQueryable();
        }

        public Task<Celeb[]> GetAllAsync(int? count = null, int? page = null)
        {
            return GetAll(count, page).ToArrayAsync();
        }

        public async Task SaveAsync(Celeb celeb)
        {

            if (celeb.Id == default(int))
            {
                db.Celebs.Add(celeb);
                db.Entry(celeb).State = EntityState.Added;
            }
            else
            {
                db.Entry(celeb).State = EntityState.Modified;
            }

            await db.SaveChangesAsync();
            //var isNew = recipe.Id == default(long);

            //_context.Entry(recipe).State = isNew ? EntityState.Added : EntityState.Modified;

            //await _context.SaveChangesAsync();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
