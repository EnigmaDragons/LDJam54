using UnityEngine;

public class ColoredContainer : MonoBehaviour
{
    [SerializeField] private SortingColor color;
    
    private void OnCollisionEnter(Collision other)
    {
        var component = other.gameObject.GetComponent<ColoredBox>();
        if (component == null)
            return;
        component.SetColorLocation(color);
        if (component.Color == color)
            CurrentGameState.State.KPIs[KPI.PlacedCorrectly]++;
    }

    private void OnCollisionExit(Collision other)
    {
        var component = other.gameObject.GetComponent<ColoredBox>();
        if (component == null)
            return;
        component.SetColorLocation(SortingColor.None);
    }
}