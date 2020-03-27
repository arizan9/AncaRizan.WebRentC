using Anca.Rizan.WebRentC.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anca.Rizan.WebRentC.Core.Contracts
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> Collection();
        void Commit();
        void Delete(int Id);
        T Find(int Id);
        void Insert(T t);
        void Update(T t);
    }
}
