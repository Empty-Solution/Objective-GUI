using DK.Scoping.Abstraction;
using OG.Common.Abstraction;

namespace OG.Common.Scoping.Abstraction;

public interface IOgTransformScope : IDkScope
{
    void Focus(IOgTransform transform);
}