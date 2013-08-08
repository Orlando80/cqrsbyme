using System;
using CQRS_ByMe.Domain;

namespace CQRS_ByMe.Repositories
{
    public interface IRepository<T> where T : BaseAggregateRoot, new () 
    {
        T GetById(Guid id);
        void Save(T aggregateRoot);
    }
}