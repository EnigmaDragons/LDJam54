using UnityEngine;

public class WorkdayFadeController : OnMessage<StartNextDayRequested>
{
    public float fadeTime = 2.4f;
    public CanvasGroup group;

    private Mode _mode;
    private float _remainingFadeTime;
    
    protected override void Execute(StartNextDayRequested msg)
    {
        if (CurrentGameState.State.PlayerIsFired)
            return;

        _remainingFadeTime = fadeTime;
        _mode = Mode.FadeOut;
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
            }
            if (_mode == Mode.FadeOut)
            {
                _mode = Mode.Teleport;
                TeleportPlayer();
                Message.Publish(new DayChanged());
                _remainingFadeTime = fadeTime;
                _mode = Mode.FadeIn;
            }

            return;
        }
        
        group.alpha = Mathf.Lerp(_mode == Mode.FadeOut ? 1 : 0,  _mode == Mode.FadeOut ? 0 : 1, _remainingFadeTime / fadeTime);
    }

    private void TeleportPlayer()
    {
        var player = GameObject.FindWithTag("Player");
        var spawnPoint = GameObject.FindWithTag("Respawn");
        player.transform.position = spawnPoint.transform.position;
    }
    
    enum Mode
    {
        Idle = 0,
        FadeOut = 1,
        Teleport = 2,
        FadeIn = 3,
    }
}
