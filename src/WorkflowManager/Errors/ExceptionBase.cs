namespace WorkflowManager.Errors;

public abstract class ExceptionBase : Exception
{
    public string Code { get; }

	public ExceptionBase(string code, string message): base(message)
	{
		Code = code;
	}
}
