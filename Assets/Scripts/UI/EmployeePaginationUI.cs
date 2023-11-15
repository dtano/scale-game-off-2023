using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class EmployeePaginationUI : MonoBehaviour
{
    [SerializeField] private int _maxIconsPerPage;
    [SerializeField] private Transform _employeeIconsParent;
    [SerializeField] private GameObject _employeeIconPrefab;
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _nextButton;

    public UnityAction<int> OnClickPassengerIconEvent;
    public UnityAction OnClickArrowEvent; // Maybe this arrow event could be placed in an event channel?

    private List<PassengerIcon> _passengerIcons = new List<PassengerIcon>();
    private int _currentPage = 0;

    // Start is called before the first frame update
    void Awake()
    {
        InstantiateEmptyIcons();

        _backButton.gameObject.SetActive(false);
        _nextButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateView(List<Employee> reservesQueue, Employee currentDisplayedEmployee)
    {
        int maxPages = (int)Math.Ceiling((double)reservesQueue.Count / _maxIconsPerPage);
        if (_currentPage > maxPages - 1 && _currentPage > 0)
        {
            _currentPage--;
        }

        int i = 0;
        while (i < _maxIconsPerPage)
        {
            int index = i + (_currentPage * _maxIconsPerPage);
            GameObject iconObject = _employeeIconsParent.GetChild(i).gameObject;
            PassengerIcon passengerIcon = _passengerIcons[i];

            if (index >= reservesQueue.Count)
            {
                iconObject.SetActive(false);
            }
            else
            {
                Employee employeeToSet = reservesQueue[index];
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

        ControlArrowsVisibility(maxPages);
    }

    private void ControlArrowsVisibility(int maxPages)
    {
        // Determine which buttons to show
        if (_currentPage - 1 < 0)
        {
            _backButton.gameObject.SetActive(false);
        }
        else
        {
            _backButton.gameObject.SetActive(true);
        }

        if (_currentPage >= maxPages - 1)
        {
            _nextButton.gameObject.SetActive(false);
        }
        else
        {
            _nextButton.gameObject.SetActive(true);
        }
    }

    public void OnClickNextButton()
    {
        _currentPage++;
        OnClickArrowEvent?.Invoke();
    }

    public void OnClickBackButton()
    {
        _currentPage--;
        OnClickArrowEvent?.Invoke();
    }

    private void InstantiateEmptyIcons()
    {
        int originalChildCount = _employeeIconsParent.childCount;
        for (int j = 0; j < _maxIconsPerPage - originalChildCount; j++)
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

    public void SetPassengerIconClickEvents(UnityAction<int> onClickEvent)
    {
        foreach (PassengerIcon passengerIcon in _passengerIcons)
        {
            passengerIcon.OnIconClickEvent += onClickEvent;
        }
    }
}
