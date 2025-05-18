using Grpc.Net.Client;
using GrpcServices;

namespace GrpcClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("GRPC Demo...");

            var channel = GrpcChannel.ForAddress("https://localhost:7143");
            var client = new PersonService.PersonServiceClient(channel);

            #region Create Person

                var createResponse = await client.CreatePersonAsync(new CreatePersonRequest
                {
                    FirstName = "Ardavan",
                    LastName = "Dejpanah",
                    NationalCode = "1234567890",
                    BirthDate = new DateTime(1982,2,14).ToShortDateString()
                });
                Console.WriteLine($"Created Person ID: {createResponse.Id}");

            #endregion

            #region  Get Person

                var getResponse = await client.GetPersonAsync(new GetPersonRequest { Id = 1 });
                Console.WriteLine($"Get Person => Name: {getResponse.FirstName} {getResponse.LastName}");

            #endregion


            #region Update Person

                await client.UpdatePersonAsync(new UpdatePersonRequest
                {
                    Id = 1,
                    FirstName = "Arad",
                    LastName = "Dejpanah",
                    NationalCode = "0987654321",
                    BirthDate = new DateTime(2013,06, 11).ToShortDateString()
                });

            Console.WriteLine($"Update Person => Name: {getResponse.FirstName} {getResponse.LastName}");

            #endregion

            #region Delete Person

            var deleteResponse = await client.DeletePersonAsync(new DeletePersonRequest { Id = 1 });
                Console.WriteLine($"Delete successful: {deleteResponse.Success}");

            #endregion

        }
    }
}
