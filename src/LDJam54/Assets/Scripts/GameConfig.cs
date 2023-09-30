using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameConfig : ScriptableObject
{
    public List<Worker> Coworkers = new List<Worker>();
    public CoworkerAverage BoxShippedAverage;
    public CoworkerAverage PlacedCorrectlyAverage;
    public float ClockSpeedFactor = 1f;
}
