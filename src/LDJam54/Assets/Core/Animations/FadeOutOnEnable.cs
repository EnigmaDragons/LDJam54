using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class FadeOutOnEnable : MonoBehaviour
{
    [SerializeField] public float fadeDuration; 
   
    private CanvasGroup _canvasGroup;
    private float _elapsedTime = 0f;

    private void OnEnable()
    {
        _canvasGroup = gameObject.GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 1;
        _elapsedTime = 0;
    }

    private void Update()
    {
        if (fadeDuration == 0)
            return;
        
        _elapsedTime += Time.unscaledDeltaTime;
        _canvasGroup.alpha = Mathf.Clamp(Mathf.Lerp(1, 0, _elapsedTime / fadeDuration), 0, 1);
        
    }
}
 