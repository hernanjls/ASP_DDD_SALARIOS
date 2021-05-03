using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Salarios.Dominio.Interfaces;
using Salarios.Infraestructura.Datos;

namespace Salarios.Infraestructura.Repositorio
{
    public class Repository<T> : IRepository<T>, IDisposable where T : class
    {
        protected readonly DbContextOptionsBuilder<ContextBase> _OptionsBuider;

        public Repository()
        {
            _OptionsBuider = new DbContextOptionsBuilder<ContextBase>();
        }


        public virtual void Add(T Entity)
        {
            using (var banco = new ContextBase(_OptionsBuider.Options))
            {
                banco.Set<T>().Add(Entity);
                banco.SaveChanges();
            }
        }

        public virtual void Delete(T Entity)
        {
            using (var banco = new ContextBase(_OptionsBuider.Options))
            {
                banco.Set<T>().Remove(Entity);
                banco.SaveChanges();
            }
        }


        public virtual T GetEntity(int Id)
        {
            using (var banco = new ContextBase(_OptionsBuider.Options))
            {
                return banco.Set<T>().Find(Id);

            }
        }

        public virtual List<T> List()
        {
            using (var banco = new ContextBase(_OptionsBuider.Options))
            {
                return banco.Set<T>().AsNoTracking().ToList();

            }
        }

        public virtual void Update(T Entity)
        {
            using (var banco = new ContextBase(_OptionsBuider.Options))
            {
                banco.Set<T>().Update(Entity);
                banco.SaveChanges();
            }
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool isDispose)
        {
            if (!isDispose) return;
        }

        ~Repository()
        {
            Dispose(false);
        }

    }
}
