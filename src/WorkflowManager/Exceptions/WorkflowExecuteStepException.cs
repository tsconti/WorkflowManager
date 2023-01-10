namespace WorkflowManager.Exceptions;

public class WorkflowExecuteStepException : ExceptionBase
{
    public WorkflowExecuteStepException(string step, string errorMessage)
        : base(
            code: WorkflowExceptionCode.EXECUTE_STEP_EXCEPTION, 
            message: $"Error executing step {step}: {errorMessage}"
        ) { }
}
