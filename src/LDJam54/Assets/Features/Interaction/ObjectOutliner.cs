using UnityEngine;

namespace Features.Interaction
{
    public class ObjectOutliner : MonoBehaviour
    {
        [SerializeField] private Material outlineMaterial;
        [SerializeField] private Material defaultMaterial;
        [SerializeField] private Renderer[] renderers;

        private void Awake()
        {
            renderers = GetComponentsInChildren<Renderer>();
        }

        public void SetOutline(bool isOutline)
        {
            foreach (var renderer in renderers)
            {
                renderer.material = isOutline ? outlineMaterial : defaultMaterial;
            }
        }
    }
}