using System;
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
    public SortingColor ColorLocation => _colorLocation;
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
    
    private bool IsMisplaced => Color != ColorLocation;

    public void SetColorLocation(SortingColor color)
    {
        if (IsMisplaced)
        {
            _colorLocation = color;
            if (!IsMisplaced)
                CurrentGameState.DecrementKPIStatic(KPI.BoxesUnsorted);
        }
        else
        {
            _colorLocation = color;
            if (IsMisplaced)
                CurrentGameState.IncrementKPIStatic(KPI.BoxesUnsorted);
        }
    }

    private void OnEnable()
    {
        Message.Publish(new ColoredBoxAppeared(this));
        if (IsMisplaced)
            CurrentGameState.IncrementKPIStatic(KPI.BoxesUnsorted);
    } 

    private void OnDisable()
    {
        Message.Publish(new ColoredBoxDisappeared(this));
        if (IsMisplaced)
            CurrentGameState.DecrementKPIStatic(KPI.BoxesUnsorted);
    }
}