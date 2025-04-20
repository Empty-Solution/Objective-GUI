using OG.Common.Scoping.Abstraction;
using UnityEngine;

namespace OG.Factory.Abstraction;

public interface IOgTextureFactoryArguments<TScope> : IOgContentFactoryArguments<Texture, TScope> where TScope : IOgTransformScope;