using Grpc.Core;
using static GrpcService.Core.Person.Types;

namespace GrpcService.Core.Services
{
    public class PersonService : MyService.MyServiceBase
    {
        private readonly ILogger<PersonService> _logger;
        public PersonService(ILogger<PersonService> logger)
        {
            _logger = logger;
        }

        public override Task<Person> SendMessage(SearchPerson request, ServerCallContext context)
        {
            try
            {
                _logger.LogInformation("Begin to send message.");
                var person = new Person
                {
                    Name = request.Name + ": from server",
                    Email = "1111.com",
                    Id = request.Id,
                    Married = request.Married,
                    Salary = request.Salary + 10000,
                };
                person.Phones.Add(new Person.Types.PhoneNumber
                {
                    Number = "9527",
                    Type = PhoneType.Home
                });
                return Task.FromResult(person);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Send message with error,err:{ex}");
                throw;
            }
        }
    }
}
