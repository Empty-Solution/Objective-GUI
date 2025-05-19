using DK.Processing.Abstraction;
using DK.Processing.Abstraction.Generic;
using OG.Builder.Abstraction;
using OG.Element.Abstraction;
namespace OG.DataKit.Processing;
public abstract class OgBuilderProcess<TContext, TElement> : IDkProcess<TContext> where TContext : IOgBuildContext<TElement> where TElement : IOgElement
{
    public bool IsActive { get; set; } = true;
    public void Execute(IDkProcessor caller, object target)
    {
        if(caller is IDkProcessor<TContext> castedCaller && target is TContext castedTarget) Execute(castedCaller, castedTarget);
    }
    public void Execute(IDkProcessor<TContext> caller, TContext target) => InternalExecute(target);
    protected abstract void InternalExecute(TContext target);
}