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


        [HttpGet, Route]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, resource.GetBooks());
        }

        [HttpGet, Route("{bookId}")]
        public HttpResponseMessage Get1(int bookId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, resource.GetBook(bookId));
        } 
    }
}
