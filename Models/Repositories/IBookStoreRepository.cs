
using System.Collections.Generic;

namespace BookStore.Models.Repositories
{
    public interface IBookStoreRepository<TEntity>
    {
        IList<TEntity> List();
        TEntity Find(int id);
        void Add(TEntity entity);
        void Update(TEntity entity , int id);
        void Delete(int id);

        List<TEntity> Search(string term);


  

    }
}
