using Seavus.BookLibrary.DataAccess.Interfaces;
using Seavus.BookLibrary.Domain.Models;
using Seavus.BookLibrary.Dtos.AuthorDto;
using Seavus.BookLibrary.Mappers;
using Seavus.BookLibrary.Services.Intefaces;
using Seavus.BookLibrary.Shared.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Seavus.BookLibrary.Services.Implementations
{
    public class AuthorService : IAuthorService
    {
        private IRepository<Author> _authorRepository;
        private IRepository<AuthorBook> _authorBookRepository;
        public AuthorService(IRepository<Author> authorRepository, IRepository<AuthorBook> authorBookRepository)
        {
            _authorRepository = authorRepository;
            _authorBookRepository = authorBookRepository;
        }
        public void AddAuthor(AddAuthorDto addAuthorDto)
        {
            ValidateAuthorInput(addAuthorDto);

            Author newAuthor = addAuthorDto.ToAuthor();
            _authorRepository.Insert(newAuthor);
        }

        public void DeleteAuthor(int id)
        {
            Author authorDb = _authorRepository.GetById(id);

            List<AuthorBook> allAuthorsBook = _authorBookRepository.GetAll().Where(x => x.AuthorId == id).ToList();
            for (int i = 0; i < allAuthorsBook.Count; i++)
            {
                _authorBookRepository.Delete(allAuthorsBook[i]);
            }

            if (authorDb == null)
            {
                throw new Exception($"Author with id {id} was not found");
            }
            _authorRepository.Delete(authorDb);
        }

        public List<AuthorDto> GetAllAuthors()
        {
            List<Author> authorDb = _authorRepository.GetAll();
            return authorDb.Select(x => x.ToAuthorDto()).ToList();
        }

        public UpdateAuthorDto GetAuthorById(int id)
        {
            Author authorDb = _authorRepository.GetById(id);
            if (authorDb == null)
            {
                throw new Exception($"Author with id {authorDb.Id} was not found");
            }

            return authorDb.ToUpdatedAuthorDto();
        }

        public void UpdateAuthor(AddAuthorDto updateAuthorDto)
        {
            ValidateAuthorInput(updateAuthorDto);
            Author authorDb = _authorRepository.GetById(updateAuthorDto.Id);
            if (authorDb == null)
            {
                throw new ResourceNotFound($"Author with id {updateAuthorDto.Id} was not found");
            }

            authorDb.FirstName = updateAuthorDto.FirstName;
            authorDb.LastName = updateAuthorDto.LastName;
            _authorRepository.Update(authorDb);
        }

        private void ValidateAuthorInput(AddAuthorDto addAuthorDto)
        {
            if (string.IsNullOrEmpty(addAuthorDto.FirstName) || string.IsNullOrEmpty(addAuthorDto.LastName))
            {
                throw new AuthorException("Firstname and lastname are required fields!");
            }
            if (addAuthorDto.FirstName.Length > 50)
            {
                throw new AuthorException("Firstname can contain maximum 50 characters");
            }
            if (addAuthorDto.LastName.Length > 50)
            {
                throw new AuthorException("Lastname can contain maximum 50 characters");
            }
        }
    }
}
