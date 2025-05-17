using DK.Getting.Abstraction.Generic;
namespace OG.Animator.Abstraction;
public interface IOgAnimator<TValue> where TValue : notnull
{
    IDkGetProvider<float>? Speed { get; }
    TValue                 Value { get; }
    void Animate(float deltaTime, TValue targetValue);
}