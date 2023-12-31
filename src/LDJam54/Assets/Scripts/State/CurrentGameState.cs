using System;
using System.Linq;
using UnityEngine;

public sealed class CurrentGameState
{
    private static CurrentGameState instance;
    public static CurrentGameState Instance
    {
        get {
            if (instance == null)
                InitInstance();
            return instance;
        }
    }

    public static GameState State => Instance._gameState;
    public static void InitInstance(GameState initialState = null)
    {
        var cgs = new CurrentGameState();
        if (initialState != null)
            cgs.Init(initialState);
        else
            cgs.Init();
        instance = cgs;
    }
    
    public static void SetStoryState(string storyState, bool isTrue) 
        => Instance.UpdateState(g => g.CutsceneStoryStates[storyState] = isTrue);
    
    public static bool StoryState(string storyState) 
        => Instance._gameState.CutsceneStoryStates.TryGetValue(storyState, out var storyStateValue) && storyStateValue;
    
    public static void AdvanceToNextDay() => Instance.UpdateState(g => g.CurrentDayNumber++);

    public static void Update(Action<GameState> apply) => Instance.UpdateState(apply);
    public static void Update(Func<GameState, GameState> apply) => Instance.UpdateState(apply);
    public static void FireCoworker(string id) => Instance.UpdateState(g => g.Coworkers.RemoveAll(c => c == id));

    [SerializeField] private GameState _gameState;

    public void Init() => _gameState = new GameState();
    public void Init(GameState initialState) => _gameState = initialState;
    public void Subscribe(Action<GameStateChanged> onChange, object owner) => Message.Subscribe(onChange, owner);
    public void Unsubscribe(object owner) => Message.Unsubscribe(owner);
    
    public void UpdateState(Action<GameState> apply)
    {
        UpdateState(_ =>
        {
            apply(_gameState);
            return _gameState;
        });
    }
    
    public void UpdateState(Func<GameState, GameState> apply)
    {
        _gameState = apply(_gameState);
        Message.Publish(new GameStateChanged(_gameState));
    }
    
    public void IncrementKPI(KPI kpi, int amount = 1)
    {
        if (!WorkdayState.IsWorkdayStarted || WorkdayState.IsWorkdayEnded)
            return;
        
        UpdateState(g =>
        {
            g.KPIs[kpi] += amount;
            return g;
        });
    }
    
    public void DecrementKPI(KPI kpi, int amount = 1)
    {
        if (!WorkdayState.IsWorkdayStarted || WorkdayState.IsWorkdayEnded)
            return;
        
        UpdateState(g =>
        {
            g.KPIs[kpi] -= amount;
            return g;
        });
    }
    
    public static void IncrementKPIStatic(KPI kpi, int amount = 1)
    {
        Instance.IncrementKPI(kpi, amount);
    }
    
    public static void DecrementKPIStatic(KPI kpi, int amount = 1)
    {
        Instance.DecrementKPI(kpi, amount);
    }

    public static float GetPace(GameConfig cfg)
    {
        var kpiScoring = cfg.KpiScoring;
        var fractionOfDayCompleted = ((WorkdayState.CurrentHour - WorkdayConfig.START_OF_DAY) * 60 + WorkdayState.CurrentMinute) / ((WorkdayConfig.END_OF_DAY - WorkdayConfig.START_OF_DAY) * 60f);
        var lowestScoreForDay = State.CoworkerScores.Min();
        var playerScores = State.ActiveKPIs.Select(kpi => State.KPIs[kpi] * kpiScoring[kpi]).Min();
        return 1f * playerScores / lowestScoreForDay / fractionOfDayCompleted;
    }
}
