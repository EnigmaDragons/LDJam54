using UnityEngine;

public class OnPlayerEnterDisableTarget : PlayerTrigger
{
    public GameObject target;
    
    protected override void OnTriggered()
    {
        target.SetActive(false);
    }
}
