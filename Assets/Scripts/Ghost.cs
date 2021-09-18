using UnityEngine;

public class Ghost : MonoBehaviour, IControllable
{
    private Controller controllerRef;
    private static readonly int Selected = Animator.StringToHash("Selected");
    private House targetHouse;
    private Mover _mover;

    // Start is called before the first frame update
    void Start()
    {
        _mover = GetComponent<Mover>();
        controllerRef = FindObjectOfType<Controller>();
        FindObjectOfType<LevelController>().UpdateCurrentGhosts();
    }

    public void EnterHouse()
    {
        targetHouse = null;
        GetComponent<Mover>().Stop();
        GetComponent<SpriteRenderer>().enabled = false;
        controllerRef.Deselect(gameObject);
    }

    public void LeaveHouse(Vector2 spawnPos)
    {
        transform.position = (Vector3)spawnPos;
        GetComponent<SpriteRenderer>().enabled = true;
    }

    public void SetTargetHouse(House house)
    {
        targetHouse = house;
    }

    public House GetTargetHouse()
    {
        return targetHouse;
    }
    
    public void HandleRightClick()
    {
        Vector3 screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos = new Vector2(screenPos.x, screenPos.y);
        _mover.SetTargetLocation(mousePos);
    }

    public void HandleDeselect()
    {
        GetComponent<Animator>().SetBool(Selected, false);
    }

    public void HandleSelected()
    {
        GetComponent<Animator>().SetBool(Selected, true);
    }
}
