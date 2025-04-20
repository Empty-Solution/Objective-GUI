using OG.Common.Scoping.Abstraction;
using UnityEngine;

namespace OG.Factory.Abstraction;

public interface IOgTextureFactoryArguments<TScope> : IOgVisualFactoryArguments<Texture, TScope> where TScope : IOgTransformScope;