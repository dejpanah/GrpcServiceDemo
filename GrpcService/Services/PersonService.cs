using Grpc.Core;
using GrpcServices;
using Serilog;
namespace GrpcServices.Services;

public class PersonService : GrpcServices.PersonService.PersonServiceBase
{
    private static readonly List<PersonResponse> _persons = new();
    private static int _nextId = 1;

    public override Task<PersonResponse> CreatePerson(CreatePersonRequest request, ServerCallContext context)
    {
        try
        {
            Log.Information("Creating new person: {FirstName} {LastName}", request.FirstName, request.LastName);

            if (string.IsNullOrWhiteSpace(request.FirstName) || string.IsNullOrWhiteSpace(request.LastName))
            {
                Log.Warning("Invalid person data: FirstName or LastName is empty");
                throw new RpcException(new Status(StatusCode.InvalidArgument, "First name and last name are required"));
            }

            if (string.IsNullOrWhiteSpace(request.NationalCode) || request.NationalCode.Length != 10)
            {
                Log.Warning("Invalid nationalCode: NCode must be 10 Characters");
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid nationalCode: NCode must be 10 Characters"));
            }

            var person = new PersonResponse
            {
                Id = _nextId++,
                FirstName = request.FirstName,
                LastName = request.LastName,
                NationalCode = request.NationalCode,

            };
            _persons.Add(person);

            Log.Information("Successfully created person with ID: {Id}", person.Id);
            return Task.FromResult(person);
        }
        catch (RpcException)
        {
            throw;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error creating person");
            throw new RpcException(new Status(StatusCode.Internal, "An error occurred while creating the person"));
        }
    }

    public override Task<PersonResponse> GetPerson(GetPersonRequest request, ServerCallContext context)
    {
        try
        {
            Log.Information("Getting person with ID: {Id}", request.Id);

            if (request.Id <= 0)
            {
                Log.Warning("Invalid person ID: {Id}", request.Id);
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Person ID must be greater than 0"));
            }

            var person = _persons.FirstOrDefault(p => p.Id == request.Id);
            if (person == null)
            {
                Log.Warning("Person not found with ID: {Id}", request.Id);
                throw new RpcException(new Status(StatusCode.NotFound, $"Person with ID {request.Id} not found"));
            }

            Log.Information("Successfully retrieved person with ID: {Id}", person.Id);
            return Task.FromResult(person);
        }
        catch (RpcException)
        {
            throw;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error getting person with ID: {Id}", request.Id);
            throw new RpcException(new Status(StatusCode.Internal, "An error occurred while retrieving the person"));
        }
    }

    public override Task<PersonResponse> UpdatePerson(UpdatePersonRequest request, ServerCallContext context)
    {
        try
        {
            Log.Information("Updating person with ID: {Id}", request.Id);

            if (request.Id <= 0)
            {
                Log.Warning("Invalid person ID: {Id}", request.Id);
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Person ID must be greater than 0"));
            }

            if (string.IsNullOrWhiteSpace(request.FirstName) || string.IsNullOrWhiteSpace(request.LastName))
            {
                Log.Warning("Invalid person data: FirstName or LastName is empty");
                throw new RpcException(new Status(StatusCode.InvalidArgument, "First name and last name are required"));
            }

            if (string.IsNullOrWhiteSpace(request.NationalCode) || request.NationalCode.Length != 10)
            {
                Log.Warning("Invalid nationalCode: NCode must be 10 Characters");
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid nationalCode: NCode must be 10 Characters"));
            }

            var person = _persons.FirstOrDefault(p => p.Id == request.Id);
            if (person == null)
            {
                Log.Warning("Person not found with ID: {Id}", request.Id);
                throw new RpcException(new Status(StatusCode.NotFound, $"Person with ID {request.Id} not found"));
            }

            person.FirstName = request.FirstName;
            person.LastName = request.LastName;
            person.NationalCode = request.NationalCode;


            Log.Information("Successfully updated person with ID: {Id}", person.Id);
            return Task.FromResult(person);
        }
        catch (RpcException)
        {
            throw;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error updating person with ID: {Id}", request.Id);
            throw new RpcException(new Status(StatusCode.Internal, "An error occurred while updating the person"));
        }
    }

    public override Task<DeletePersonResponse> DeletePerson(DeletePersonRequest request, ServerCallContext context)
    {
        try
        {
            Log.Information("Deleting person with ID: {Id}", request.Id);

            if (request.Id <= 0)
            {
                Log.Warning("Invalid person ID: {Id}", request.Id);
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Person ID must be greater than 0"));
            }

            var person = _persons.FirstOrDefault(p => p.Id == request.Id);
            if (person == null)
            {
                Log.Warning("Person not found with ID: {Id}", request.Id);
                throw new RpcException(new Status(StatusCode.NotFound, $"Person with ID {request.Id} not found"));
            }

            _persons.Remove(person);
            Log.Information("Successfully deleted person with ID: {Id}", request.Id);
            return Task.FromResult(new DeletePersonResponse { Success = true });
        }
        catch (RpcException)
        {
            throw;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error deleting person with ID: {Id}", request.Id);
            throw new RpcException(new Status(StatusCode.Internal, "An error occurred while deleting the person"));
        }
    }
}