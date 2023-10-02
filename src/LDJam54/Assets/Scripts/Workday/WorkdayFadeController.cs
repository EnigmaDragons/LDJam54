using UnityEngine;

public class WorkdayFadeController : OnMessage<StartNextDayRequested>
{
    public float fadeTime = 2.4f;
    public float asleepTime = 1.5f;
    public CanvasGroup group;

    private Mode _mode;
    private float _remainingFadeTime;
    
    protected override void Execute(StartNextDayRequested msg)
    {
        if (CurrentGameState.State.PlayerIsFired)
            return;

        _remainingFadeTime = fadeTime;
        _mode = Mode.FadeOut;
        FirstPersonInteractionStatus.IsEnabled = false;
    }

    private void Update()
    {
        if (_mode == Mode.Idle)
            return;
        
        _remainingFadeTime -= Time.deltaTime;
        if (_remainingFadeTime <= 0)
        {
            group.alpha = _mode == Mode.FadeOut ? 1 : 0;
            if (_mode == Mode.FadeIn)
            {
                _mode = Mode.Idle;
                FirstPersonInteractionStatus.IsEnabled = true;
            }
            else if (_mode == Mode.FadeOut)
            {
                _mode = Mode.Teleport;
                _remainingFadeTime = asleepTime;
                TeleportPlayerToHome();
                Message.Publish(new DayChanged());
            }
            else if (_mode == Mode.Teleport)
            {
                _remainingFadeTime = fadeTime;
                _mode = Mode.FadeIn;
            }

            return;
        }
        
        if (_mode != Mode.Teleport)
            group.alpha = Mathf.Lerp(_mode == Mode.FadeOut ? 1 : 0,  _mode == Mode.FadeOut ? 0 : 1, _remainingFadeTime / fadeTime);
    }

    public static void TeleportPlayerToHome()
    {
        var player = GameObject.FindWithTag("Player");
        var spawnPoint = GameObject.FindWithTag("Respawn");
        player.transform.position = spawnPoint.transform.position;
        player.transform.rotation = spawnPoint.transform.rotation;
        FirstPersonInteractionStatus.IsEnabled = true;
    }
    
    enum Mode
    {
        Idle = 0,
        FadeOut = 1,
        Teleport = 2,
        FadeIn = 3,
    }
}
