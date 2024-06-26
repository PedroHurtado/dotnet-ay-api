using Microsoft.AspNetCore.Mvc;

namespace Controller{
    public class Congtroller : ControllerBase{

        public class UserDto {

        }
        public class UserCredentidal{

        }
        [HttpPost("/login")]
        public UserCredentidal Login([FromBody] UserDto user){
            //TODO->
            return new UserCredentidal();
        }

        [HttpPost("/logout")]
        public void Logout(){
            //HttpContext.Response.Headers.Cookie.
        }

    }
    
}