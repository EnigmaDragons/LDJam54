using UnityEngine;

public class HudInstructionDisplay : OnMessage<StartWorkdayRequested, WorkdayEnded>
{
    public Transform spawnTarget;
    public GameObject startWorkdayPrefab;
    public GameObject endWorkdayPrefab;
    public float displayTime = 3f;
    
    protected override void Execute(StartWorkdayRequested msg)
    {
        var g = Instantiate(startWorkdayPrefab, spawnTarget);
        this.ExecuteAfterDelay(displayTime, () => Destroy(g));
    }

    protected override void Execute(WorkdayEnded msg)
    {
        var g = Instantiate(endWorkdayPrefab, spawnTarget);
        this.ExecuteAfterDelay(displayTime, () => Destroy(g));
    }
}
