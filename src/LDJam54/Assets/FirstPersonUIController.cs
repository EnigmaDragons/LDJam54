using TMPro;

public class FirstPersonUIController : OnMessage<GameStateChanged>
{
    public TextMeshProUGUI PlayerNameLabel;

    public void Start()
    {
        PlayerNameLabel.text = "Employee: " + CurrentGameState.State.PlayerID;
    }

    protected override void Execute(GameStateChanged msg)
    {
        PlayerNameLabel.text = "Employee: " + msg.State.PlayerID;
    }
}
