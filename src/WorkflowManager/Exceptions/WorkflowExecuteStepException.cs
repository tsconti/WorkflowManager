namespace WorkflowManager.Exceptions;

public class WorkflowExecuteStepException : ExceptionBase
{
    public WorkflowExecuteStepException(string step, string errorMessage)
        : base(
            code: WorkflowExceptionCode.EXECUTE_STEP_ERROR, 
            message: $"Error executing step {step}: {errorMessage}"
        ) { }
}
