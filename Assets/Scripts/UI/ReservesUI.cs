using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System;

public class ReservesUI : MonoBehaviour
{
    private const int MAX_ICONS_IN_PAGE = 5;
    
    [SerializeField] private GameObject _employeeIconPrefab;
    [SerializeField] private Transform _employeeIconsParent;
    [SerializeField] private Button _rightArrow;
    [SerializeField] private Button _leftArrow;

    public UnityAction<int> OnClickPassengerIconEvent;
    public UnityAction OnClickArrowEvent;

    private List<PassengerIcon> _passengerIcons = new List<PassengerIcon>();
    private int _currentPage = 0;

    // Start is called before the first frame update
    void Awake()
    {
        InstantiateEmptyIcons();

        _leftArrow.gameObject.SetActive(false);
        _rightArrow.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickRightArrow()
    {
        // Need to know how many people are in the reserves queue
        // Then use that amount to determine which page
        _currentPage++;
        OnClickArrowEvent?.Invoke();
    }

    public void OnClickLeftArrow()
    {
        _currentPage--;
        OnClickArrowEvent?.Invoke();
    }

    public void UpdateView(ElevatorQueue reservesQueue, Employee currentDisplayedEmployee)
    {
        // Need to check if the amount of people in the reserves still allow for the current page to be displayed
        double pagesInDecimal = (double) reservesQueue.Count / MAX_ICONS_IN_PAGE;
        int maxPages = (int) Math.Ceiling(pagesInDecimal);

        if (_currentPage > maxPages - 1 && _currentPage > 0)
        {
            _currentPage--;
        }

        int i = 0;
        while(i < MAX_ICONS_IN_PAGE)
        {            
            int index = i + (_currentPage * MAX_ICONS_IN_PAGE); // I'm wrong here
            GameObject iconObject = _employeeIconsParent.GetChild(i).gameObject;
            PassengerIcon passengerIcon = _passengerIcons[i];

            if (index >= reservesQueue.Count)
            {
                iconObject.SetActive(false);
            }
            else
            {
                Employee employeeToSet = reservesQueue.GetByIndex(index);
                passengerIcon.SetData(employeeToSet, index);

                if (currentDisplayedEmployee != null && currentDisplayedEmployee == employeeToSet)
                {
                    passengerIcon.SetSelectedIndicator(true);
                }
                else
                {
                    passengerIcon.SetSelectedIndicator(false);
                }

                iconObject.SetActive(true);
            }

            i++;
        }

        // Determine which buttons to show
        if (_currentPage - 1 < 0)
        {
            _leftArrow.gameObject.SetActive(false);
        }
        else
        {
            _leftArrow.gameObject.SetActive(true);
        }

        if (_currentPage >= maxPages - 1)
        {
            _rightArrow.gameObject.SetActive(false);
        }
        else
        {
            _rightArrow.gameObject.SetActive(true);
        }
    }

    private void InstantiateEmptyIcons()
    {
        int originalChildCount = _employeeIconsParent.childCount;
        for (int j = 0; j < MAX_ICONS_IN_PAGE - originalChildCount; j++)
        {
            Instantiate(_employeeIconPrefab, _employeeIconsParent);
        }

        foreach(Transform child in _employeeIconsParent)
        {
            if (child.TryGetComponent(out PassengerIcon passengerIcon))
            {
                _passengerIcons.Add(passengerIcon);
            }
            child.gameObject.SetActive(false);
        }
    }

    public void SetPassengerIconClickEvents(UnityAction<int> onClickEvent)
    {
        foreach(PassengerIcon passengerIcon in _passengerIcons)
        {
            passengerIcon.OnIconClickEvent += onClickEvent;
        }
    }
}
