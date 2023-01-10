using WorkflowManager;

namespace SampleApp.Steps;

public class FinalStep : IWorkflowStep<SampleStore>
{
    public bool HasExecuted { get; set; }

    public async Task<SampleStore> Execute(SampleStore store)
    {
        store.Description = "FinalStep";

        return store;
    }

    public async Task<SampleStore> Rollback(SampleStore store)
    {
        store.Description = "SecondStep";

        return store;
    }
}
