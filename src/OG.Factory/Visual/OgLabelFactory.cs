using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using OG.Element.Visual;
using OG.Factory.Abstraction;
using OG.Graphics.Abstraction;
using OG.Style.Abstraction;

namespace OG.Factory.Visual;

public class OgLabelFactory<TScope>(IOgTextStyle style, IOgGraphics<IOgTextGraphicsContext<IOgTextStyle>> graphics) :
    OgTextFactory<IOgTextStyle, IOgTextFactoryArguments<TScope>, IOgGraphics<IOgTextGraphicsContext<IOgTextStyle>>,
        IOgTextGraphicsContext<IOgTextStyle>, TScope>(style, graphics) where TScope : IOgTransformScope
{
    protected override IOgVisualContent<string> InternalCreate(string name, string content, IOgTransform transform,
        IOgTextStyle style, TScope scope, IOgGraphics<IOgTextGraphicsContext<IOgTextStyle>> graphics) =>
        new OgText<IOgTextStyle, TScope>(name, scope, transform, style, content, graphics);
}