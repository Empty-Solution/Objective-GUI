using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using OG.Element.Interactive;
using OG.Factory.Abstraction;
using OG.Factory.General;
using UnityEngine;

namespace OG.Factory.Interactive;

public class OgScrollFactory(IOgClipTransformScope scope) : OgFactory<IOgValueView<IOgElement, IOgClipTransformScope, Vector2>, IOgFactoryArguments>
{
    public override string TypeName { get; } = "Scroll";

    public override IOgValueView<IOgElement, IOgClipTransformScope, Vector2> Create(IOgFactoryArguments arguments) =>
        new OgScroll<IOgElement, IOgClipTransformScope>(arguments.Name, scope, arguments.Transform);
}