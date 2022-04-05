using System.Collections.Generic;
using System.Linq;

namespace BookStore.Models.Repositories
{
    public class AuthorRepository : IBookStoreRepository<Author>
    {

        IList<Author> authors;

        public AuthorRepository()
        {
            authors = new List<Author>()
            {
                new Author{Id=1 , FullName="Max"},
                new Author{Id=2 , FullName="William"},
                new Author{Id=3 , FullName="Brad"},
                new Author{Id=4 , FullName="Toni"},
            };
                
        }
        public void Add(Author entity)
        {
            entity.Id=authors.Max(a => a.Id)+1;   
            authors.Add(entity);    
           
        }

        public void Delete(int id)
        {
            var author = authors.SingleOrDefault(b => b.Id == id);
            authors.Remove(author);
        }

        public Author Find(int id)
        {
            return authors.SingleOrDefault(b => b.Id == id);
        }

        public IList<Author> List()
        {
            return authors;
        }

        public List<Author> Search(string term)
        {
           return authors.Where(a=>a.FullName.Contains(term)).ToList();
        }

        public void Update(Author newAuthor, int id)
        {
            var author =Find(id);
            
            author.FullName = newAuthor.FullName;
            
        }
    }
}
