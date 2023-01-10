# Workflow Manager

This is a workflow manager.

## Usage

Create a class to represent the `Store` object that will be shared between workflow steps. 
That class has to implement `IStore` interface. 

```csharp
public class SampleStore : IStore
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool ShouldRunNextStep { get; set; } = true;
}
```

In your application:
- instantiate the store object 
- instantiate the workflow object setting the type with the store object type
- build the workflow object with the store and the steps
- call workflow's Process() method to execute
- if necessary you can call workflow's Rollback() method to execute your steps Rollback methods in reverse order.

```csharp
var store = new SampleStore();
var workflow = new Workflow<SampleStore>();

workflow
    .WithStore(store)
    .WithStep(new FirstStep())
    .WithStep(new SecondStep())
    .WithStep(new FinalStep());

await workflow.Process();

//await workflow.Rollback();
```
