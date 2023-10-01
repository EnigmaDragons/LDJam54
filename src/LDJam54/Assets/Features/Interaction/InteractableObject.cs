using System;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private Renderer renderer;
    [SerializeField] private Collider collider;
    [SerializeField] private Collider[] colliders;
    public Rigidbody Body;
    //Hold Details
    public bool CanBeHeld;
    public DropSize DropSize;
    public float DistanceInFront;
    public float HeightOffset;
    [field: SerializeField] public float BobbingIntensity { get; private set; } = 0.05f;

    public Bounds Bounds
    {
        get
        {
            if (!_isHeld && !_isSettingDown)
                _bounds = collider.bounds;
            return _bounds;
        }

    }
    private Bounds _bounds;
    private bool _isSettingDown;
    private bool _isHeld;
    private float _setDownSpeed;
    private Vector3 _setDownDestination;
    private float _setDownRotationSpeed;
    private Quaternion _setDownRotation;
    
    private float yOffset;
    
    private void Start() => _bounds = collider.bounds;

    private void FixedUpdate()
    {
        if (!_isSettingDown)
            return;
        transform.rotation = Quaternion.Lerp(transform.rotation, _setDownRotation, _setDownRotationSpeed * Time.fixedDeltaTime);
        Body.MovePosition(Vector3.MoveTowards(transform.position, _setDownDestination, _setDownSpeed * Time.fixedDeltaTime));
        if (Vector3.Distance(Body.position, _setDownDestination) < 0.01f && Mathf.Abs(Mathf.Abs(Quaternion.Dot(transform.rotation, _setDownRotation)) - 1) < 0.05f)
        {
            transform.rotation = _setDownRotation;
            _isSettingDown = false;
            Body.useGravity = true;
            SetColliders(true);
        }
    }


    public void Hold()
    {
        _isSettingDown = false;
        _isHeld = true;
        Body.useGravity = false;
        SetColliders(false);
    }

    public void SetDown(Vector3 destination, Quaternion rotation, float speed, float rotationSpeed)
    {
        _isSettingDown = true;
        _isHeld = false;
        _setDownDestination = destination;
        _setDownRotation = rotation;
        _setDownSpeed = speed;
        _setDownRotationSpeed = rotationSpeed;
    }
    
    public void Release()
    {
        _isSettingDown = false;
        _isHeld = false;
        Body.useGravity = true;
        SetColliders(true);
    }

    public void Highlight()
    {
    }

    public void Unhighlight()
    {
    }

    public void PlayShippedAnimation()
    {
        //1 disable collider & rigidbody
        SetColliders(false);
        Body.isKinematic = true;
        
        //set the material to transparent
        SetToTransparent();
        
        //move box up
        transform.DOMoveY(transform.position.y + 0.5f, 1f);
        //fade out
        renderer.material.DOColor(new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 0f), 1f);
        
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

    public void Throw(Vector3 throwDirection, float throwForce)
    {
        Release();
        //add random rotational force
        Body.AddTorque(new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)), ForceMode.Impulse);
        Body.AddForce(throwDirection * throwForce, ForceMode.Impulse);
    }

    private void SetColliders(bool isEnabled) => colliders.ForEach(x => x.enabled = isEnabled);
}