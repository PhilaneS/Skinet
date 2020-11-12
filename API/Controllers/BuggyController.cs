using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _context;
        public BuggyController(StoreContext context)
        {
            _context = context;
            
        }  

        [HttpGet("NotFound")]
        public ActionResult GetNotFound() {

            var action = _context.Products.Find(45);

            if(action ==null){
            return NotFound(new ApiResponse(404));
            }

                return Ok();
        } 
         [HttpGet("ServerError")]
        public ActionResult GetServerError() {
            
            var  action = _context.Products.Find(42);

            var results = action.Name.ToString();

            return Ok();
        }
         [HttpGet("BadRequest")]
        public ActionResult GetBadRequest() {
            
                return BadRequest(new ApiResponse(400));
        }  

         [HttpGet("BadRequest/{id}")]
        public ActionResult GetNotFoundRequest(int id) {
            
                return BadRequest();
        }  
    }
}