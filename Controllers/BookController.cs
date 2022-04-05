using BookStore.Models;
using BookStore.Models.Repositories;
using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookStoreRepository<Book> bookRepository;
        private readonly IBookStoreRepository<Author> authorRepository;
        private readonly IHostingEnvironment hosting;
        public BookController(IBookStoreRepository<Book> bookRepository, IBookStoreRepository<Author> authorRepository, IHostingEnvironment hosting)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
            this.hosting = hosting;

        }
        // GET: BookController
        public ActionResult Index()
        {
            var books = bookRepository.List();
            return View(books);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var book = bookRepository.Find(id);
            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {

            var model = new BookAuhtorVM
            {
                Authors = FillSelectList()
            };
            return View(model);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuhtorVM model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    string fileName = UploadFile(model.File) ?? string.Empty;

                    if (model.AuthorId == -1)
                    {
                        ViewBag.Message = "Please Select An Author From The List";
                        return View(GetAllAuthors());

                    }

                    Book book = new Book()
                    {
                        Id = model.BookId,
                        Title = model.Title,
                        Description = model.Description,
                        Author = authorRepository.Find(model.AuthorId),
                        ImageUrl = fileName,
                    };

                    bookRepository.Add(book);
                    return RedirectToAction(nameof(Index));
                }

                catch
                {
                    return View();
                }
            }
            ModelState.AddModelError("", "you have to fail all required fields!");
            return View(GetAllAuthors());
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {


            var book = bookRepository.Find(id);
            var authorId = book.Author == null ? book.Author.Id = 0 : book.Author.Id;
            var viewModel = new BookAuhtorVM
            {
                BookId = book.Id,
                Title = book.Title,
                Description = book.Description,
                AuthorId = authorId,
                Authors = authorRepository.List().ToList(),
                ImageUrl = book.ImageUrl,
            };
            return View(viewModel);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BookAuhtorVM bookVm)
        {
            try
            {
                string fileName = UploadFile(bookVm.File,bookVm.ImageUrl);

                Book book = new Book()
                {
                    Id = bookVm.BookId,
                    Title = bookVm.Title,
                    Description = bookVm.Description,
                    Author = authorRepository.Find(bookVm.AuthorId),
                    ImageUrl = fileName
                };
                bookRepository.Update(book, id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            var book = bookRepository.Find(id);
            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Book book)
        {
            try
            {

                bookRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Search(string term)
        {
            var result=bookRepository.Search(term); 
            return View("Index", result);
        }

        List<Author> FillSelectList()
        {
            var authors = authorRepository.List().ToList();

            authors.Insert(0, new Author { Id = -1, FullName = "-- please enter an author --- " });
            return authors;
        }

        BookAuhtorVM GetAllAuthors()
        {
            var vModel = new BookAuhtorVM
            {
                Authors = FillSelectList()
            };
            return vModel;
        }

        string UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "uploads");
                string fullPath = Path.Combine(uploads, file.FileName);
                file.CopyTo(new FileStream(fullPath, FileMode.Create));

                return file.FileName;
            }
            return null;
        }

        string UploadFile(IFormFile file ,string imageUrl)
        {
            if (file != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "uploads");

                string newPath = Path.Combine(uploads, file.FileName);
                string oldPath = Path.Combine(uploads, imageUrl);

                if (oldPath != newPath)
                {
                    // delete the old file
                    System.IO.File.Delete(oldPath);
                    //save the new file
                    file.CopyTo(new FileStream(newPath, FileMode.Create));
                }
                return file.FileName; 
            }
            return imageUrl;
        }





    }
}
