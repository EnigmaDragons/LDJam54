using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameConfig : ScriptableObject
{
    [Header("Coworkers")]
    public List<Worker> Coworkers = new List<Worker>();
    public CoworkerAverage BoxShippedAverage;
    public CoworkerAverage PlacedCorrectlyAverage;
    
    [Header("Timing/Franticness")]
    public float ClockSpeedFactor = 1f;

    [Header("Player Movement")] 
    public float PlayerWalkSpeed = 5f;
    public float PlayerRunSpeed = 10f;
    public float PlayerJumpForce = 2f;

}
