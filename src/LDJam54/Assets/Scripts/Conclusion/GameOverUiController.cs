using TextFx;
using UnityEngine;

public class GameOverUiController : OnMessage<WasFired, DayChanged>
{
    public GameObject target;
    public TextFxTextMeshProUGUI text;

    protected override void Execute(DayChanged msg)
    {
        target.SetActive(false);
    }

    protected override void AfterEnable()
    {
        target.SetActive(false);
        text.AnimationManager.PlayAnimation();
    }
    
    protected override void Execute(WasFired msg)
    {
        target.SetActive(true);
        FirstPersonInteractionStatus.IsEnabled = false;
    }
}
