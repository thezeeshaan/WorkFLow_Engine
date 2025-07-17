using WorkflowEngine.Models;

namespace WorkflowEngine.Persistence;

public static class InMemoryStore {
    public static Dictionary<string, WorkflowDefinition> Definitions = new();
    public static Dictionary<string, WorkflowInstance> Instances = new();
}
