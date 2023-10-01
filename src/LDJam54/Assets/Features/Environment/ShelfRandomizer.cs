using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShelfRandomizer : MonoBehaviour
{
    [SerializeField] private ColoredContainer coloredContainer;
    [SerializeField] private List<DropTarget> targets;
    [SerializeField] private float chanceOfBox;
    [SerializeField] private float chanceOfStack;
    [SerializeField] private ColoredBox smallBox;
    [SerializeField] private ColoredBox medBox;
    [SerializeField] private ColoredBox largeBox;
    [SerializeField] private float delay;

    private float _t;
    
    private void Awake()
    {
        coloredContainer.IgnoreCollision = true;
        _t = delay;
        
        var randomTargets = targets.Shuffled();
        while (randomTargets.Count > 0)
        {
            var target = randomTargets[0];
            if (Rng.Chance(chanceOfBox))
            {
                randomTargets.RemoveAll(x => target.OverlappingDropTargets.Contains(x));
                ColoredBox box = null;
                if (target.Size == DropSize.Small)
                    box = smallBox;
                else if (target.Size == DropSize.Medium)
                    box = medBox;
                else if (target.Size == DropSize.Large)
                    box = largeBox;
                var yExtents = box.MeshFilter.sharedMesh.bounds.extents.y;
                var instBox = Instantiate(box, target.GetLocation(yExtents), transform.rotation);
                instBox.SetColor(coloredContainer.color);
                if (Rng.Chance(chanceOfStack) && target.Size != DropSize.Large)
                {
                    var stackedBox = Instantiate(box, target.GetLocation(yExtents) + new Vector3(0, yExtents * 2f + 0.01f, 0), transform.rotation);
                    stackedBox.SetColor(coloredContainer.color);
                }
            }
            randomTargets.RemoveAt(0);
        }
    }

    private void Update()
    {
        if (_t <= 0)
            return;
        _t -= Time.deltaTime;
        if (_t <= 0)
            coloredContainer.IgnoreCollision = false;
    }
}