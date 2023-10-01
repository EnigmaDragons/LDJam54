using System;
using UnityEngine;

public class ColoredBox : MonoBehaviour
{
    public MeshFilter MeshFilter;
    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private SerializableDictionary<SortingColor, Material> colorToMaterial;

    public SortingColor Color;
    public SortingColor ColorLocation;

    private void Awake()
    {
        renderer.materials = new[] { colorToMaterial[Color] };
    }
    
    private bool IsMisplaced => Color != ColorLocation;

    public void SetColorLocation(SortingColor color)
    {
        if (IsMisplaced)
        {
            ColorLocation = color;
            if (!IsMisplaced)
                CurrentGameState.DecrementKPIStatic(KPI.BoxUnsorted);
        }
        else
        {
            ColorLocation = color;
            if (IsMisplaced)
                CurrentGameState.IncrementKPIStatic(KPI.BoxUnsorted);
        }
    }

    private void OnEnable()
    {
        if (IsMisplaced)
            CurrentGameState.IncrementKPIStatic(KPI.BoxUnsorted);
    } 

    private void OnDisable()
    {
        if (IsMisplaced)
            CurrentGameState.DecrementKPIStatic(KPI.BoxUnsorted);
    }
}