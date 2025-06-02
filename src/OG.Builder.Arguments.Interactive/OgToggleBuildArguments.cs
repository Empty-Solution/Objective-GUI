using DK.Property.Observing.Abstraction.Generic;
namespace OG.Builder.Arguments.Interactive;
public class OgToggleBuildArguments(string name, IDkObservableProperty<bool> value) : OgValueElementBuildArguments<bool>(name, value);