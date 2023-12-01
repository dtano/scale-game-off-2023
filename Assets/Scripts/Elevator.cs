using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Up,
    Down
}

public class Elevator : MonoBehaviour, IDroppable
{
    private const string IS_OPEN_ANIMATION_PARAM = "isOpen";
    
    [SerializeField] private ElevatorDataSO _elevatorData;
    [SerializeField] private HashSet<Employee> _passengers = new HashSet<Employee>();
    [SerializeField] private Direction _currentDirection = Direction.Up;
    [SerializeField] private ElevatorDisplayUI _elevatorDisplayUI;
    [SerializeField] private FloorNumberIndicator _floorNumberIndicator;
    [SerializeField] private GameStateEventChannel _gameStateEventChannel;
    [SerializeField] private AudioEventChannel _sfxEventChannel;

    private Animator _animator;
    private AudioSource _audioSource;
    private DroppableArea droppableArea;
    private Dictionary<int, List<Employee>> _destinationMap = new Dictionary<int, List<Employee>>();
    
    private bool _isMoving = false;
    private int _currentCapacity;
    private int _currentFloor;
    private int _totalFloors;

    public HashSet<Employee> Passengers => _passengers;
    public Dictionary<int, List<Employee>> DestinationMap => _destinationMap;
    public string Name => _elevatorData.Model;
    public int CurrentCapacity => _currentCapacity;
    public int MaxCapacity => _elevatorData.MaxCapacity;
    public float Speed => _elevatorData.SpeedInSeconds;
    public bool IsMoving => _isMoving;
    public int CurrentFloor => _currentFloor;
    public ElevatorDataSO Data => _elevatorData;

    // Start is called before the first frame update
    void Awake()
    {
        droppableArea = GetComponent<DroppableArea>();
        droppableArea.OnDropObjectEvent += OnDropObject;

        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        if(_elevatorDisplayUI != null)
        {
            _elevatorDisplayUI.SetInformation(_currentCapacity, _elevatorData.MaxCapacity);
            _elevatorDisplayUI.Hide();
        }

        if(_floorNumberIndicator != null)
        {
            _floorNumberIndicator.SetFloorNumber(_currentFloor);
            //_floorNumberIndicator.SwitchDirection(true);
        }
    }

    private bool CanAddToElevator(int weightToAdd)
    {
        return !_isMoving && (_currentCapacity + weightToAdd <= _elevatorData.MaxCapacity);
    }

    public void OpenElevator()
    {
        if (_animator != null)
        {
            _animator.SetBool(IS_OPEN_ANIMATION_PARAM, true);
        }

        if (_elevatorData.SfxCollection != null) PlaySound(_elevatorData.SfxCollection.OpenSfx);
    }

    public void InitiateMovement()
    {
        _isMoving = true;
    }

    private ErrorDTO CreateError()
    {
        ErrorDTO errorDTO = new ErrorDTO(ErrorSourceEnum.ELEVATOR_FULL, "Elevator is Full!");
        return errorDTO;
    }

    private void PlaySound(AudioCueSO audioCue)
    {
        if(_audioSource == null || audioCue == null) return;

        _audioSource.loop = audioCue.IsLooping;
        _audioSource.pitch = audioCue.Pitch;
        _audioSource.clip = audioCue.Clip;
        _audioSource.Play();
    }

    public DropResultDTO AddToElevator(Employee employee)
    {
        // Check weight here
        if (!CanAddToElevator(employee.Weight))
        {
            return new DropResultDTO(CreateError());
        }

        bool success = _passengers.Add(employee);
        if (!success)
        {
            return new DropResultDTO(CreateError());
        }

        // Map it in the destinationMap
        int destination = employee.DestinationFloor;
        if (_destinationMap.ContainsKey(destination))
        {
            _destinationMap[destination].Add(employee);
        }
        else
        {
            _destinationMap.Add(destination, new List<Employee>());
            _destinationMap[destination].Add(employee);
        }

        _currentCapacity += employee.Weight;
        if (_elevatorDisplayUI != null) _elevatorDisplayUI.SetInformation(_currentCapacity, _elevatorData.MaxCapacity);

        return new DropResultDTO(true);
    }

    public bool RemoveFromElevator(Employee employee)
    {
        bool success = _passengers.Remove(employee);
        if (!success)
        {
            return false;
        }

        if (_destinationMap.ContainsKey(employee.DestinationFloor))
        {
            _destinationMap[employee.DestinationFloor].Remove(employee);
        }

        _currentCapacity -= employee.Weight;
        if (_elevatorDisplayUI != null) _elevatorDisplayUI.SetInformation(_currentCapacity, _elevatorData.MaxCapacity);

        return true;
    }

