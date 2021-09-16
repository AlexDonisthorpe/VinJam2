// Interface for the clickable objects within the game (Ghost & House)

public interface IControllable
{
    void HandleRightClick();
    void HandleDeselect();
    void HandleSelected();
}
