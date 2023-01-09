using System.Threading.Tasks;

namespace WorkflowManager;

public interface IWorkflowStep<T>
{
    bool HasExecuted { get; set; }
    Task<T> Execute(T store);
    Task<T> Rollback(T store);
}