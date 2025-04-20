using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using OG.Element.Interactive;
using OG.Factory.Abstraction;
using UnityEngine;

namespace OG.Factory;

public class OgVectorFactory<TScope> : OgFactory<IOgElement, IOgRangeValueFactoryArguments<Vector2, TScope>, TScope> where TScope : IOgTransformScope
{
    public override string TypeName { get; } = "Vector";
    public override IOgElement Create(IOgRangeValueFactoryArguments<Vector2, TScope> arguments) => 
        new OgVector<IOgElement, TScope>(arguments.Name, arguments.Scope, arguments.Transform, arguments.Value, arguments.Range);
}