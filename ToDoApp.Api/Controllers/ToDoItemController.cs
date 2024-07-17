using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Api.Services;
using ToDoApp.Data.Context;

namespace ToDoApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemController : ControllerBase
    {
        private readonly ToDoContext _context;
        private readonly TestService _service;
        public ToDoItemController(ToDoContext context, TestService service)
        {
            _context = context;
            _service = service;
        }
        
    }
    
}
