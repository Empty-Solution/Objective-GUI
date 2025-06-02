using DK.Processing.Abstraction.Generic;
using OG.Builder.Contexts.Interactive;
using OG.Factory.Interactive;
namespace OG.Builder.Interactive;
public class OgHorizontalSliderBuilder(OgHorizontalSliderFactory factory, IDkProcessor<OgSliderBuildContext>? processor)
    : OgSliderBuilder<OgHorizontalSliderFactory>(factory, processor);