using System;
using DG.Tweening;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public Rigidbody Body;
    public Collider Collider;
    //Hold Details
    public bool CanBeHeld;
    public float DistanceInFront;
    public float HeightOffset;
    //Set On Details
    public bool CanBeSetOn;
    public bool SnapToCenter;

    [field: SerializeField] public float BobbingIntensity { get; private set; } = 0.05f;
    
    [SerializeField]
    private int defaultLayer = 6;
    [SerializeField]
    private int highlightLayer = 7;
    
    private bool _isSettingDown;
    private bool _isHeld;
    private float _setDownSpeed;
    private Vector3 _setDownDestination;
    private float _setDownRotationSpeed;
    private Quaternion _setDownRotation;
    
    private float yOffset;
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void FixedUpdate()
    {
        if (!_isSettingDown)
            return;
        Body.MoveRotation(Quaternion.RotateTowards(transform.rotation, _setDownRotation, _setDownRotationSpeed * Time.fixedDeltaTime));
        Body.MovePosition(Vector3.MoveTowards(transform.position, _setDownDestination, _setDownSpeed * Time.fixedDeltaTime));
        if (Vector3.Distance(Body.position, _setDownDestination) < 0.01f)
        {
            _isSettingDown = false;
            Body.useGravity = true;
        }
    }


    public void Hold()
    {
        _isSettingDown = false;
        _isHeld = true;
        Body.useGravity = false;
        Collider.enabled = false;
    }

    public void SetDown(Vector3 destination, Quaternion rotation, float speed, float rotationSpeed)
    {
        Collider.enabled = true;
        _isSettingDown = true;
        _isHeld = false;
        _setDownDestination = destination;
        _setDownRotation = rotation;
        _setDownSpeed = speed;
        _setDownRotationSpeed = rotationSpeed;
    }

    public void Highlight()
    {
        gameObject.layer = highlightLayer;
    }

    public void Unhighlight()
    {
        gameObject.layer = defaultLayer;
    }

    public void PlayShippedAnimation()
    {
        //1 disable collider & rigidbody
        Collider.enabled = false;
        Body.isKinematic = true;
        
        //set the material to transparent
        SetToTransparent();
        
        //move box up
        transform.DOMoveY(transform.position.y + 0.5f, 1f);
        //fade out
        _renderer.material.DOColor(new Color(_renderer.material.color.r, _renderer.material.color.g, _renderer.material.color.b, 0f), 1f);
        
        Destroy(gameObject, 1.2f);
    }
    
    private void SetToTransparent()
    {
        var meshRenderer = GetComponent<MeshRenderer>();
        Material material = new Material(meshRenderer.material);
        material.shader = Shader.Find("Universal Render Pipeline/Lit");
        material.SetInt("_Surface", 1); // make it transparent.
        material.SetInt("_Blend", 0);   // set it to alpha blend
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.renderQueue = 3000;
        material.color = new Color(material.color.r, material.color.g, material.color.b, 1f);
        meshRenderer.material = material;
    }
}