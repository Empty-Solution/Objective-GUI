namespace OG.Builder.Arguments.Interactive;
public class OgModalButtonBuildArguments(string name, bool rightClickOnly) : OgElementBuildArguments(name)
{
    public bool RightClickOnly { get; } = rightClickOnly;
}