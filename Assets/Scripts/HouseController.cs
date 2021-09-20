using System.Collections.Generic;
using UnityEngine;

public class HouseController : MonoBehaviour
{
    [SerializeField] private int[] maxActiveHousesPerLevel = new int[4]{2, 3, 5, 7};
    
    [SerializeField] private int maxActiveHouses = 1;
    [SerializeField] int _currentActiveHouses = 0;
    private List<House> _houses = new List<House>();

    private int currentSet = 0;

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
            GetRandomDisabledHouse().SetEnabled();
            _currentActiveHouses++;
        }
    }

    public House GetRandomDisabledHouse()
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
    
    public House GetRandomHouse()
    {
            int index = Random.Range(0, _houses.Count);
            return _houses[index];
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
                break;
            case 2:
                AddNewSetOfHouses(ref _secondSetOfHouses);
                break;
            case 3:
                AddNewSetOfHouses(ref _thirdSetOfHouses);
                break;
            case 4:
                AddNewSetOfHouses(ref _fourthSetOfHouses);
                break;
        }

        maxActiveHouses = maxActiveHousesPerLevel[currentSet-1];
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
