using TMPro;
using UnityEngine;

public class TintChangeText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Color altColor;

    private Color _initial;
    
    private void Awake()
    {
        _initial = text.color;
    }

    public void Tint() => text.color = altColor;
    public void Revert() => text.color = _initial;
}
