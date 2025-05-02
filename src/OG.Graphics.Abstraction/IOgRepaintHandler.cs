using DK.Matching.Abstraction;
using DK.Scoping.Extensions;
using OG.Graphics.Abstraction.Contexts;
namespace OG.Graphics.Abstraction;
public interface IOgRepaintHandler : IDkMatcher<IOgRepaintContext>
{
    DkScopeContext Handle(IOgRepaintContext context);
}