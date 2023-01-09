namespace WorkflowManager;

public interface IStore
{
    bool ShouldRunNextStep { get; set; }
}