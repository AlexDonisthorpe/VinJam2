using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseController : MonoBehaviour
{
    [SerializeField] private int maxActiveHouses = 1;
    [SerializeField] int _currentActiveHouses = 0;
    private List<House> _houses = new List<House>();

    private static int currentSet = 0;

    [SerializeField] private House[] _firstSetOfHouses;
    [SerializeField] private House[] _secondSetOfHouses;
    [SerializeField] private House[] _thirdSetOfHouses;
    [SerializeField] private House[] _fourthSetOfHouses;

    private bool gameStarted = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (var house in GetComponentsInChildren<House>())
        {
            _houses.Add(house);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted && _currentActiveHouses < maxActiveHouses )
        {
            GetRandomHouse().SetEnabled();
            _currentActiveHouses++;
        }
    }

    private House GetRandomHouse()
    {
        while (true)
        {
            int index = Random.Range(0, _houses.Count);

            if (!_houses[index].GetEnabled())
            {
                return _houses[index];
            }
        }
    }

    public void DecreaseActiveHouses()
    {
        --_currentActiveHouses;
    }

    public void UpdateTotalHouses()
    {
        currentSet++;

        switch (currentSet)
        {
            case 1:
                AddNewSetOfHouses(ref _firstSetOfHouses);
                maxActiveHouses += 1;
                break;
            case 2:
                AddNewSetOfHouses(ref _secondSetOfHouses);
                maxActiveHouses += 1;
                break;
            case 3:
                AddNewSetOfHouses(ref _thirdSetOfHouses);
                maxActiveHouses += 2;
                break;
            case 4:
                AddNewSetOfHouses(ref _fourthSetOfHouses);
                maxActiveHouses += 2;
                break;
        }
    }
    
    private void AddNewSetOfHouses(ref House[] newSet)
    {
        foreach (var house in newSet)
        {
            house.gameObject.SetActive(true);
            _houses.Add(house);
        }
    }

    public void StartHousing()
    {
        gameStarted = true;
    }
}
