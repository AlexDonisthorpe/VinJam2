using UnityEngine;

public class Ghost : MonoBehaviour
{
    private Controller controllerRef;
    private static readonly int Selected = Animator.StringToHash("Selected");
    private House targetHouse;

    // Start is called before the first frame update
    void Start()
    {
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

    public void Select()
    {
        GetComponent<Animator>().SetBool(Selected, true);
    }

    public void Unselect()
    {
        GetComponent<Animator>().SetBool(Selected, false);
    }

    public void SetTargetHouse(House house)
    {
        targetHouse = house;
    }

    public House GetTargetHouse()
    {
        return targetHouse;
    }
}
