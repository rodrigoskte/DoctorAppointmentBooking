﻿using DoctorAppointmentBooking.Domain.Entities;
using DoctorAppointmentBooking.Domain.Interfaces;
using DoctorAppointmentBooking.Infrastructure.Context;
using System.Collections.Generic;
using System.Linq;

namespace DoctorAppointmentBooking.Infrastructure.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly SqlDbContext _dbContext;

        public BaseRepository(SqlDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Insert(TEntity obj)
        {
            _dbContext.Set<TEntity>().Add(obj);
            _dbContext.SaveChanges();
        }

        public void Update(TEntity obj)
        {
            _dbContext.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = Select(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                _dbContext.Set<TEntity>().Update(entity);
                _dbContext.SaveChanges();
            }
        }

        public IList<TEntity> Select()
        {
            var entity = _dbContext.Set<TEntity>().ToList();
            if (entity != null)
                return entity.Where(e => e.IsDeleted == false).ToList();

            return entity;
        }
            

        public TEntity Select(int id)
        {
            return _dbContext.Set<TEntity>().First(e => e.Id == id && e.IsDeleted == false);
        }
            
    }
}
