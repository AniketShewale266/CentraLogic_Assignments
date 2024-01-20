using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Library_Management.DTO;
using Library_Management.Entities;
using Library_Management.Interface;
using Microsoft.Azure.Cosmos.Linq;

namespace Library_Management.Services
{
    public class BookService:IBookInterface
    {
        public readonly Container _container;
        private Container GetContainer()
        {
            string URI = Environment.GetEnvironmentVariable("URI");
            string PrimaryKey = Environment.GetEnvironmentVariable("PrimaryKey");
            string DatabaseName = Environment.GetEnvironmentVariable("DatabaseName");
            string ContainerName = Environment.GetEnvironmentVariable("ContainerNameL");
            CosmosClient cosmosclient = new CosmosClient(URI, PrimaryKey);
            //Connect with Our Database
            Database database = cosmosclient.GetDatabase(DatabaseName);
            //Connect with Our Container 
            Container container = database.GetContainer(ContainerName);

            return container;
        }
        public BookService()
        {
            _container = GetContainer();
        }
        public async Task<BookEntity> AddBook(BookEntity book)
        {
            book.Id = Guid.NewGuid().ToString(); // 16 didit hex code
            book.UId = book.Id; // taskEntity.Id; 
            book.DocumentType = "Book Information";

            book.CreatedOn = DateTime.Now;
            book.CreatedByName = "Anikets Library";
            book.CreatedBy = "Aniket UID";

            book.UpdatedOn = DateTime.Now;
            book.UpdatedByName = "Aniket";
            book.UpdatedBy = "Aniket UID";

            book.Version = 1;
            book.Active = true;
            book.Archieved = false;  // Not Accesible to System

            book.IsAvailable = true;
            book.IssuedToUserId = null;

            BookEntity resposne = await _container.CreateItemAsync(book);

            return resposne;
        }

        public async Task<IEnumerable<BookEntity>> ReadAllBooks()
        {
            try
            {
                var sqlQuery = "SELECT * FROM c";
                var queryDefinition = new QueryDefinition(sqlQuery);

                var result = new List<BookEntity>();
                var iterator = _container.GetItemQueryIterator<BookEntity>(queryDefinition);

                while (iterator.HasMoreResults)
                {
                    var response = await iterator.ReadNextAsync();
                    result.AddRange(response.Resource);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error while getting books", ex);
            }
        }

        public async Task<BookEntity> ReadBookById(string bookId)
        {
            try
            {
                ItemResponse<BookEntity> res = await _container.ReadItemAsync<BookEntity>(bookId, new PartitionKey(bookId));

                return res.Resource;
            
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error while reading book with ID {bookId}", ex);
            }
        }

        public async Task<BookEntity> UpdateBook(string bookId, BookEntity updatedBook)
        {
            try
            {
                // Retrieve the existing book from Cosmos DB
                var existingBook = await _container.ReadItemAsync<BookEntity>(bookId, new PartitionKey(bookId));

                if (existingBook == null)
                {
                    // not found
                    return null;
                }
                existingBook.Resource.Title = updatedBook.Title;
                existingBook.Resource.Author = updatedBook.Author;
                existingBook.Resource.Subject = updatedBook.Subject;

                var response = await _container.ReplaceItemAsync<BookEntity>(existingBook.Resource, existingBook.Resource.Id, new PartitionKey(existingBook.Resource.Id));

                return response.Resource;
            }
            catch (CosmosException ex)
            {
                throw new ApplicationException($"Error updating book with ID {bookId}: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteBookByUid(string uid)
        {
            try
            {
                ItemResponse<BookEntity> deleteResponse = await _container.DeleteItemAsync<BookEntity>(uid, new PartitionKey(uid));
                Console.WriteLine(deleteResponse);
                if (deleteResponse == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<BookEntity> IssueBook(string userId, string bookId)
        {
            try
            {
                // Check if the book is available
                var bookQuery = _container.GetItemLinqQueryable<BookEntity>(true)
                    .Where(b => b.Id == bookId && b.IsAvailable)
                    .AsEnumerable()
                    .FirstOrDefault();

                if (bookQuery != null)
                {
                    // Update book status 
                    bookQuery.IsAvailable = false;
                    bookQuery.IssuedToUserId = userId;
          
                    // Update the book entity in Cosmos DB
                    var response = await _container.ReplaceItemAsync(bookQuery, bookQuery.Id, new PartitionKey(bookQuery.Id));
                    return response.Resource;
                }
                else
                {
                    return null;
                }
            }
            catch (CosmosException ex)
            {
                throw new ApplicationException("Error while issuing the book", ex);
            }
        }




    }
}
