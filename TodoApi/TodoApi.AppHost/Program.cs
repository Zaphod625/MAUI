var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.TodoApi_ApiService>("apiservice");

builder.AddProject<Projects.TodoApi_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
