using UnityEngine;

[CreateAssetMenu(menuName = "Cutscene Script")]
public class CutsceneScript : ScriptableObject
{
    public string Name => name;
    public CutsceneSegmentData[] Segments;
}
