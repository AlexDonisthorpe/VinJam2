using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseController : MonoBehaviour
{
    [SerializeField] private int maxActiveHouses = 3;

    private int _currentActiveHouses = 0;
    private House[] _houses;
    
    // Start is called before the first frame update
    void Start()
    {
        _houses = GetComponentsInChildren<House>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentActiveHouses < maxActiveHouses)
        {
            GetRandomHouse().SetEnabled();
            _currentActiveHouses++;
        }
        
        
    }

    private House GetRandomHouse()
    {
        while (true)
        {
            int index = Random.Range(0, _houses.Length);

            if (!_houses[index].GetEnabled())
            {
                return _houses[index];
            }
        }
    }

    public void UpdateActiveHouses()
    {
        --_currentActiveHouses;
    }
}
