using System.Linq;
using UnityEngine;

public class DropTarget : MonoBehaviour
{
    [SerializeField] private DropTarget[] linkedDropTargets;
    public DropTarget TopmostDropTarget => linkedDropTargets.Concat(new [] {this}).OrderByDescending(x => x.transform.position.y).First();
}