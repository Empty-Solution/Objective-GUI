using OG.DataTypes.KeyCode;
namespace OG.Unity.Event.Prefab;
public class OgUnityKeyEvent : OgUnityKeyboardEvent
{
    public EOgKeyCode KeyCode   { get; set; }
    public char       Character { get; set; }
}