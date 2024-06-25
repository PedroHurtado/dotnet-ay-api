using Microsoft.AspNetCore.Mvc;
using IOC;
using Segregation;
using Microsoft.AspNetCore.Diagnostics;

namespace Controller
{

    internal sealed class GlobalExceptionHandler : IExceptionHandler
    {
        public async  ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not NotFoudnException e)
            {
                return false;
            }

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Bad Request",
                Detail = "No existe"
            };
            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response
                .WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
    class NotFoudnException : Exception
    {

    }

    public static class IEnumerableNotFound
    {
        public static T ResultOrNotFound<T>(this IEnumerable<T> value)
        {
            var result = value.FirstOrDefault();
            if (result == null)
            {
                throw new NotFoudnException();
            }
            return result;
        }
    }
    class Foo : IActionResult
    {
        public Task ExecuteResultAsync(ActionContext context)
        {
            var result = new ObjectResult(null);
            return result.ExecuteResultAsync(context);
        }
    }
    public class Service : IService
    {
        public void Add()
        {
            //throw new NotImplementedException();
        }
    }
    public interface IService
    {
        void Add();
    }

    public class MyControllerBase : ControllerBase
    {
        protected IActionResult OkOrNotFound<T>(IEnumerable<T> query)
        {
            var result = query.FirstOrDefault();
            if (result == null)
            {
                return
                NotFound();
            }
            return Ok(result);
        }
    }
    [ApiController]
    [Route("[controller]")]
    public class PizzasController : MyControllerBase
    {

        //private readonly List<int?> list = [22,39,30,40,50,80];
        private readonly List<int?> list = new List<int?> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        private readonly IService _service;
        private readonly IB _b;
        private readonly IUpdate<Customer, int> _updateCustomer;
        public PizzasController(IService service, IB b, IUpdate<Customer, int> updateCustomer)
        {
            _service = service;
            _b = b;
            _updateCustomer = updateCustomer;
            Console.WriteLine("Controlador");

        }
        public readonly record struct Person(string FirstName, string LastName);
        [HttpGet]
        public string GetAll()
        {
            return "Hello World";
        }

        [HttpGet("{id}")]
        public int? Get(int id)
        {

            return list.Where(v => v == id).ResultOrNotFound();
            //return OkOrNotFound(list.Where(v=>v==id));
            //return list.Where(v=>v==id).OkOrNotFound()

            //var result = list.Where(v => v == id).FirstOrDefault();

            //if (result == null)
            //{
            //    return NotFound();
            //}
            //return Ok(result);
            //throw new Exception("El id no existe");
            //return id.ToString();
        }

        [HttpPost]
        public IActionResult Add([FromBody] Person person)
        {
            _service.Add();
            return Created("", person);
        }
        [HttpPut("{id}")]
        public Person Update(int id, [FromBody] Person person)
        {
            return person;
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            return NoContent();
        }

    }
}