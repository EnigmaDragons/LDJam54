using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameConfig : ScriptableObject
{
    [Header("Coworkers")]
    public List<string> Coworkers = new List<string>();

    [Header("KPI Scoring")]
    public int BoxShippingScore;
    public int PlacedCorrectlyScore;
    public int BoxUnsortedFixedScore;

    [Header("Player Movement")] 
    public float PlayerWalkSpeed = 5f;
    public float PlayerRunSpeed = 10f;
    public float PlayerJumpForce = 2f;

    [Header("Days")]
    public int StartingDay = 1;
    public int TotalPlayableDays = 1;
    public DayConfig[] Days;
}
