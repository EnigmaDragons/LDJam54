using System.Linq;
using UnityEngine;

public class LargeShelfRandimizer : MonoBehaviour
{
    [SerializeField] private ColoredContainer coloredContainer;
    [SerializeField] private PalletRandomizer palletPrefab;
    [SerializeField] private DropTarget[] palletDropTargets;
    [SerializeField] private float chanceOfPallet;
    [SerializeField] private float delay;
    
    private float _t;
    
    private void Awake()
    {
        coloredContainer.IgnoreCollision = true;
        _t = delay;
        var yExtent = palletPrefab.MeshFilter.sharedMesh.bounds.extents.y;
        
        foreach (var target in palletDropTargets.Where(x => Rng.Chance(chanceOfPallet)))
        {
            var pallet = Instantiate(palletPrefab, target.GetLocation(yExtent), transform.rotation);
            pallet.Color = coloredContainer.color;
            pallet.ColorLocation = coloredContainer.color;
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