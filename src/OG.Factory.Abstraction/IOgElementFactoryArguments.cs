using DK.Common.Factory.Abstraction;
using OG.Common.Abstraction;

namespace OG.Factory.Abstraction;

public interface IOgElementFactoryArguments : IDkFactoryArguments
{
    string Name { get; }
    IOgTransform Transform { get; }
}