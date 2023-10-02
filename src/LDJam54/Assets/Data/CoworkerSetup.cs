using UnityEngine;

public class CoworkerSetup : MonoBehaviour
{
    [SerializeField] private GameConfig gameConfig;
    
    public void Start()
    {
        CurrentGameState.State.Coworkers = gameConfig.Coworkers;
    }
}
