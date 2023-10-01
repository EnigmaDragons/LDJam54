using UnityEngine;
using Input = UnityEngine.Input;

public class Door : MonoBehaviour {
	private Animation anim;
	public float OpenSpeed = 1;
	public float CloseSpeed = 1;
	public bool isAutomatic = false;
	public bool AutoClose = false;
	public bool DoubleSidesOpen = false;
	public string PlayerColliderTag = "MainCamera";
	public string OpenForwardAnimName = "Door_anim";
	public string OpenBackwardAnimName = "DoorBack_anim";
	private string _animName;
	private bool inTrigger = false;
	private bool isOpen = false;
	private Vector3 relativePos;
	
	private const bool DEBUG_LOG = false;

	void Log(string msg)
	{
		if (DEBUG_LOG)
			Debug.Log(msg);
	}
	
	void Start () 
	{
		anim = GetComponent<Animation> ();
		_animName = anim.clip.name;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (inTrigger) 
		{
			Log("In Door Trigger");
			if(Input.GetKeyDown(KeyCode.E) && !isAutomatic){
				if (!isOpen) {
					Debug.Log("Requested Door Open");
					isOpen = true;
					OpenDoor ();
				} else {
					Debug.Log("Requested Door Close");
					isOpen = false;
					CloseDoor ();
				}
			}
		}
	}
	
	void OpenDoor()
	{
		anim [_animName].speed = 1 * OpenSpeed;
		anim [_animName].normalizedTime = 0;
		anim.Play (_animName);
	}
	
	void CloseDoor()
	{
		anim [_animName].speed = -1 * CloseSpeed;
		if (anim [_animName].normalizedTime > 0) {
			anim [_animName].normalizedTime = anim [_animName].normalizedTime;
		} else {
			anim [_animName].normalizedTime = 1;
		}
		anim.Play (_animName);
	}

	void OnTriggerEnter(Collider other)
	{
		Log("Door Trigger Enter: " + other.name);
		
		if(other.GetComponent<Collider>().CompareTag(PlayerColliderTag)){
			if(DoubleSidesOpen){
			relativePos = gameObject.transform.InverseTransformPoint (other.transform.position);
			if (relativePos.z > 0) {
				_animName = OpenForwardAnimName;
			} else {
				_animName = OpenBackwardAnimName;
			}
			}
			if (isAutomatic) {
				OpenDoor ();
			}

			inTrigger = true;
			Log("In Door Trigger");
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if(other.GetComponent<Collider>().CompareTag(PlayerColliderTag))
		{
			if (isAutomatic) {
				CloseDoor ();
			} else {
				inTrigger = false;
				Log("Out of Door Trigger");
			}
			if (AutoClose && isOpen) {
				CloseDoor ();
				inTrigger = false;
				isOpen = false;
			}
		}
	}
}
