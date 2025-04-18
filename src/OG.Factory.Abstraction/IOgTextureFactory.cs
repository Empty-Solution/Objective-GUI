using UnityEngine;

namespace OG.Factory.Abstraction;

public interface IOgTextureFactory<TArguments> : IOgVisualFactory<Texture, TArguments> where TArguments : IOgTextureFactoryArguments;
