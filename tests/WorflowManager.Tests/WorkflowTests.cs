using Moq;
using WorkflowManager;
using WorkflowManager.Exceptions;

namespace WorflowManager.Tests;

public class WorkflowTests
{
    IStore _storeMock;

    [SetUp]
    public void Setup()
    {
        _storeMock = new Mock<IStore>().SetupAllProperties().Object;
        _storeMock.ShouldRunNextStep = true;
    }

    [Test]
    public void Process_Should_Throw_WorkflowStepNotProvidedException_When_NoStepIsProvided()
    {
        var workflow = new Workflow<IStore>();

        workflow
            .WithStore(_storeMock);

        var ex = Assert.ThrowsAsync<WorkflowStepNotProvidedException>(() => workflow.Process());
        Assert.AreEqual(WorkflowExceptionCode.STEP_NOT_PROVIDED, ex.Code);
    }

    [Test]
    public void Process_Should_Throw_WorkflowExecuteStepException_When_StepExecuteMethodTrhowsException()
    {
        var stepMock = new Mock<IWorkflowStep<IStore>>();
        var workflow = new Workflow<IStore>();

        stepMock
            .Setup(x => x.Execute(It.IsAny<IStore>()))
            .Throws(new WorkflowExecuteStepException(It.IsAny<string>(), It.IsAny<string>()));

        workflow
            .WithStore(_storeMock)
            .WithStep(stepMock.Object);

        var ex = Assert.ThrowsAsync<WorkflowExecuteStepException>(() => workflow.Process());
        Assert.AreEqual(WorkflowExceptionCode.EXECUTE_STEP_EXCEPTION, ex.Code);
    }

    [Test]
    public void Process_Should_Succed_When_StepsExecuteSuccesfuly()
    {
        var stepOneMock = new Mock<IWorkflowStep<IStore>>();
        var stepTwoMock = new Mock<IWorkflowStep<IStore>>();
        var workflow = new Workflow<IStore>();

        stepOneMock
            .Setup(x => x.Execute(It.IsAny<IStore>()))
            .ReturnsAsync(It.IsAny<IStore>());

        stepTwoMock
            .Setup(x => x.Execute(It.IsAny<IStore>()))
            .ReturnsAsync(It.IsAny<IStore>());

        workflow
            .WithStore(_storeMock)
            .WithStep(stepOneMock.Object)
            .WithStep(stepTwoMock.Object);

        workflow.Process();

        stepOneMock
            .Verify(x => x.Execute(It.IsAny<IStore>()), Times.Once());
        stepTwoMock
            .Verify(x => x.Execute(It.IsAny<IStore>()), Times.Once());
    }

    [Test]
    public void Rollback_Should_Throw_WorkflowRollbackStepException_When_StepRollbackMethodTrhowsException()
    {
        var stepMock = new Mock<IWorkflowStep<IStore>>()
            .SetupAllProperties();
        var workflow = new Workflow<IStore>();

        stepMock
            .Setup(x => x.Rollback(It.IsAny<IStore>()))
            .Throws(new WorkflowRollbackStepException(It.IsAny<string>(), It.IsAny<string>()));
        stepMock.Object.HasExecuted = true;

        workflow
            .WithStore(_storeMock)
            .WithStep(stepMock.Object);

        var ex = Assert.ThrowsAsync<WorkflowRollbackStepException>(() => workflow.Rollback());
        Assert.AreEqual(WorkflowExceptionCode.ROLLBACK_STEP_EXCEPTION, ex.Code);
    }

    [Test]
    public void Rollback_Should_Succed_When_StepsRollbackSuccesfuly()
    {
        var stepOneMock = new Mock<IWorkflowStep<IStore>>()
            .SetupAllProperties();
        var stepTwoMock = new Mock<IWorkflowStep<IStore>>()
            .SetupAllProperties();
        var workflow = new Workflow<IStore>();

        stepOneMock
            .Setup(x => x.Rollback(It.IsAny<IStore>()))
            .ReturnsAsync(It.IsAny<IStore>());
        stepOneMock.Object.HasExecuted = true;

        stepTwoMock
            .Setup(x => x.Rollback(It.IsAny<IStore>()))
            .ReturnsAsync(It.IsAny<IStore>());
        stepTwoMock.Object.HasExecuted = true;

        workflow
            .WithStore(_storeMock)
            .WithStep(stepOneMock.Object)
            .WithStep(stepTwoMock.Object);

        workflow.Rollback();

        stepOneMock
            .Verify(x => x.Rollback(It.IsAny<IStore>()), Times.Once());
        stepTwoMock
            .Verify(x => x.Rollback(It.IsAny<IStore>()), Times.Once());
    }
}
