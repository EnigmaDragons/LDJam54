using UnityEngine;

public class AreasController : OnMessage<GameStateChanged, DayChanged>
{
    public GameObject day1Prefab;
    public GameObject day2Prefab;
    public GameObject day3Prefab;
    public GameObject day4Prefab;
    public GameObject day5Prefab;
    public GameObject day6Prefab;
    public GameObject day7Prefab;
    public Door[] doors;
    public Door[] doorsToStartLocked;

    private GameObject _currentDayPrefab;
    
    private void Awake()
    {
        for (var i = 0; i < doors.Length; i++)
            doors[i].SetLabels("Meeting Room", "Room " + (i + 1));
        doorsToStartLocked.ForEach(d => d.SetLocked(true));
        Refresh(CurrentGameState.State);
    }

    private void Start() => Refresh(CurrentGameState.State);
    
    protected override void Execute(GameStateChanged msg) => Refresh(msg.State);
    protected override void Execute(DayChanged msg)
    {
        RefreshRoom(CurrentGameState.State);
    }

    private void Refresh(GameState gs)
    {
        for (var i = 0; i < doors.Length; i++) 
            doors[i].SetLocked(gs.CurrentDayNumber != i + 1);
    }

    private void RefreshRoom(GameState gs)
    {
        if (_currentDayPrefab != null)
        {
            Destroy(_currentDayPrefab);
            _currentDayPrefab = null;
        }
        if (CurrentGameState.State.CurrentDayNumber == 2 && day2Prefab != null)
            _currentDayPrefab = Instantiate(day2Prefab, transform);
        else if (CurrentGameState.State.CurrentDayNumber == 3 && day3Prefab != null)
            _currentDayPrefab = Instantiate(day3Prefab, transform);
        else if (CurrentGameState.State.CurrentDayNumber == 4 && day4Prefab != null)
            _currentDayPrefab = Instantiate(day4Prefab, transform);
        else if (CurrentGameState.State.CurrentDayNumber == 5 && day5Prefab != null)
            _currentDayPrefab = Instantiate(day5Prefab, transform);
        else if (CurrentGameState.State.CurrentDayNumber == 6 && day6Prefab != null)
            _currentDayPrefab = Instantiate(day6Prefab, transform);
        else if (CurrentGameState.State.CurrentDayNumber == 7 && day7Prefab != null)
            _currentDayPrefab = Instantiate(day7Prefab, transform);
        else
            _currentDayPrefab = Instantiate(day1Prefab, transform);
    }
}
