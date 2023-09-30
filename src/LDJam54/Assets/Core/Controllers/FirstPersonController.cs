using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FirstPersonController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;
    [SerializeField]
    private float m_lookSensitivity = 3.0f;
    private Vector3 m_velocity;
    private Rigidbody m_Rigid;
    private bool m_cursorIsLocked = true;

    [Header("The Camera the player looks through")]
    public Camera m_Camera;

    public float Speed { get => speed; set => speed = value; }

    private void Start()
    {
        m_Rigid = GetComponent<Rigidbody>();
        LockCursor();
    }

    private void Update()
    {
        // Mouse movement 
        float m_yRot = Input.GetAxisRaw("Mouse X") * m_lookSensitivity;
        float m_xRot = Input.GetAxisRaw("Mouse Y") * m_lookSensitivity;

        // Apply camera rotation
        if (m_Camera != null)
        {
            m_Camera.transform.Rotate(-m_xRot * Time.deltaTime, m_yRot * Time.deltaTime, 0);
        }

        InternalLockUpdate();
    }

    private void FixedUpdate()
    {
        float m_MovX = Input.GetAxis("Horizontal");
        float m_MovY = Input.GetAxis("Vertical");

        Vector3 m_moveHorizontal = transform.right * m_MovX;
        Vector3 m_movVertical = transform.forward * m_MovY;

        m_velocity = (m_moveHorizontal + m_movVertical).normalized * speed;

        // Move the actual player here
        if (m_velocity != Vector3.zero)
        {
            m_Rigid.MovePosition(m_Rigid.position + m_velocity * Time.fixedDeltaTime);
        }
    }

    private void InternalLockUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            m_cursorIsLocked = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            m_cursorIsLocked = true;
        }

        if (m_cursorIsLocked)
        {
            UnlockCursor();
        }
        else
        {
            LockCursor();
        }
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
