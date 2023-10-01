using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameConfig : ScriptableObject
{
    [Header("Coworkers")]
    public List<Worker> Coworkers = new List<Worker>();
    public CoworkerAverage BoxShippedAverage;
    public CoworkerAverage PlacedCorrectlyAverage;
    public CoworkerAverage BoxUnsortedAverage;
    
    [Header("Timing/Franticness")]
    public float ClockSpeedFactor = 1f;
    public float BoxSpawnInterval = 1.5f;

    [Header("Player Movement")] 
    public float PlayerWalkSpeed = 5f;
    public float PlayerRunSpeed = 10f;
    public float PlayerJumpForce = 2f;
}
