using WorkflowManager;

namespace SampleApp.Steps;

public class SecondStep : IWorkflowStep<SampleStore>
{
    public bool HasExecuted { get; set; }

    public async Task<SampleStore> Execute(SampleStore store)
    {
        store.Description = "SecondStep";

        return store;
    }

    public async Task<SampleStore> Rollback(SampleStore store)
    {
        store.Description = "FirstStep";

        return store;
    }
}
