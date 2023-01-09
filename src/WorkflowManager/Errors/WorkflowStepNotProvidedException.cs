namespace WorkflowManager.Errors;

public class WorkflowStepNotProvidedException : ExceptionBase
{
    public WorkflowStepNotProvidedException(string worflowCaller)
        : base(
            code: WorkflowExceptionCode.STEP_NOT_PROVIDED,
            message: $"No steps were provided to this workflow. Caller: {worflowCaller}"
        ) { }
}
