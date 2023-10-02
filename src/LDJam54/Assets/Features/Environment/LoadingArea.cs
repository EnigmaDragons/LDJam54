using System;
using System.Collections.Generic;
using UnityEngine;

public class LoadingArea : MonoBehaviour
{
    private HashSet<ColoredBox> _coloredBoxes = new HashSet<ColoredBox>();

    private void OnTriggerExit(Collider other)
    {
        var component = other.GetComponent<ColoredBox>();
        if (component == null || _coloredBoxes.Contains(component))
            return;
        _coloredBoxes.Add(component);
        CurrentGameState.IncrementKPIStatic(KPI.BoxesUnloaded);
    }

    private void OnTriggerEnter(Collider other)
    {
        var component = other.GetComponent<ColoredBox>();
        if (component == null || !_coloredBoxes.Contains(component))
            return;
        _coloredBoxes.Remove(component);
        CurrentGameState.DecrementKPIStatic(KPI.BoxesUnloaded);
    }
}