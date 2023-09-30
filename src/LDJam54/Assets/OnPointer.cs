
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnPointer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] UnityEvent playHooverSound;
    [SerializeField] UnityEvent stopHooverSound;


    public void OnPointerEnter(PointerEventData eventData)
    {
       playHooverSound.Invoke();   
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        stopHooverSound.Invoke();
    }
}