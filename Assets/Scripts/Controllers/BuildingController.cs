using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// How can I tell if the game is over?
public class BuildingController : MonoBehaviour
{
    [SerializeField] private BuildingDataSO _buildingData;
    [SerializeField] private EmployeeSpawner _employeeSpawner;
    [SerializeField] private ReservesController _reservesController;
    [SerializeField] private ElevatorQueue _elevatorQueue;
    [SerializeField] private List<Elevator> _elevators;
    [SerializeField] private BuildingInfoUI _buildingInfoUI;
    [SerializeField] private GameFinishedUI _gameFinishedUI;
    [SerializeField] private Clock _clock;

    [SerializeField] private DragAndDropEventChannel _dragAndDropEventChannel;
    [SerializeField] private DragAndDropEventChannel _reservesDragAndDropEventChannel;
    [SerializeField] private TabletInteractionEventChannel _tabletInteractionEventChannel;
    [SerializeField] private AudioEventChannel _sfxEventChannel;
    [SerializeField] private GameStateEventChannel _gameStateEventChannel;
    [SerializeField] private EmployeePaginationUI _elevatorQueueUI;
    [SerializeField] private ProgressIndicator _progressIndicator;
    [SerializeField] private HUDController _hudController;
    [SerializeField] private GameDetailsSO _gameDetailsSO;

    private Employee _currentFirstEmployee;
    private int _totalEmployeesInBuilding;
    private int _servedEmployeesCount;

    public List<Elevator> Elevators => _elevators;

    // Start is called before the first frame update
    void Start()
    {
        if(_elevatorQueue == null)
        {
            _elevatorQueue = GetComponent<ElevatorQueue>();
        }

        InitEventListeners();
    }

    private void InitEventListeners()
    {
        if (_dragAndDropEventChannel != null)
        {
            _dragAndDropEventChannel.OnSuccessfulDropEvent += OnAddEmployeeToElevator;
        }

        if (_reservesDragAndDropEventChannel != null)
        {
            _reservesDragAndDropEventChannel.OnSuccessfulDropEvent += OnDropEmployeeInReserves;
        }

        if (_tabletInteractionEventChannel != null)
        {
            _tabletInteractionEventChannel.OnKickEmployeeFromElevatorEvent += OnKickEmployeeFromElevator;
        }

        if (_gameStateEventChannel != null)
        {
            _gameStateEventChannel.OnReleaseEmployeesInFloorEvent += RecordReleasedEmployeesInFloor;
            _gameStateEventChannel.OnAllEmployeesServedEvent += TriggerGameWon;
            _gameStateEventChannel.OnTimeLimitReachedEvent += TriggerGameOver;
            _gameStateEventChannel.OnSceneTransitionOverEvent += StartGame;
            _gameStateEventChannel.OnEndTutorialEvent += StartGame;
        }
    }

    private void UnregisterEventListeners()
    {
        if (_dragAndDropEventChannel != null)
        {
            _dragAndDropEventChannel.OnSuccessfulDropEvent -= OnAddEmployeeToElevator;
        }

        if (_reservesDragAndDropEventChannel != null)
        {
            _reservesDragAndDropEventChannel.OnSuccessfulDropEvent -= OnDropEmployeeInReserves;
        }

        if (_tabletInteractionEventChannel != null)
        {
            _tabletInteractionEventChannel.OnKickEmployeeFromElevatorEvent -= OnKickEmployeeFromElevator;
        }

        if (_gameStateEventChannel != null)
        {
            _gameStateEventChannel.OnReleaseEmployeesInFloorEvent -= RecordReleasedEmployeesInFloor;
            _gameStateEventChannel.OnAllEmployeesServedEvent -= TriggerGameWon;
            _gameStateEventChannel.OnTimeLimitReachedEvent -= TriggerGameOver;
            _gameStateEventChannel.OnSceneTransitionOverEvent -= StartGame;
            _gameStateEventChannel.OnEndTutorialEvent -= StartGame;
        }
    }

    private void StartGame()
    {
        if(_gameDetailsSO != null && _gameDetailsSO.ShowTutorialScreen)
        {
            _gameDetailsSO.ShowTutorialScreen = false;
            _hudController.ShowTutorial();
            return;
        }
        InitLevel();
    }

    private void InitLevel()
    {
        foreach (Elevator elevator in _elevators)
        {
            elevator.SetTotalFloors(_buildingData.NumFloors);
            elevator.OpenElevator();
        }

        // Then initiate the steps
        SpawnEmployees();

        // Once employees are spawned, take out the first employee to be displayed in game
        _currentFirstEmployee = _elevatorQueue.GetNextInQueue();

        InitUI();

        _clock.TurnOn();
    }

