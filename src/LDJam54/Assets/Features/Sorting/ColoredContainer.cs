using UnityEngine;

public class ColoredContainer : MonoBehaviour
{
    [SerializeField] private SortingColor color;
    
    private void OnTriggerEnter(Collider other)
    {
        var component = other.gameObject.GetComponent<ColoredBox>();
        if (component == null)
            return;
        component.SetColorLocation(color);
        if (component.Color == color)
            CurrentGameState.IncrementKPIStatic(KPI.PlacedCorrectly);
    }

    private void OnTriggerExit(Collider other)
    {
        var component = other.gameObject.GetComponent<ColoredBox>();
        if (component == null)
            return;
        component.SetColorLocation(SortingColor.None);
    }
}