using System.Collections;
using UnityEngine;

public class Ghost : MonoBehaviour, IControllable
{
    private Controller controllerRef;
    private static readonly int Selected = Animator.StringToHash("Selected");
    private House targetHouse;
    private Mover _mover;
    private Animator _animator;
    private static readonly int Party = Animator.StringToHash("Party");

    // Start is called before the first frame update
    void Start()
    {
        _mover = GetComponent<Mover>();
        controllerRef = FindObjectOfType<Controller>();
        FindObjectOfType<LevelController>().UpdateCurrentGhosts();
        _animator = GetComponent<Animator>();
    }

    public void EnterHouse()
    {
        targetHouse = null;
        GetComponent<Mover>().Stop();
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        controllerRef.Deselect(gameObject);
    }

    public void LeaveHouse(Vector2 spawnPos)
    {
        transform.position = (Vector3)spawnPos;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = true;
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
        _animator.SetBool(Selected, false);
    }

    public void HandleSelected()
    {
        _animator.SetBool(Party, false);
        _animator.SetBool(Selected, true);
    }

    public void StartParty(float partyTimer)
    {
        StartCoroutine(PartyTimer(partyTimer));
    }

    IEnumerator PartyTimer(float partyTimer)
    {
        _animator.SetBool(Party, true);
        yield return new WaitForSeconds(partyTimer);
        _animator.SetBool(Party, false);
    }
}
