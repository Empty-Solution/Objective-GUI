using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using OG.Element.Visual;
using OG.Factory.Abstraction;
using OG.Graphics.Abstraction;
using OG.Style.Abstraction;

namespace OG.Factory.Visual;

public class OgTextFactory(IOgTextStyle style, IOgTransformScope scope, IOgGraphics<IOgTextGraphicsContext<IOgTextStyle>> graphics) :
    OgAbstractTextFactory<IOgTextFactoryArguments, IOgGraphics<IOgTextGraphicsContext<IOgTextStyle>>, IOgTextGraphicsContext<IOgTextStyle>>(style, scope, graphics)
{
    protected override IOgVisualContent<string> InternalCreate(
        string name, string content, IOgTransform transform,
        IOgTextStyle style, IOgGraphics<IOgTextGraphicsContext<IOgTextStyle>> graphics) =>
        new OgText<IOgTextStyle, IOgTransformScope>(name, scope, transform, style, content, graphics);
}