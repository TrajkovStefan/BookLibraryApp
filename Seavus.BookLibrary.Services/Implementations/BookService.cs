using Seavus.BookLibrary.DataAccess.Interfaces;
using Seavus.BookLibrary.Domain.Enums;
using Seavus.BookLibrary.Domain.Models;
using Seavus.BookLibrary.Dtos.BookDto;
using Seavus.BookLibrary.Mappers;
using Seavus.BookLibrary.Services.Intefaces;
using Seavus.BookLibrary.Shared.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Seavus.BookLibrary.Services.Implementations
{
    public class BookService : IBookService
    {
        private IBookRepository _bookRepository;
        private IRepository<Author> _authorRepository;
        private IRepository<AuthorBook> _authorBookRepository;

        public BookService(IBookRepository bookRepository, IRepository<Author> authorRepository, IRepository<AuthorBook> authorBookRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _authorBookRepository = authorBookRepository;
        }

        public void AddBook(AddBookDto addBookDto)
        {
            ValidateBookInput(addBookDto);
            ValidateAuthorInput(addBookDto);

            Book lastBook = GetBookByUserInput(addBookDto);
            Author lastAuthor = GetAuthorByUserInput(addBookDto);
            if(lastBook == null)
            {
                throw new ResourceNotFound("Book was not found");
            }
            if(lastAuthor == null)
            {
                throw new ResourceNotFound("Author was not found!");
            }

            AuthorBook lastBookAuthorBook = new AuthorBook();

            lastBookAuthorBook.BookId = lastBook.Id;
            lastBookAuthorBook.AuthorId = lastAuthor.Id;

            lastBook.AuthorBook.Add(lastBookAuthorBook);
            lastAuthor.AuthorBook.Add(lastBookAuthorBook);
            _authorBookRepository.Insert(lastBookAuthorBook);
            _bookRepository.Update(lastBook);
            _authorRepository.Update(lastAuthor);
        }

        public void DeleteBook(int id)
        {
            Book bookDb = _bookRepository.GetById(id);
            List<AuthorBook> allAuthorsBook = _authorBookRepository.GetAll().Where(x => x.BookId == id).ToList();
            for (int i = 0; i < allAuthorsBook.Count; i++)
            {
                _authorBookRepository.Delete(allAuthorsBook[i]);
            }

            if (bookDb == null)
            {
                throw new ResourceNotFound($"Book was not found");
            }
            _bookRepository.Delete(bookDb);
        }

        public List<BookDto> GetAllBooks()
        {
            List<Book> booksDb = _bookRepository.GetAll();
            return booksDb.Select(x => x.ToBookDto()).ToList();
        }

        public UpdateBookDto GetBookById(int id)
        {
            Book bookDb = _bookRepository.GetById(id);
            if (bookDb == null)
            {
                return null;
            }
            return bookDb.ToUpdatedBookDto();
        }

        public void UpdateBook(UpdateBookDto updateBookDto)
        {

            ValidateUpdateBookInput(updateBookDto);
            Book bookDb = _bookRepository.GetById(updateBookDto.Id);
            if (bookDb == null)
            {
                throw new ResourceNotFound($"Book was not found");
            }

            bookDb.Title = updateBookDto.Title;
            bookDb.Genre = (RoleGenre)updateBookDto.Genre;
            bookDb.Status = bool.Parse(updateBookDto.Status);
            bookDb.NumOfCopies = updateBookDto.NumOfCopies;

            _bookRepository.Update(bookDb);
        }

        public IEnumerable<BookDto> GetAllBooksByUserInput(string title, string genre, string author)
        {
            //IF ALL INPUTS ARE EMPTY OR NULL
            if (string.IsNullOrEmpty(title) && string.IsNullOrEmpty(genre) && string.IsNullOrEmpty(author))
            {
                IEnumerable<Book> allBooks = _bookRepository.GetAllBooksIEnumerable();
                if (!allBooks.Any())
                {
                    throw new ResourceNotFound($"Book was not found");
                }
                return allBooks.Select(x => x.ToBookDto());
            }

            //SEARCH BOOK BY TITLE AND GENRE
            if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(genre) && string.IsNullOrEmpty(author))
            {
                IEnumerable<Book> allBooks = _bookRepository.GetAllBooksIEnumerable().Where(x => x.Title.ToLower().Contains(title.ToLower()) && x.Genre.ToString().ToLower().Contains(genre.ToLower()));
                if (!allBooks.Any())
                {
                    throw new ResourceNotFound($"Book was not found");
                }
                return allBooks.Select(x => x.ToBookDto());
            }

            //SEARCH BOOK BY TITLE AND AUTHOR
            if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(author) && string.IsNullOrEmpty(genre))
            {
                IEnumerable<Book> allBooks = _bookRepository.GetAllBooksIEnumerable().Where(x => x.Title.ToLower().Contains(title.ToLower()) && x.AuthorBook.Select(x => x.Author).Any(x => x.FirstName.ToLower().Contains(author.ToLower())));
                if (!allBooks.Any())
                {
                    throw new ResourceNotFound($"Book was not found");
                }
                return allBooks.Select(x => x.ToBookDto());
            }

            //SEARCH BOOK BY GENRE AND AUTHOR
            if (!string.IsNullOrEmpty(genre) && !string.IsNullOrEmpty(author) && string.IsNullOrEmpty(title))
            {
                IEnumerable<Book> allBooks = _bookRepository.GetAllBooksIEnumerable().Where(x => x.Genre.ToString().ToLower().Contains(genre.ToLower()) && x.AuthorBook.Select(x => x.Author).Any(x => x.FirstName.ToLower().Contains(author.ToLower())));
                if (!allBooks.Any())
                {
                    throw new ResourceNotFound($"Book was not found");
                }
                return allBooks.Select(x => x.ToBookDto());
            }

            //SEARCH BOOK BY TITLE AND GENRE AND AUTHOR
            if (!string.IsNullOrEmpty(genre) && !string.IsNullOrEmpty(author) && !string.IsNullOrEmpty(title))
            {
                IEnumerable<Book> allBooks = _bookRepository.GetAllBooksIEnumerable().Where(x => x.Title.ToLower().Contains(title.ToLower()) && x.Genre.ToString().ToLower().Contains(genre.ToLower()) && x.AuthorBook.Select(x => x.Author).Any(x => x.FirstName.ToLower().Contains(author.ToLower())));
                if (!allBooks.Any())
                {
                    throw new ResourceNotFound($"Book was not found");
                }
                return allBooks.Select(x => x.ToBookDto());
            }

            if (string.IsNullOrEmpty(title))
            {
                title = new Random().ToString();
            }
            if (string.IsNullOrEmpty(genre))
            {
                genre = new Random().ToString();
            }
            if (string.IsNullOrEmpty(author))
            {
                author = new Random().ToString();
            }
            IEnumerable<BookDto> booksDb = _bookRepository.GetAllBooksIEnumerable()
                                                   .Where(x => x.Title.ToLower().Contains(title.ToLower()) ||
                                                   x.Genre.ToString().ToLower().Contains(genre.ToLower()) ||
                                                   x.AuthorBook.Select(x => x.Author)
                                                   .Any(x => x.FirstName.ToLower().Contains(author.ToLower())))
                                                   .Select(y => y.ToBookDto());
            if (!booksDb.Any())
            {
                throw new ResourceNotFound($"Book was not found");
            }

            return booksDb;
        }

        public Book GetBookByUserInput(AddBookDto addBookDto)
        {
            Book bookDb = _bookRepository.GetAll().FirstOrDefault(x => x.Title.ToLower() == addBookDto.Title.ToLower() && x.Genre == (RoleGenre)addBookDto.Genre && x.Description == addBookDto.Description && x.Status == bool.Parse(addBookDto.Status) && x.NumOfCopies == addBookDto.NumOfCopies);
            if (bookDb == null)
            {
                Book newBook = addBookDto.ToBook();
                _bookRepository.Insert(newBook);
                return newBook;
            }
            return bookDb;
        }

        public Author GetAuthorByUserInput(AddBookDto addBookDto)
        {
            Author authorDb = _authorRepository.GetAll().FirstOrDefault(x => x.FirstName.ToLower() == addBookDto.FirstName.ToLower() && x.LastName.ToLower() == addBookDto.LastName.ToLower());
            if (authorDb == null)
            {
                Author newAuthor = addBookDto.ToAuthor();
                _authorRepository.Insert(newAuthor);
                return newAuthor;
            }
            return authorDb;
        }

        #region private methods
        private void ValidateBookInput(AddBookDto addBookDto)
        {
            if (string.IsNullOrEmpty(addBookDto.Title))
            {
                throw new BookException("Title is a required field");
            }
            if (addBookDto.Title.Length > 30)
            {
                throw new BookException("Tite can contain maximum 30 characters!");
            }
            if (string.IsNullOrEmpty(addBookDto.Genre.ToString()))
            {
                throw new BookException("Genre is a required field");
            }
            if (string.IsNullOrEmpty(addBookDto.Status.ToString()))
            {
                throw new BookException("Status is a required field");
            }
            if (addBookDto.NumOfCopies < 0)
            {
                throw new BookException("The number of copies can not be less than 0");
            }
        }
        private void ValidateAuthorInput(AddBookDto addBookDto)
        {
            if (string.IsNullOrEmpty(addBookDto.FirstName) || string.IsNullOrEmpty(addBookDto.LastName))
            {
                throw new AuthorException("Firstname and Lastname are required fields!");
            }
            if (addBookDto.FirstName.Length > 50)
            {
                throw new AuthorException("Firstname can contain maximum 50 characters!");
            }
            if (addBookDto.LastName.Length > 50)
            {
                throw new AuthorException("Lastname can contain maximum 50 characters!");
            }
        }
        private void ValidateUpdateBookInput(UpdateBookDto updateBookDto)
        {
            if (string.IsNullOrEmpty(updateBookDto.Title))
            {
                throw new BookException("Title is a required field");
            }
            if (updateBookDto.Title.Length > 30)
            {
                throw new BookException("Tite can contain maximum 30 characters!");
            }
            if (string.IsNullOrEmpty(updateBookDto.Genre.ToString()))
            {
                throw new BookException("Genre is a required field");
            }
            if (string.IsNullOrEmpty(updateBookDto.Status.ToString()))
            {
                throw new BookException("Status is a required field");
            }
            if (updateBookDto.NumOfCopies < 0)
            {
                throw new BookException("The number of copies can not be less than 0");
            }
        }
        #endregion
    }
}