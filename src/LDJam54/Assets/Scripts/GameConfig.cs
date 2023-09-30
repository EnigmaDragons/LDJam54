using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameConfig : ScriptableObject
{
    public List<Worker> Coworkers = new List<Worker>();
    public List<CoworkerAverage> CoworkerAverages = new List<CoworkerAverage>();
    public float ClockSpeedFactor = 1f;
}
