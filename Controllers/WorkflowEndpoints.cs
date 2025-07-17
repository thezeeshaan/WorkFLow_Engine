using WorkflowEngine.Models;
using WorkflowEngine.Services;
using WorkflowEngine.Persistence;

namespace WorkflowEngine.Controllers;

public static class WorkflowEndpoints {
    public static void MapWorkflowEndpoints(this WebApplication app) {
        var defService = new WorkflowDefinitionService();
        var instService = new WorkflowInstanceService();

        app.MapPost("/workflows", (WorkflowDefinition def) => {
            var success = defService.CreateWorkflowDefinition(def, out var error);
            return success ? Results.Ok(def) : Results.BadRequest(error);
        });

        app.MapGet("/workflows/{id}", (string id) => {
            var def = defService.GetWorkflowDefinition(id);
            return def != null ? Results.Ok(def) : Results.NotFound();
        });

        app.MapPost("/instances", (string definitionId) => {
            var instance = instService.StartInstance(definitionId, out var error);
            return instance != null ? Results.Ok(instance) : Results.BadRequest(error);
        });

        app.MapPost("/instances/{id}/actions/{actionId}", (string id, string actionId) => {
            var (success, error) = instService.ExecuteAction(id, actionId);
            return success ? Results.Ok(InMemoryStore.Instances[id]) : Results.BadRequest(error);
        });

        app.MapGet("/instances/{id}", (string id) => {
            var inst = instService.GetInstance(id);
            return inst != null ? Results.Ok(inst) : Results.NotFound();
        });
    }
}