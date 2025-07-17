using WorkflowEngine.Models;
using WorkflowEngine.Persistence;

namespace WorkflowEngine.Services;

public class WorkflowInstanceService {
    public WorkflowInstance? StartInstance(string definitionId, out string error) {
        error = string.Empty;
        if (!InMemoryStore.Definitions.TryGetValue(definitionId, out var def)) {
            error = "Definition not found";
            return null;
        }

        var initial = def.States.FirstOrDefault(s => s.IsInitial && s.Enabled);
        if (initial == null) {
            error = "No enabled initial state found.";
            return null;
        }

        var instance = new WorkflowInstance {
            WorkflowDefinitionId = def.Id,
            CurrentStateId = initial.Id
        };
        InMemoryStore.Instances[instance.Id] = instance;
        return instance;
    }

    public (bool success, string error) ExecuteAction(string instanceId, string actionId) {
        if (!InMemoryStore.Instances.TryGetValue(instanceId, out var instance))
            return (false, "Instance not found");

        if (!InMemoryStore.Definitions.TryGetValue(instance.WorkflowDefinitionId, out var def))
            return (false, "Definition not found");

        var action = def.Actions.FirstOrDefault(a => a.Id == actionId);
        if (action == null || !action.Enabled)
            return (false, "Invalid or disabled action.");

        if (!action.FromStates.Contains(instance.CurrentStateId))
            return (false, "Current state is not valid for this action.");

        if (!def.States.Any(s => s.Id == action.ToState))
            return (false, "ToState does not exist in definition.");

        if (def.States.First(s => s.Id == instance.CurrentStateId).IsFinal)
            return (false, "Cannot act on a final state.");

        instance.CurrentStateId = action.ToState;
        instance.History.Add((actionId, DateTime.UtcNow));
        return (true, string.Empty);
    }

    public WorkflowInstance? GetInstance(string id) {
        InMemoryStore.Instances.TryGetValue(id, out var inst);
        return inst;
    }
}