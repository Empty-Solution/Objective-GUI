using OG.Common.Abstraction;
using OG.Factory.Abstraction;
using UnityEngine;

namespace OG.Factory.Arguments;

public class OgTextureFactoryArguments(string name, IOgTransform transform, Texture content) :
    OgContentFactoryArguments<Texture>(name, transform, content), IOgTextureFactoryArguments;