using Grpc.Core;
using GrpcServices; 

namespace GrpcServices.Services;

public class PersonService : GrpcServices.PersonService.PersonServiceBase
{
    private static readonly List<PersonResponse> _persons = new();
    private static int _nextId = 1;

    public override Task<PersonResponse> CreatePerson(CreatePersonRequest request, ServerCallContext context)
    {
        var person = new PersonResponse
        {
            Id = _nextId++,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Phone = request.Phone,
            Age = request.Age
        };
        _persons.Add(person);
        return Task.FromResult(person);
    }

    public override Task<PersonResponse> GetPerson(GetPersonRequest request, ServerCallContext context)
    {
        var person = _persons.FirstOrDefault(p => p.Id == request.Id);
        return person == null
            ? throw new RpcException(new Status(StatusCode.NotFound, "Person not found"))
            : Task.FromResult(person);
    }

    public override Task<PersonResponse> UpdatePerson(UpdatePersonRequest request, ServerCallContext context)
    {
        var person = _persons.FirstOrDefault(p => p.Id == request.Id);
        if (person == null)
            throw new RpcException(new Status(StatusCode.NotFound, "Person not found"));

        person.FirstName = request.FirstName;
        person.LastName = request.LastName;
        person.Email = request.Email;
        person.Phone = request.Phone;
        person.Age = request.Age;

        return Task.FromResult(person);
    }

    public override Task<DeletePersonResponse> DeletePerson(DeletePersonRequest request, ServerCallContext context)
    {
        var person = _persons.FirstOrDefault(p => p.Id == request.Id);
        if (person == null)
            throw new RpcException(new Status(StatusCode.NotFound, "Person not found"));

        _persons.Remove(person);
        return Task.FromResult(new DeletePersonResponse { Success = true });
    }
}