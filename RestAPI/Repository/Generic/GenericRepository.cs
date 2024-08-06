using Microsoft.EntityFrameworkCore;
using RestAPI.Model;
using RestAPI.Model.Context;
using System;

namespace RestAPI.Repository.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly MySQLContext _context;
        private DbSet<T> dataset;
        public GenericRepository(MySQLContext context)
        {
            _context = context;
            dataset = _context.Set<T>();
        }
        public List<T> FindAll()
        {
            return dataset.ToList();
        }

        public T FindById(long id)
        {
            return dataset.SingleOrDefault(p => p.Id.Equals(id));
        }

        public T Create(T entity)
        {
            try
            {
                dataset.Add(entity);
                _context.SaveChanges();
                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public T Update(T entity)
        {
            if (!Exists(entity.Id)) return null;

            var result = dataset.SingleOrDefault(p => p.Id.Equals(entity.Id));

            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(entity);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return entity;
        }

        public void Delete(long id)
        {
            var result = dataset.SingleOrDefault(p => p.Id.Equals(id));

            if (result != null)
            {
                dataset.Remove(result);
                _context.SaveChanges();
            }
        }

        public bool Exists(long id)
        {
            return dataset.Any(p => p.Id.Equals(id));
        }
    }
}
