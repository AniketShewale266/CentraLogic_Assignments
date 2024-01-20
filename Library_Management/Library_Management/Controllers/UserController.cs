using Microsoft.Azure.Cosmos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Library_Management.DTO;
using Library_Management.Entities;
using Library_Management.Interface;

namespace Library_Management.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly Container _container;
        public IUserInterface _userInterface;

        private Container GetContainer()
        {
            string URI = Environment.GetEnvironmentVariable("URI");
            string PrimaryKey = Environment.GetEnvironmentVariable("PrimaryKey");
            string DatabaseName = Environment.GetEnvironmentVariable("DatabaseName");
            string ContainerName = Environment.GetEnvironmentVariable("ContainerName");
            CosmosClient cosmosclient = new CosmosClient(URI, PrimaryKey);
            //Connect with Our Database
            Database databse = cosmosclient.GetDatabase(DatabaseName);
            //Connect with Our Container 
            Container container = databse.GetContainer(ContainerName);

            return container;
        }

        public UserController(IUserInterface userInterface)
        {
            _container = GetContainer();
            _userInterface = userInterface;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDTO userDTO)
        {
            UserEntity userEntity = new UserEntity();
            // Convert student model to student entity
            userEntity.PRN = userDTO.PRN;
            userEntity.Username = userDTO.Username;
            userEntity.Password = userDTO.Password;
            userEntity.CPassword = userDTO.CPassword;

            // Call service function
            var responce = await _userInterface.CreateUser(userEntity);
            // Return model to UI
            userDTO.PRN = responce.PRN;
            userDTO.Username = responce.Username;

            return Ok(userDTO);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(UserEntity userEntity)
        {
            var result = await _userInterface.LoginUser(userEntity);

            if (result != null)
            {
                return Ok(new { UId = result });
            }

            return Unauthorized(new { Message = "Invalid credentials." });
        }



    }
}
