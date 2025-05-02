using DK.Matching.Abstraction;
using OG.Graphics.Abstraction.Contexts;
namespace OG.Graphics.Abstraction;
public interface IOgRepaintHandler : IDkMatcher<IOgRepaintContext>
{
    bool Handle(IOgRepaintContext context);
}