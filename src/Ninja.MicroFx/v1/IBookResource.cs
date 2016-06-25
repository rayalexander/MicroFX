using System.Collections.Generic;
using Ninja.MicroFx.Platform;
using Ninja.MicroFx.v1.Contracts;

namespace Ninja.MicroFx.v1
{
    public interface IBookResource: IResource
    {
        IEnumerable<BookDocument> GetBooks();
        BookDocument GetBook(int bookId);
    }
}