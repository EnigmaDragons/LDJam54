using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class FadeInOnEnable : MonoBehaviour
{
   [SerializeField] public float fadeInDuration; 
   
   private CanvasGroup _canvasGroup;
   private float _elapsedTime = 0f;

   private void OnEnable()
   {
      _canvasGroup = gameObject.GetComponent<CanvasGroup>();
      _elapsedTime = 0f;
   }

   private void FixedUpdate()
   {
      _elapsedTime += Time.unscaledDeltaTime;
      if (_canvasGroup.alpha < 1)
      {
         _canvasGroup.alpha = Mathf.Lerp(0, 1, _elapsedTime / fadeInDuration);
      }
      else
      {
         _canvasGroup.alpha = 1;
         enabled = false;
      }
   }
}
