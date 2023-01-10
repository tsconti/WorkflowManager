using WorkflowManager;

namespace SampleApp.Steps;

public class FirstStep : IWorkflowStep<SampleStore>
{
    public bool HasExecuted { get ; set; }

    public async Task<SampleStore> Execute(SampleStore store)
    {
        store.Description = "FirstStep";
        store.ShouldRunNextStep = false;

        return store;
    }

    public async Task<SampleStore> Rollback(SampleStore store)
    {
        store.Description = string.Empty;

        return store;
    }
}
