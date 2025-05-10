namespace OG.DataTypes.ElementState;
public enum EOgElementState
{
    IDLE,        // очевидно lol
    HOVERED,     // очевидно lol
    INTERACTING, // зажал кнопку условно
    FOCUSED,     // для текстфилда отдельно
    APPEAR,      // когда объект появляется
    DISAPPEAR,   // когда объект пропадает
    ACTIVE       // изменение значения
}