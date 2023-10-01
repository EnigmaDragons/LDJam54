using UnityEngine;

public class ColoredContainer : MonoBehaviour
{
    public SortingColor color;
    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private SerializableDictionary<SortingColor, Material> colorToMaterial;

    private void Awake()
    {
        renderer.materials = new[] {colorToMaterial[color]};
    }
    
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