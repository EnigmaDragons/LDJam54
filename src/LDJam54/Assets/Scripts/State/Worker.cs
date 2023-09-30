using System;
using System.Collections.Generic;

[Serializable]
public class Worker
{
    public string Name = "";
    public int Skill = 0;
    public List<KPI> MasteredKpis = new();
    public List<KPI> PotentialKpis = new();
}
