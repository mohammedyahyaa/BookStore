using System.Collections.Generic;
using System.Linq;

namespace BookStore.Models.Repositories
{
    public class BookRepository : IBookStoreRepository<Book>
    {

        List<Book> books;

        public BookRepository()
        {
            books = new List<Book>()
                {
                    new Book
                    {
                        Id = 1,
                        Title="C# Programming" ,
                        Description="No Description" ,
                        Author = new Author(),
                        ImageUrl ="Csharp.png"
                    },
                    new Book
                    {
                        Id = 2,
                        Title="Java Programming" ,
                        Description="Nothing" ,
                        Author = new Author(),
                        ImageUrl ="Java.png"
                    },
                    new Book
                    {
                        Id = 3, 
                        Title="Python Programming" ,
                        Description="No data" , 
                        Author = new Author() ,
                        ImageUrl ="python.png"
                    }
                };
        }
        public void Add(Book entity)
        {
            entity.Id = books.Max(x => x.Id) + 1;
            books.Add(entity);
        }

        public void Delete(int id)
        {
            var book = books.SingleOrDefault(x => x.Id == id);
            books.Remove(book);

        }

        public Book Find(int id)
        {
            return books.SingleOrDefault(b => b.Id == id);

        }

        public IList<Book> List()
        {
            return books;
        }

        public List<Book> Search(string term)
        {
            return books.Where(a => a.Title.Contains(term)).ToList();
        }

        public void Update(Book newBook, int id)
        {
            var book = books.SingleOrDefault(b => b.Id == id);
            book.Title = newBook.Title;
            book.Description = newBook.Description;
            book.Author = newBook.Author;
            book.ImageUrl = newBook.ImageUrl;

        }
    }
}
