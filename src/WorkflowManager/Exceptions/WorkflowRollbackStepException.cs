namespace WorkflowManager.Exceptions;

public class WorkflowRollbackStepException : ExceptionBase
{
    public WorkflowRollbackStepException(string step, string errorMessage)
        : base(
            code: WorkflowExceptionCode.ROLLBACK_STEP_ERROR,
            message: $"Error rolling back step {step}: {errorMessage}"
        ) { }
}