    private void InitUI()
    {
        if(_progressIndicator != null)
        {
            _progressIndicator.InitValues(_employeeSpawner.NumEmployeesToSpawn);
        }
        if (_elevatorQueueUI != null)
        {
            _elevatorQueueUI.OnClickArrowEvent += UpdateElevatorQueueUI;
            UpdateElevatorQueueUI();
        }
        if(_buildingInfoUI != null)
        {
            _buildingInfoUI.SetInformation(_buildingData);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // I'm placing it here cuz I'm worried that all the event calls will mess things up
        if (_totalEmployeesInBuilding > 0 && _servedEmployeesCount == _totalEmployeesInBuilding && (!GameStateManager.Instance.IsTimeLimitReached && !GameStateManager.Instance.IsGameOver))
        {
            Debug.Log("GAME IS WON");
            if (_gameStateEventChannel != null) _gameStateEventChannel.OnAllEmployeesServed();
        }
    }

    private void TriggerGameWon()
    {
        if(_gameFinishedUI != null)
        {
            if (_sfxEventChannel != null) _sfxEventChannel.RaiseOnPlayerWonEvent();
            _gameFinishedUI.Show();
            _gameFinishedUI.ShowGameWon(_servedEmployeesCount, _totalEmployeesInBuilding, _clock.ElapsedTime);
        }
    }

    private void TriggerGameOver()
    {
        if (GameStateManager.Instance.DidPlayerWin) return;
        if (_gameFinishedUI != null)
        {
            if (_sfxEventChannel != null) _sfxEventChannel.RaiseOnPlayerLostEvent();
            _gameFinishedUI.Show();
            _gameFinishedUI.ShowGameOver(_servedEmployeesCount, _totalEmployeesInBuilding);
        }
    }

    private void RecordReleasedEmployeesInFloor(List<Employee> releasedEmployees, int floorNum)
    {
        int releasedCount = 0;
        foreach(Employee employee in releasedEmployees)
        {
            if (employee == null) continue;
            Debug.Log($"{floorNum} : {employee.Weight}");
            //Destroy(employee); // Maybe destroying is unnecessary?
            releasedCount++;
        }

        _servedEmployeesCount += releasedCount;
        if (_progressIndicator != null) _progressIndicator.UpdateValue(_servedEmployeesCount);
    }

    private void SpawnEmployees()
    {
        // Then initiate the steps
        if (_employeeSpawner != null) _employeeSpawner.SpawnEmployees(_buildingData.NumFloors);

        _totalEmployeesInBuilding = _elevatorQueue.Count;
    }

    private void OnAddEmployeeToElevator(DraggableObject draggableObject)
    {
        if (draggableObject == null) return;

        if(draggableObject.TryGetComponent(out Employee employee))
        {
            // Need to check where the employee was dragged from
            if (QueueType.Elevator == employee.CurrentQueueType)
            {
                bool removalSuccess = _elevatorQueue.RemoveFromQueue(employee);
                if (!removalSuccess) Debug.Log("Failed to remove an employee from the elevatorQueue");
                _currentFirstEmployee = _elevatorQueue.GetNextInQueue();

                UpdateElevatorQueueUI();
            }
            else
            {
                _reservesController.RemoveEmployee(employee);
            }

            // Need to check if we've reached the end of the queue
            if (_currentFirstEmployee == null)
            {
                Debug.Log("Reached the end of queue");
            }
        }
    }

    // I think I can find a way to use just one event channel for this
    private void OnDropEmployeeInReserves(DraggableObject draggableObject)
    {
        // Remove from elevator queue
        // Get next employee in queue
        // 
        Debug.Log("OnDropEmployeeInReserves");
        if (draggableObject == null) return;

        if (draggableObject.TryGetComponent(out Employee employee))
        {
            _elevatorQueue.RemoveFromQueue(employee);
            _currentFirstEmployee = _elevatorQueue.GetNextInQueue();
            UpdateElevatorQueueUI();
        }
    }

    private void UpdateElevatorQueueUI()
    {
        if (_elevatorQueueUI != null)
        {
            List<Employee> employeesToDisplay = new List<Employee>();
            for(int i = 1; i < _elevatorQueue.Count; i++)
            {
                employeesToDisplay.Add(_elevatorQueue.GetByIndex(i));
            }
            _elevatorQueueUI.UpdateView(employeesToDisplay, null);
        }
    }

    private bool OnKickEmployeeFromElevator(Elevator elevator, Employee employee)
    {
        if (!_reservesController.AddToQueue(employee))
        {
            return false;
        }

        // Remove from elevator
        if (!elevator.RemoveFromElevator(employee))
        {
            return false;
        }

        // Add to reserves
        return true;
    }

    private void OnDisable()
    {
        UnregisterEventListeners();
    }

    private void OnDestroy()
    {
        UnregisterEventListeners();
    }
}
