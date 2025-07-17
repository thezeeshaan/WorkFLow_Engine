namespace WorkflowEngine.Models;

public class State {
    public string Id { get; set; } = string.Empty;
    public bool IsInitial { get; set; }
    public bool IsFinal { get; set; }
    public bool Enabled { get; set; }
}
