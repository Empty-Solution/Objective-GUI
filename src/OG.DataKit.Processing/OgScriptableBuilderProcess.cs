using OG.Builder.Abstraction;
using System;
namespace OG.DataKit.Processing;
public class OgScriptableBuilderProcess<TContext>(Action<TContext> action) : OgBuilderProcess<TContext> where TContext : IOgBuildContext
{
    protected override void InternalExecute(TContext target) => action.Invoke(target);
}