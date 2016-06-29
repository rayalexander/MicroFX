using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ninja.MicroFx.v1.Controllers
{
    [RoutePrefix("v1/books")]
    public class BookController: ApiController
    {
        private readonly IBookResource resource;

        public BookController(IBookResource resource)
        {
            this.resource = resource;
        }

        [AllowAnonymous]
        [HttpGet, Route]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, resource.GetBooks());
        }

        [AllowAnonymous]
        [HttpGet, Route("{bookId}")]
        public HttpResponseMessage Get(int bookId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, resource.GetBook(bookId));
        } 
    }
}
