using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaySetup : MonoBehaviour
{
    [SerializeField] private GameConfig gameConfig;
    public void Start()
    {
        CurrentGameState.Instance.UpdateState(s => s.CurrentDayNumber = gameConfig.StartingDay);
        Message.Publish(new DayChanged());
    }
}
