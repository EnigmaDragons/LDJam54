using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PalletRandomizer : MonoBehaviour
{
    public MeshFilter MeshFilter;
    [SerializeField] private DropTarget[] targets;
    [SerializeField] private ColoredBox smallBox;
    [SerializeField] private ColoredBox mediumBox;
    [SerializeField] private ColoredBox largeBox;

    public SortingColor Color;
    public SortingColor ColorLocation;
    
    private void Awake()
    {
        var size = new List<DropSize> {DropSize.Small, DropSize.Medium, DropSize.Large }.DrawRandom();
        var sizedTargets = targets.Where(x => x.Size == size).ToArray();
        ColoredBox boxPrefab = null;
        int stack = 1;
        if (size == DropSize.Small)
        {
            boxPrefab = smallBox;
            stack = 3;
        }
        else if (size == DropSize.Medium)
        {
            boxPrefab = mediumBox;
            stack = 3; 
        }
        else if (size == DropSize.Large)
        {
            boxPrefab = largeBox;
            stack = 1; 
        }

        var yExtent = boxPrefab.MeshFilter.sharedMesh.bounds.extents.y;
        var rotation = transform.rotation;
        foreach (var target in sizedTargets)
        {
            var box = Instantiate(boxPrefab, target.GetLocation(yExtent), rotation);
            box.Color = Color;
            box.ColorLocation = ColorLocation;
            for (var i = 1; i < stack; i++)
            {
                box = Instantiate(boxPrefab, target.GetLocation(yExtent) + new Vector3(0, yExtent * i * 2 + 0.01f * i, 0), rotation);
                box.Color = Color;
                box.ColorLocation = ColorLocation;
            }
        }
    }
}