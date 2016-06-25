using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Ninja.MicroFx.Domain;
using Ninja.MicroFx.v1.Contracts;

namespace Ninja.MicroFx.v1.Impl
{
    public class BookResource : IBookResource
    {
       Book[] books = new[]
            {
                new Book{Id = 1, Author = "Mark Twain",Name = ".Net Blues"},
                new Book{Id = 2, Author = "Robert Miles",Name = "101 Great Hits"}
            };  

        public BookDocument GetBook(int bookId)
        {
            var book = books.FirstOrDefault(b => b.Id == bookId);

            if (book == null)
            throw new Exception("Not Found");

            return Mapper.Map<BookDocument>(book);
        }

        public IEnumerable<BookDocument> GetBooks()
        {
           return Mapper.Map<BookDocument[]>(books);
        }
    }
}
