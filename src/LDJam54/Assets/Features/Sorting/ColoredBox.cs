using System;
using UnityEngine;

public class ColoredBox : MonoBehaviour
{
    public SortingColor Color;
    
    private SortingColor _colorLocation = SortingColor.None;
    private bool IsMisplaced => Color != _colorLocation;

    public void SetColorLocation(SortingColor color)
    {
        if (IsMisplaced)
        {
            _colorLocation = color;
            if (!IsMisplaced)
                CurrentGameState.DecrementKPIStatic(KPI.BoxUnsorted);
        }
        else
        {
            _colorLocation = color;
            if (IsMisplaced)
                CurrentGameState.IncrementKPIStatic(KPI.BoxUnsorted);
        }
    }

    private void OnEnable() => CurrentGameState.IncrementKPIStatic(KPI.BoxUnsorted);

    private void OnDisable()
    {
        if (IsMisplaced)
            CurrentGameState.DecrementKPIStatic(KPI.BoxUnsorted);
    }
}