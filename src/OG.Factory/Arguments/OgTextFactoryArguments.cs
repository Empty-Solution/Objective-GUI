using OG.Common.Abstraction;
using OG.Factory.Abstraction;

namespace OG.Factory.Arguments;

public class OgTextFactoryArguments(string name, IOgTransform transform, string content) :
    OgContentFactoryArguments<string>(name, transform, content), IOgTextFactoryArguments;