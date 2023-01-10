using WorkflowManager;

namespace SampleApp;

public class SampleStore : IStore
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool ShouldRunNextStep { get; set; } = true;
}
