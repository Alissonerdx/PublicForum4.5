using PublicForum2.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicForum2.Infra.Data.Context
{
    public class UnitOfWork : IUnitOfWork
    {
        public EFContext _context;

        public UnitOfWork(EFContext context)
        {
            _context = context;
        }
        public void BeginTransaction()
        {
            if (_context == null)
                throw new System.ArgumentException("Context not initialized");
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public Task<int> CommitAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
