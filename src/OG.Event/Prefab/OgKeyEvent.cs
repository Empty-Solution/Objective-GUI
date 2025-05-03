using OG.DataTypes.KeyCode;
namespace OG.Event.Prefab;
public class OgKeyEvent : OgKeyboardEvent
{
    public EOgKeyCode KeyCode   { get; set; }
    public char       Character { get; set; }
}