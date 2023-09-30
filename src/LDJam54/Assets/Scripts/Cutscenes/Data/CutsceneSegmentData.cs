using System;
using UnityEngine;

[Serializable]
public class CutsceneSegmentData
{
    public CutsceneSegmentType Type;
    public CutsceneCharacter Character;
    [TextArea(0, 4)] public string Text;
}
