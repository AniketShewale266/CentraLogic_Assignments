﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Library_Management.DTO;
using Library_Management.Entities;
using Library_Management.Interface;
using Microsoft.Azure.Cosmos.Linq;

namespace Library_Management.Services
{
    public class UserService: IUserInterface
    {
        public readonly Container _container;
        private Container GetContainer()
        {
            string URI = Environment.GetEnvironmentVariable("URI");
            string PrimaryKey = Environment.GetEnvironmentVariable("PrimaryKey");
            string DatabaseName = Environment.GetEnvironmentVariable("DatabaseName");
            string ContainerName = Environment.GetEnvironmentVariable("ContainerName");
            CosmosClient cosmosclient = new CosmosClient(URI, PrimaryKey);
            //Connect with Our Database
            Database database = cosmosclient.GetDatabase(DatabaseName);
            //Connect with Our Container 
            Container container = database.GetContainer(ContainerName);

            return container;
        }
        public UserService()
        {
            _container = GetContainer();
        }
        public async Task<UserEntity> CreateUser(UserEntity user)
        {
            user.Id = Guid.NewGuid().ToString(); // 16 didit hex code
            user.UId = user.Id; // taskEntity.Id; 
            user.DocumentType = "User Information";

            user.CreatedOn = DateTime.Now;
            user.CreatedByName = "Aniket";
            user.CreatedBy = "Aniket UID";

            user.UpdatedOn = DateTime.Now;
            user.UpdatedByName = "Aniket";
            user.UpdatedBy = "Aniket UID";

            user.Version = 1;
            user.Active = true;
            user.Archieved = false;  // Not Accesible to System

            UserEntity resposne = await _container.CreateItemAsync(user);

            return resposne;
        }

        public async Task<string> LoginUser(UserEntity user)
        {
            var query = _container.GetItemLinqQueryable<UserEntity>()
                .Where(u => u.Username == user.Username && u.Password == user.Password)
                .Take(1)
                .ToFeedIterator();

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                var matchedUser = response.FirstOrDefault();

                if (matchedUser != null)
                {
                    return matchedUser.UId;
                }
            }

            return null;
        }
    }
}