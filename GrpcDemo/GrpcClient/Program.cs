﻿
using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;

//var input = new HelloRequest { Name = "Tim" };

//var channel = GrpcChannel.ForAddress("https://localhost:7206/");
//var client = new Greeter.GreeterClient(channel);

//var reply = await client.SayHelloAsync(input); 

//Console.WriteLine(reply.Message);

var channel = GrpcChannel.ForAddress("https://localhost:7206/");
var customerClient = new Customer.CustomerClient(channel);
var clientRequested = new CustomerLookupModel { UserId = 2}; 

var customer = await customerClient.GetCustomerInfoAsync(clientRequested);

Console.WriteLine($"{customer.FirstName} { customer.LastName}");

using (var call = customerClient.GetNewCustomers(new NewCustomerRequest()))
{
    while( await call.ResponseStream.MoveNext())
    {
        var curr = call.ResponseStream.Current;
        Console.WriteLine($"{curr.FirstName} {curr.LastName} : {curr.Email}");
    }
}

Console.ReadLine();