    private void ReleasePassengersAtGivenFloor(int floorNumber)
    {
        if (!_destinationMap.ContainsKey(floorNumber)) return;

        List<Employee> passengersToRelease = new List<Employee>(_destinationMap[floorNumber]);
        foreach (Employee passenger in passengersToRelease)
        {
            RemoveFromElevator(passenger);
        }

        _destinationMap[floorNumber].Clear();
        _destinationMap.Remove(floorNumber);

        if (_gameStateEventChannel != null) _gameStateEventChannel.OnReleaseEmployeesInFloor(passengersToRelease, floorNumber);
    }

    private void OnReachedFloor(int floorNumber)
    {
        if (_floorNumberIndicator != null) _floorNumberIndicator.StopMovement();

        // Play some sort of sound
        if (_elevatorData.SfxCollection != null) PlaySound(_elevatorData.SfxCollection.ReachedDestinationSfx);
        
        ReleasePassengersAtGivenFloor(floorNumber);
    }

    public void OnPressCloseButton()
    {
        if (_isMoving || (GameStateManager.Instance.IsTabletOn || GameStateManager.Instance.IsGameOver)) return;

        if (_currentDirection == Direction.Up && _passengers.Count == 0)
        {
            return;
        }

        if (_currentDirection == Direction.Down && _currentFloor == 0) return;

        if (_elevatorData.SfxCollection != null)
        {
            PlaySound(_elevatorData.SfxCollection.CloseSfx);
            if (_sfxEventChannel != null) _sfxEventChannel.RaiseEvent(_elevatorData.SfxCollection.PressCloseButtonSfx);
        }
        if (_elevatorDisplayUI != null) _elevatorDisplayUI.Hide();
        _isMoving = true;

        if (_animator != null)
        {
            _animator.SetBool(IS_OPEN_ANIMATION_PARAM, false);
        }
    }

    public void StartJourney()
    {
        StartCoroutine(ProcessTrip());
    }

    private void OnElevatorReturn()
    {
        // Trigger Open elevator animation
        if (_elevatorDisplayUI != null) _elevatorDisplayUI.Show();
        _isMoving = false;
        _currentDirection = Direction.Up;
    }

    private void OnReachTop()
    {
        if (_floorNumberIndicator != null)
        {
            _floorNumberIndicator.SwitchDirection(false);
        }

        _currentDirection = Direction.Down;
        StartCoroutine(ProcessTrip());
    }

    private void PlayMovementSound()
    {
        if (_elevatorData.SfxCollection == null) return;
        PlaySound(_elevatorData.SfxCollection.MovementSfx);
    }

    private IEnumerator ProcessTrip()
    {
        if(_floorNumberIndicator != null)
        {
            _floorNumberIndicator.StartMovement();
        }

        PlayMovementSound();

        if (_currentDirection == Direction.Up)
        {
            while (_currentFloor != _totalFloors)
            {
                if (_destinationMap.ContainsKey(_currentFloor))
                {
                    OnReachedFloor(_currentFloor);
                    yield return new WaitForSeconds(1f);

                    if (_destinationMap.Keys.Count == 0)
                    {
                        // Means that all of the destination floors have been visited
                        break;
                    }

                    // Should stop movement start if we've reached the last floor
                    PlayMovementSound();
                    if (_floorNumberIndicator != null) _floorNumberIndicator.StartMovement();
                    Debug.Log("Moving On...");
                }

                yield return new WaitForSeconds(_elevatorData.SpeedInSeconds);
                _currentFloor++;
                if (_floorNumberIndicator != null) _floorNumberIndicator.SetFloorNumber(_currentFloor);
            }

            OnReachTop();
        }
        else if(_currentDirection == Direction.Down)
        {
            while(_currentFloor != 0)
            {
                yield return new WaitForSeconds(_elevatorData.SpeedInSeconds);
                _currentFloor--;
                if (_floorNumberIndicator != null) _floorNumberIndicator.SetFloorNumber(_currentFloor);
            }

            // Once we've reached the bottom floor, trigger some function
            if (_floorNumberIndicator != null)
            {
                _floorNumberIndicator.SwitchDirection(true);
            }

            OpenElevator();
        }
    }

    public void ForceStop()
    {
        StopAllCoroutines();
        if(_audioSource != null) _audioSource.Stop();
    }

    public DropResultDTO OnDropObject(DraggableObject draggableObject)
    {   
        if (_isMoving) return new DropResultDTO();
        // Need to get component from draggableObject
        if (draggableObject == null) return new DropResultDTO();

        if (draggableObject.gameObject.TryGetComponent(out Employee employee))
        {
            DropResultDTO dropOnElevatorResult = AddToElevator(employee);

            if (dropOnElevatorResult.Success)
            {
                // Hide the employee once they are added to the elevator
                draggableObject.gameObject.SetActive(false);
            }

            return dropOnElevatorResult;
        }

        return new DropResultDTO();
    }

    public void SetTotalFloors(int totalFloors)
    {
        _totalFloors = totalFloors;
    }
}
