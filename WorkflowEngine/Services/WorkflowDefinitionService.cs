using WorkflowEngine.Models;
using WorkflowEngine.Persistence;

namespace WorkflowEngine.Services;

public class WorkflowDefinitionService {
    public bool CreateWorkflowDefinition(WorkflowDefinition def, out string error) {
        error = string.Empty;
        if (def.States.Count(s => s.IsInitial) != 1) {
            error = "Must have exactly one initial state.";
            return false;
        }
        InMemoryStore.Definitions[def.Id] = def;
        return true;
    }

    public WorkflowDefinition? GetWorkflowDefinition(string id) {
        InMemoryStore.Definitions.TryGetValue(id, out var def);
        return def;
    }
}