using TMPro;

public class CurrentDayDisplay : OnMessage<GameStateChanged>
{
    public TextMeshProUGUI label;

    private void Start() => UpdateDisplay(CurrentGameState.State);
    protected override void Execute(GameStateChanged msg) => UpdateDisplay(msg.State);
    
    private void UpdateDisplay(GameState state) => label.text = $"Day {state.CurrentDayNumber}";
}
