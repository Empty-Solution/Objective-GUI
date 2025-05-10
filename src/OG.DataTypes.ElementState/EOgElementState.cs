namespace OG.DataTypes.ElementState;
public enum EOgElementState
{
    NORMAL,    // очевидно lol
    HOVERED,   // очевидно lol
    PRESSED,   // зажал кнопку условно
    FOCUSED,   // для текстфилда отдельно
    APPEAR,    // когда объект появляется
    DISAPPEAR, // когда объект пропадает
    ACTIVE     // выделенный объект (дрыгаем окно или thumb слайдера)
}