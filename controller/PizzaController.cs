using Microsoft.AspNetCore.Mvc;


namespace Controller{
    
    [ApiController]
    [Route("[controller]")]
    public class PizzasController: ControllerBase{

         public readonly record struct Person(string FirstName, string LastName);
        [HttpGet]
        public string GetAll(){
            return "Hello World";
        }

        [HttpGet("{id}")]
        public string Get(int id){            
            return id.ToString();
        }

        [HttpPost]
        public IActionResult Add([FromBody]Person person){
            return Created("",person);
        }
        [HttpPut("{id}")]
        public Person Update(int id, [FromBody]Person person){
            return person;
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(int id){
            return NoContent();
        }
        
    }
}