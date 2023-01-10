using SampleApp;
using SampleApp.Steps;
using WorkflowManager;

Console.WriteLine("Workflow Sample!");

var store = new SampleStore();
var workflow = new Workflow<SampleStore>();

workflow
    .WithStore(store)
    .WithStep(new FirstStep())
    .WithStep(new SecondStep())
    .WithStep(new FinalStep());

await workflow.Process();

//await workflow.Rollback();
