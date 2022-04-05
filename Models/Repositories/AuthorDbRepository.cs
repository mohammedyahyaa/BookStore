using System.Collections.Generic;
using System.Linq;

namespace BookStore.Models.Repositories
{
    public class AuthorDbRepository : IBookStoreRepository<Author>
    {

        BookStoreDbContext db; 
        public AuthorDbRepository(BookStoreDbContext _db)
        {
                  
            db = _db;
        }
        public void Add(Author entity)
        {
            db.Authors.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var author = Find(id);
            db.Authors.Remove(author);
            db.SaveChanges();
           
        }
        public Author Find(int id)
        {
            return db.Authors.SingleOrDefault(a=> a.Id == id); 
        }

        public IList<Author> List()
        {
            return db.Authors.ToList(); 
        }

        public List<Author> Search(string term)
        {
            return db.Authors.Where(a => a.FullName.Contains(term)).ToList();
        }

        public void Update(Author newAuthor, int id)
        {
            db.Authors.Update(newAuthor);
            db.SaveChanges();   
        }
    }
}