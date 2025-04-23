namespace OG.Animation.Abstraction;

public interface IOgAnimator
{
    IOgCurve Curve { get; }
    bool Reversed { get; set; }
    bool Looped { get; set; }
    float Scale { get; set; }
    float Speed { get; set; }
    float Value { get; }
    void Animate(float deltaTime);
}