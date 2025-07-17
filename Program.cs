using WorkflowEngine.Controllers;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapWorkflowEndpoints();

app.Run();
