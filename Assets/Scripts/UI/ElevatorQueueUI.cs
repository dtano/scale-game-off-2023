using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

// This is mostly a copy of the ReservesUI script, so will need to somehow make it more modular
public class ElevatorQueueUI : MonoBehaviour
{
    [SerializeField] private GameObject _employeeIconPrefab;
    [SerializeField] private Transform _employeeIconsParent;
    [SerializeField] private Button _rightArrow;
    [SerializeField] private Button _leftArrow;

    private const int MAX_ICONS_PER_PAGE = 8;
    private int _currentPage;
    private List<PassengerIcon> _passengerIcons = new List<PassengerIcon>();

    public UnityAction OnClickArrowEvent; // Maybe this arrow event could be placed in an event channel?

    // Start is called before the first frame update
    void Awake()
    {
        //InstantiateEmptyIcons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickNextArrow()
    {
        _currentPage++;
        OnClickArrowEvent?.Invoke();
    }

    public void OnClickBackArrow()
    {
        _currentPage--;
        OnClickArrowEvent?.Invoke();
    }

    public void UpdateView(List<Employee> elevatorList)
    {
        // Show everything except the head of the queue
        int maxPages = (int)Math.Ceiling((double)elevatorList.Count / MAX_ICONS_PER_PAGE);
        if (_currentPage > maxPages - 1 && _currentPage > 0)
        {
            _currentPage--;
        }

        int i = 0;
        while (i < MAX_ICONS_PER_PAGE)
        {
            int index = i + (_currentPage * MAX_ICONS_PER_PAGE);
            GameObject iconObject = _employeeIconsParent.GetChild(i).gameObject;
            PassengerIcon passengerIcon = _passengerIcons[i];

            if (index >= elevatorList.Count)
            {
                iconObject.SetActive(false);
            }
            else
            {
                Employee employeeToSet = elevatorList[index];
                passengerIcon.SetData(employeeToSet, index);
                iconObject.SetActive(true);
            }

            i++;
        }

        ControlArrowsVisibility(maxPages);
    }

    private void ControlArrowsVisibility(int maxPages)
    {
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
        for (int j = 0; j < MAX_ICONS_PER_PAGE - originalChildCount; j++)
        {
            Instantiate(_employeeIconPrefab, _employeeIconsParent);
        }

        foreach (Transform child in _employeeIconsParent)
        {
            if (child.TryGetComponent(out PassengerIcon passengerIcon))
            {
                _passengerIcons.Add(passengerIcon);
            }
            child.gameObject.SetActive(false);
        }
    }
}
