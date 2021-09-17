using UnityEngine;

// Singleton Script for the persistant object script
// (Used as a parent for other persistant objects)

public class Persistence : MonoBehaviour
{
    static Persistence _instance;

    public static Persistence Instance
    {
        get { return _instance;}
    }
    private void Awake() {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
    }
}
