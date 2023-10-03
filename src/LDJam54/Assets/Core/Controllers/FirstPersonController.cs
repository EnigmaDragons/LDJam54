// FPS Controller
// 1. Create a Parent Object like a 3D model
// 2. Make the Camera the user is going to use as a child and move it to the height you wish. 
// 3. Attach a Rigidbody to the parent
// 4. Drag the Camera into the m_Camera public variable slot in the inspector
// Escape Key: Escapes the mouse lock
// Mouse click after pressing escape will lock the mouse again

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FirstPersonController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;
    [SerializeField] private float m_lookSensitivity = 3.0f;
    private float m_MovX;
    private float m_MovY;
    private Vector3 m_moveHorizontal;
    private Vector3 m_movVertical;
    public Vector3 m_velocity;
    private Rigidbody m_Rigid;
    private float m_yRot;
    private float m_xRot;
    private Vector3 m_rotation;
    private Vector3 m_cameraRotation;
    private bool m_cursorIsLocked = true;
    private bool debug_camera = false;

    [Header("The Camera the player looks through")]
    public Camera m_Camera;

    [SerializeField]
    AnimatorToSoundController animatorToSoundController;

    
    public float Speed { get => speed; set => speed = value; }
    // Use this for initialization
    private void Start()
    {
        m_Rigid = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (debug_camera)
            InvokeRepeating(nameof(LogCamera), 1, 1);
    }

    private void LogCamera()
    {
        Debug.Log("Body: " + m_Rigid.transform.eulerAngles);
        Debug.Log("Camera: " + m_Camera.transform.eulerAngles);
    }

    // Update is called once per frame
    public void Update()
    {
        if (!FirstPersonInteractionStatus.IsEnabled)
            return;
        
        m_MovX = Input.GetAxis("Horizontal");
        m_MovY = Input.GetAxis("Vertical");

        m_moveHorizontal = transform.right * m_MovX;
        m_movVertical = transform.forward * m_MovY;

        m_yRot = Input.GetAxisRaw("Mouse X");
        m_rotation = new Vector3(0, m_yRot, 0) * m_lookSensitivity;

        m_xRot = Input.GetAxisRaw("Mouse Y");
        m_cameraRotation = new Vector3(m_xRot, 0, 0) * m_lookSensitivity;

        //apply camera rotation
        if (m_rotation != Vector3.zero)
        {
            //rotate the camera of the player
            m_Rigid.MoveRotation(m_Rigid.rotation * Quaternion.Euler(m_rotation));
        }

        if (m_Camera != null)
        {
            //negate this value so it rotates like a FPS not like a plane
            m_Camera.transform.Rotate(-m_cameraRotation);

            Vector3 cameraRotation = m_Camera.transform.eulerAngles;
            if (cameraRotation.z > 1 && cameraRotation.z < 359)
            {
                Debug.Log("Camera Clamping from: " + cameraRotation);
                cameraRotation.x = cameraRotation.x > 270 ? 270.01f : 89.99f;
                cameraRotation.y = (cameraRotation.y + 180) % 360;
                //cameraRotation.y = m_Rigid.transform.eulerAngles.y;
                cameraRotation.z = 0;
                Debug.Log("to: " + cameraRotation);
                m_Camera.transform.eulerAngles = cameraRotation;
            }
        }

        InternalLockUpdate();
    }
    
    public void FixedUpdate()
    {        
        if (!FirstPersonInteractionStatus.IsEnabled)
            return;
        
        m_velocity = (m_moveHorizontal + m_movVertical).normalized * speed;
        //move the actual player here
        if (m_velocity != Vector3.zero)
        {
            m_Rigid.MovePosition(m_Rigid.position + m_velocity * Time.fixedDeltaTime);
        }
    }   

    //controls the locking and unlocking of the mouse
    private void InternalLockUpdate()
    {
        // if (Input.GetKeyUp(KeyCode.Escape))
        //     m_cursorIsLocked = false;
        if (Input.GetMouseButtonUp(0))
            m_cursorIsLocked = true;

        MouseState.SetCursorVisible(!m_cursorIsLocked);
    }
}
