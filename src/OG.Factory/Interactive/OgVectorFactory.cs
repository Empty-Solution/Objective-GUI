using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using OG.Element.Interactive;
using OG.Factory.Abstraction;
using OG.Factory.General;
using UnityEngine;

namespace OG.Factory.Interactive;

public class OgVectorFactory(IOgTransformScope scope) : OgFactory<IOgElement, IOgRangeValueFactoryArguments<Vector2>>
{
    public override string TypeName { get; } = "Vector";

    public override IOgElement Create(IOgRangeValueFactoryArguments<Vector2> arguments) =>
        new OgVector<IOgElement, IOgTransformScope>(arguments.Name, scope, arguments.Transform, arguments.Value, arguments.Range);
}