namespace OG.Factory.Abstraction;

public interface IOgSliderFactoryArguments : IOgRangeValueFactoryArguments<float>
{
    float ScrollStep { get; set; }
}