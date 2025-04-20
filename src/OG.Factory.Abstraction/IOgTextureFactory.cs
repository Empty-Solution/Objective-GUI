using OG.Common.Scoping.Abstraction;
using UnityEngine;

namespace OG.Factory.Abstraction;

public interface IOgTextureFactory<TArguments, TScope> : IOgVisualFactory<Texture, TArguments, TScope>
    where TScope : IOgTransformScope where TArguments : IOgTextureFactoryArguments<TScope>;