using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Serialization.HybridRow.Schemas;
using MyAllTasks.DTO;
using MyAllTasks.Model;
using System.ComponentModel;
using System.Threading.Tasks;
using Container = Microsoft.Azure.Cosmos.Container;
using PartitionKey = Microsoft.Azure.Cosmos.PartitionKey;
namespace MyAllTasks.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        public string URI = "https://localhost:8081";
        public string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        public string DatabaseName = "MyDB";
        public string ContainerName = "MyAllTasks";
        public int versionCount = 1;
        public readonly Container _container;
        private Container GetContainer()
        {
            CosmosClient cosmosclient = new CosmosClient(URI, PrimaryKey);
            // step 2 Connect with Our Database
            Database databse = cosmosclient.GetDatabase(DatabaseName);
            // step 3 Connect with Our Container 
            Container container = databse.GetContainer(ContainerName);

            return container;
        }
        public TasksController()
        {
            _container = GetContainer();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(TasksDTO taskDto)
        {
            try
            {
                MyTasksModel taskEntity = new MyTasksModel();
                // mapping
                taskEntity.TaskName = taskDto.TaskName;
                taskEntity.TaskDescription = taskDto.TaskDescription;

                // mandatory feilds 
                taskEntity.Id = Guid.NewGuid().ToString(); // 16 didit hex code
                taskEntity.UId = Guid.NewGuid().ToString(); // taskEntity.Id; 
                taskEntity.DocumentType = "Mytasks";

                taskEntity.CreatedOn = DateTime.Now;
                taskEntity.CreatedByName = "Aniket";
                taskEntity.CreatedBy = "Aniket UID";

                taskEntity.UpdatedOn = DateTime.Now;
                taskEntity.UpdatedByName = "Aniket";
                taskEntity.UpdatedBy = "Aniket UID";

                taskEntity.Version = versionCount;
                taskEntity.Active = true;
                taskEntity.Archieved = false;  // Not Accesible to System

                MyTasksModel resposne = await _container.CreateItemAsync(taskEntity);

                // Reverse MAppin
                taskDto.TaskName = resposne.TaskName;
                taskDto.TaskDescription = resposne.TaskDescription;

                return Ok(taskDto);
            }
            catch (Exception ex)
            {
                return BadRequest("Data Adding Failed" + ex);
            }
        }

        // get all records which are store in cosmos DB
        [HttpGet]
        public IActionResult GetAllTasks()
        {
            try
            {
                var allTasks = _container.GetItemLinqQueryable<MyTasksModel>(true).AsEnumerable().ToList();
                return Ok(allTasks);    // 200 - succes 

            }
            catch (Exception ex)
            {
                return BadRequest("Error while getting result" + ex);
            }
        }

        // get particular record using id which is store in cosmos DB
        [HttpGet]
        public IActionResult GetParticularTaskByUId(string uId)
        {
            try
            {
                var particularTask = _container.GetItemLinqQueryable<MyTasksModel>(true).Where(q => q.UId == uId).AsEnumerable().ToList();
                return Ok(particularTask);    // 200 - succes 

            }
            catch (Exception ex)
            {
                return BadRequest("Error while getting result" + ex);
            }
        }
 
        // update data using id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(string id, TasksDTO td, string partitionKey)
        {
           
            try
            {
                ItemResponse<MyTasksModel> res = await _container.ReadItemAsync<MyTasksModel>(id, new PartitionKey(partitionKey));
                //Get Existing Item
                var existingItem = res.Resource;
                existingItem.TaskName = td.TaskName;
                existingItem.TaskDescription = td.TaskDescription;
                existingItem.Version++;

                //Replace existing item values with new values  
                var updateRes = await _container.ReplaceItemAsync(existingItem, id, new PartitionKey(partitionKey));
                return Ok(updateRes.Resource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(string id, string partitionKey)
        {
            try
            {
                ItemResponse<MyTasksModel> deleteResponse = await _container.DeleteItemAsync<MyTasksModel>(id, new PartitionKey(partitionKey));
                Console.WriteLine(deleteResponse);
                if (deleteResponse == null)
                {
                    return NotFound($"Task with id {id} not found");
                }
                return Ok($"Task deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Unable to Delete {ex.Message}");
            }

        }
    }
}
