using System;
using FMODUnity;

[Serializable]
public class DayConfig
{
    public KPI[] ActiveKPIs;
    public int[] CoworkerScores;
    public float BoxSpawnInterval;
    public float ClockSpeedFactor;
    public EventReference MorningSpeech;
    public EventReference KpiSpeech;
    public EventReference EventSpeech;
}
