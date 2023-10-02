using UnityEngine;

public class ThrowControlUI: OnMessage<SetCanThrow>
{
    [SerializeField] private GameObject[] visuals;

    protected override void Execute(SetCanThrow msg)
    {
        foreach (var visual in visuals)
            visual.SetActive(msg.CanThrow);
    }
}