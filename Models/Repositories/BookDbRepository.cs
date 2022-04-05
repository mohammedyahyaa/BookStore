﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Models.Repositories
{
    public class BookDbRepository : IBookStoreRepository<Book>
    {

        BookStoreDbContext db;
        public BookDbRepository(BookStoreDbContext _db)
        {

            db = _db;
        }


        public void Add(Book entity)
        {
            db.Books.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var book = Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
        }

        public Book Find(int id)
        {
            return db.Books.Include(a => a.Author).SingleOrDefault(x => x.Id == id);

        }

        public IList<Book> List()
        {
            return db.Books.Include(a => a.Author).ToList();
        }

        public void Update(Book newBook, int id)
        {
            db.Update(newBook);
            db.SaveChanges();
        }

        public List<Book> Search(string term)
        {
            var result = db.Books.Include(a => a.Author).Where(b => b.Title.Contains(term)
                     || b.Description.Contains(term)
                     || b.Author.FullName.Contains(term)).ToList();

            return result;
        }

    }
}