using EFCoreInMemoryDbDemo;
using Microsoft.EntityFrameworkCore;
using PearsonAssesment.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PearsonAssesment.Repositories
{
    internal class BaseRepository<T>: IBaseRepository<T> where T: class
    {
        public BaseRepository()
        {
        }

        public T GetById(string id)
        {
            using (var context = new PearsonAssesmentContext())
            {
                return context.Set<T>().Find(id);
            }
        }

        public T Add(T entity)
        {
            using (var context = new PearsonAssesmentContext())
            {
                context.Set<T>().Add(entity);
                context.SaveChanges();
                return entity;
            }
        }

        public void Update(T entity)
        {
            using (var context = new PearsonAssesmentContext())
            {
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

    }
}
