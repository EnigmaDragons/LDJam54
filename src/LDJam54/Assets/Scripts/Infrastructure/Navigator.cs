using UnityEngine;

[CreateAssetMenu]
public sealed class Navigator : ScriptableObject
{
    public static void NavigateToMainMenu() => NavigateTo("MainMenu");
    public static void NavigateToGameScene() => NavigateTo("GameScene");
    public static void NavigateToCreditsScene() => NavigateTo("CreditsScene");
    public static void NavigateToScene(string sceneName) => NavigateTo(sceneName);

    private static void NavigateTo(string sceneName)
    {
        Log.Info($"Navigating to {sceneName}");
        Message.Publish(new NavigateToSceneRequested(sceneName));
    }
    
    public static void HardExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
#else
        Application.Quit();
#endif 
    }
}
