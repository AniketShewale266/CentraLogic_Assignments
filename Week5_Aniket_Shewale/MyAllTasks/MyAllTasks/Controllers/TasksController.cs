using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Serialization.HybridRow.Schemas;
using MyAllTasks.Entity;
using MyAllTasks.DTO;
using System.ComponentModel;
using System.Threading.Tasks;
using Container = Microsoft.Azure.Cosmos.Container;
using PartitionKey = Microsoft.Azure.Cosmos.PartitionKey;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Concurrent;
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
            //Connect with Our Database
            Database databse = cosmosclient.GetDatabase(DatabaseName);
            //Connect with Our Container 
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
                taskEntity.UId = taskEntity.Id; // taskEntity.Id; 
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

                // Reverse Mapping
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

        // get particular record using uid which is store in cosmos DB
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
 
        // update data using uid
        [HttpPost]
        public async Task<IActionResult> UpdateTask(string uid, TasksDTO td)
        {         
            try
            {
                ItemResponse<MyTasksModel> res = await _container.ReadItemAsync<MyTasksModel>(uid, new PartitionKey(uid));
                //Get Existing Item
                var existingItem = res.Resource;
                //existingItem.Id = existingItem.Id;
                //existingItem.UId = existingItem.UId;
                existingItem.TaskName = td.TaskName;
                existingItem.TaskDescription = td.TaskDescription;
                existingItem.Version = existingItem.Version + 1;
               // existingItem.UpdatedBy = existingItem.UpdatedBy;
               // existingItem.UpdatedOn = existingItem.UpdatedOn;

                //Replace existing item values with new values  
                var updateRes = await _container.ReplaceItemAsync(existingItem, uid, new PartitionKey(uid));

                //var newRes = await _container.CreateItemAsync(existingItem);

                return Ok(updateRes.Resource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // delete data using uid
        [HttpPost("{uid}")]
        public async Task<IActionResult> DeleteTask(string uid)
        {
            try
            {
                ItemResponse<MyTasksModel> deleteResponse = await _container.DeleteItemAsync<MyTasksModel>(uid, new PartitionKey(uid));
                Console.WriteLine(deleteResponse);
                if (deleteResponse == null)
                {
                    return NotFound($"Task with uid {uid} not found");
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
