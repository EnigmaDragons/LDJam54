using TMPro;
using UnityEngine;

public class InteractControlUI : OnMessage<UpdateInteractControl>
{
    [SerializeField] private GameObject[] visuals;
    [SerializeField] private TextMeshProUGUI text;

    protected override void Execute(UpdateInteractControl msg)
    {
        text.text = msg.Text;
        foreach (var visual in visuals)
            visual.SetActive(!string.IsNullOrWhiteSpace(msg.Text));
    }
}