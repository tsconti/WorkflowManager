using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkflowManager.Exceptions;

namespace WorkflowManager;

public class Workflow<T> where T : class, IStore
{
    private T _store;
    private IList<IWorkflowStep<T>> _steps;

    public Workflow()
    {
        _steps = new List<IWorkflowStep<T>>();
    }

    public Workflow<T> WithStore(T store)
    {
        _store = store;
        return this;
    }

    public Workflow<T> WithStep(IWorkflowStep<T> step)
    {
        _steps.Add(step);
        return this;
    }

    public async Task Process()
    {
        if (!_steps.Any())
        {
            throw new WorkflowStepNotProvidedException(this.GetType().BaseType.Name);
        }

        foreach (var step in _steps)
        {
            if (_store.ShouldRunNextStep)
            {
                try
                {                   
                    await step.Execute(_store);
                    step.HasExecuted = true;
                }
                catch (Exception ex)
                {
                    throw new WorkflowExecuteStepException(step.GetType().Name, ex.Message);
                }
            }
        }
    }

    public async Task Rollback()
    {
        var stepsToRollback = _steps
            .Where(s => s.HasExecuted == true)
            .Reverse();
        
        foreach (var step in stepsToRollback)
        {
            try
            {
                await step.Rollback(_store);
            }
            catch (Exception ex)
            {
                throw new WorkflowRollbackStepException(step.GetType().Name, ex.Message);
            }

            step.HasExecuted = true;
        }
    }
}
