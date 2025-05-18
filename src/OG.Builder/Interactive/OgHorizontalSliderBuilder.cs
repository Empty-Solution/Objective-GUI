using DK.Processing.Abstraction.Generic;
using OG.Builder.Contexts.Interactive;
using OG.Element.Visual.Abstraction;
using OG.Factory.Interactive;
namespace OG.Builder.Interactive;
public class OgHorizontalSliderBuilder(OgHorizontalSliderFactory<IOgVisualElement> factory, IDkProcessor<OgSliderBuildContext> processor)
    : OgSliderBuilder<OgHorizontalSliderFactory<IOgVisualElement>>(factory, processor);