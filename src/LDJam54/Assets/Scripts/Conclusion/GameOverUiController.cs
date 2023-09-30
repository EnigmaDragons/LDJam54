using TextFx;
using UnityEngine;

public class GameOverUiController : OnMessage<WasFired>
{
    public GameObject target;
    public TextFxTextMeshProUGUI text;
    
    protected override void AfterEnable()
    {
        target.SetActive(false);
        text.AnimationManager.PlayAnimation();
    }
    
    protected override void Execute(WasFired msg)
    {
        target.SetActive(true);
    }
}
