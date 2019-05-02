using System.Collections.Generic;

namespace MongoDB
{
    internal interface IDataProvider<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetItem(string id);
        void Create(TEntity item);
        void Update(TEntity item);
        void Delete(string id);
    }
}