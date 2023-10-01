using UnityEngine;

public class MeetingTimeController : OnMessage<GameStateChanged>
{
    public GameObject eveningElement;
    public GameObject morningElement;

    private void Start() => Render(CurrentGameState.State.MeetingTime);
    
    protected override void Execute(GameStateChanged msg) => Render(msg.State.MeetingTime);

    private void Render(MeetingTime mt)
    {
        eveningElement.SetActive(mt == MeetingTime.Evening);
        morningElement.SetActive(mt == MeetingTime.Morning);
    }
}
