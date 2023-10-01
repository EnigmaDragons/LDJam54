using UnityEngine;

public class ColoredContainer : MonoBehaviour
{
    public SortingColor color;
    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private SerializableDictionary<SortingColor, Material> colorToMaterial;
    public bool IgnoreCollision;
    
    private void Awake()
    {
        renderer.materials = new[] {colorToMaterial[color]};
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IgnoreCollision)
            return;
        var component = other.gameObject.GetComponent<ColoredBox>();
        if (component == null)
            return;
        component.SetColorLocation(color);
        if (component.Color == color)
            CurrentGameState.IncrementKPIStatic(KPI.PlacedCorrectly);
    }

    private void OnTriggerExit(Collider other)
    {
        if (IgnoreCollision)
            return;
        var component = other.gameObject.GetComponent<ColoredBox>();
        if (component == null)
            return;
        component.SetColorLocation(SortingColor.None);
    }
}