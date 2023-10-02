using UnityEngine;

public class OnPlayerEnterEnableTarget : PlayerTrigger
{
    public GameObject target;
    
    protected override void OnTriggered()
    {
        target.SetActive(true);
    }
}
