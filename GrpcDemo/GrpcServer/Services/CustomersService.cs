using Grpc.Core;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GrpcServer.Services
{
    public class CustomersService : Customer.CustomerBase
    {
        private readonly ILogger<CustomersService> _logger;

        public CustomersService(ILogger<CustomersService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();

            if (request.UserId == 1)
            {
                output.FirstName = "Jamie";
                output.LastName = "Smith";
            }
            else if (request.UserId == 2)
            {
                output.FirstName = "Jane";
                output.LastName = "Doe";

            }
            else
            {
                output.FirstName = "Grey";
                output.LastName = "Thomas";
            }

            return Task.FromResult(output);
        }

        public override async Task GetNewCustomers(
            NewCustomerRequest request,
            IServerStreamWriter<CustomerModel> responseStream,
            ServerCallContext context)
        {
            List<CustomerModel> customers = new List<CustomerModel>
            {

                new CustomerModel
                {
                    FirstName = "Jamie",
                    LastName = "Smith",
                    Email = "dggss",
                    Age = 41,
                    IsAlive = true
                },
                new CustomerModel
                {
                    FirstName = "Jane",
                    LastName = "Doe",
                    Email = "shdyj",
                    Age = 41,
                    IsAlive = true

                },
                new CustomerModel
                {
                    FirstName = "Grey",
                    LastName = "Thomas",
                    Email = "kjlhjt",
                    Age = 41,
                    IsAlive = true
                },
            };

            foreach(var customer in customers)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(customer);
            }
        }
    }
}
