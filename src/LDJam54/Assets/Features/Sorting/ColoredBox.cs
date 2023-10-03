using UnityEngine;

public class ColoredBox : MonoBehaviour
{
    public MeshFilter MeshFilter;
    [SerializeField] private DropSize size;
    [SerializeField] private SortingColor _initialColor;
    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private SerializableDictionary<SortingColor, Material> colorToMaterial;

    private SortingColor _color;
    private SortingColor _colorLocation;

    public SortingColor Color => _color;
    public DropSize Size => size;

    public void SetColor(SortingColor color) => SetColor(color, color);
    public void SetColor(SortingColor color, SortingColor colorLocation)
    {
        _color = color;
        _colorLocation = colorLocation;
        renderer.materials = new[] { colorToMaterial[color] };
    }
    
    private void Awake()
    {
        renderer.materials = new[] { colorToMaterial[Color] };
        SetColor(_initialColor);
    }
    
    private bool GetIsOutOfPlace()
    {
        var isOutOfPlace = _colorLocation != _color;
        return isOutOfPlace;
    }

    public void SetColorLocation(SortingColor color)
    {
        if (GetIsOutOfPlace())
        {
            _colorLocation = color;
            if (!GetIsOutOfPlace())
                CurrentGameState.IncrementKPIStatic(KPI.BoxesTidied);
        }
        else
        {
            _colorLocation = color;
            if (GetIsOutOfPlace())
                CurrentGameState.DecrementKPIStatic(KPI.BoxesTidied);
        }
    }

    private void OnEnable()
    {
        Message.Publish(new ColoredBoxAppeared(this));
    } 

    private void OnDisable()
    {
        Message.Publish(new ColoredBoxDisappeared(this));
        if (GetIsOutOfPlace())
            CurrentGameState.IncrementKPIStatic(KPI.BoxesTidied);
    }
}