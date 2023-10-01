using System.Linq;
using UnityEngine;

public class DropTarget : MonoBehaviour
{
    [SerializeField] private DropTarget[] linkedDropTargets;
    
    public DropTarget[] OverlappingDropTargets;
    public DropSize Size;
    
    public DropTarget TopmostDropTarget => linkedDropTargets.Concat(new [] {this}).OrderByDescending(x => x.transform.position.y).First();
    
    public Vector3 GetLocation(float yExtent)
     => new Vector3(transform.position.x, transform.position.y + yExtent + 0.01f, transform.position.z);
}