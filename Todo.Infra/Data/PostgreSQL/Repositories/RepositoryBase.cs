using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;
using Todo.Domain.Interfaces.Repositories;
using Todo.Infra.Data.PostgresSQL.Context;

namespace Todo.Infra.Data.PostgreSQL.Repositories
{
    public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected ApplicationDbContext _context { get; set; }
        public DbSet<TEntity> DbSet { get; }

        public RepositoryBase(ApplicationDbContext context)
        {
            _context = context;
            this.DbSet = _context.Set<TEntity>();      
        }

        public async ValueTask<TEntity> SaveAsync(TEntity entity)
        {
            if(entity.Id == null || entity.Id == 0){
                this.DbSet.Entry(entity).State = EntityState.Added;
                await this.DbSet.AddAsync(entity);
            }
            else
            {
                this.DbSet.Entry(entity).State = EntityState.Modified;
                this.DbSet.Update(entity);
            }

            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(long? id)
        {
            var entity = await this.GetByIdAsync(id);
            this.DbSet.Remove(entity);

            await this._context.SaveChangesAsync();
        }

        public async ValueTask<TEntity> GetByIdAsync(long? id) 
            => await this.DbSet.AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(c => c.Id == id);

        public async ValueTask<IEnumerable<TEntity>> GetAllAsync()
            => await this.DbSet.AsNoTrackingWithIdentityResolution().ToListAsync();

        public async ValueTask<bool> ExistsByIdAsync(long? id)
            => await this.DbSet.AnyAsync(c => c.Id == id);
    }
}