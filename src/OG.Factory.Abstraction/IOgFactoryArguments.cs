using DK.Common.Factory.Abstraction;
using OG.Common.Abstraction;

namespace OG.Factory.Abstraction;

public interface IOgFactoryArguments : IDkFactoryArguments
{
    string Name { get; }

    IOgTransform Transform { get; }
}
