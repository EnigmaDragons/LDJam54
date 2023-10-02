using TMPro;
using UnityEngine;
using Input = UnityEngine.Input;

public class Door : MonoBehaviour {
	private Animation anim;
	public float OpenSpeed = 1;
	public float CloseSpeed = 1;
	public bool isAutomatic = false;
	public bool AutoClose = false;
	public bool DoubleSidesOpen = false;
	public string PlayerColliderTag = "Player";
	public string OpenForwardAnimName = "Door_anim";
	public string OpenBackwardAnimName = "Door_anim";
	private string _animName;
	private bool inTrigger = false;
	private bool isOpen = false;
	public bool startedLocked = false;
	private Vector3 relativePos;
	public GameObject lockedVisual;
	public GameObject unlockedVisual;
	public TextMeshPro forwardLabel;
	public TextMeshPro backwardLabel;
	public string initialForwardText;
	public string initialBackwardText;
	
	private const bool DEBUG_LOG = false;
	private bool _lockedInitialized = false;
	private bool _isLocked;
	
	public bool Locked => _isLocked;

	void Log(string msg)
	{
		if (DEBUG_LOG)
			Debug.Log(msg);
	}
	
	void Start() 
	{
		anim = GetComponent<Animation> ();
		_animName = anim.clip.name;
		if (!_lockedInitialized)
			SetLocked(startedLocked);
		if (forwardLabel.text == "")
			forwardLabel.text = initialForwardText;
		if (backwardLabel.text == "")
			backwardLabel.text = initialBackwardText;
	}

	public void SetLabels(string forwardText, string backwardText)
	{
		if (forwardLabel != null)
			forwardLabel.text = forwardText;
		if (backwardLabel != null)
			backwardLabel.text = backwardText;
	}
	
	public void SetLocked(bool isLocked)
	{
		_lockedInitialized = true;
		_isLocked = isLocked;
		if (lockedVisual != null)
			lockedVisual.SetActive(isLocked);
		if (unlockedVisual != null)
			unlockedVisual.SetActive(!isLocked);
	}

	// Update is called once per frame
	void Update () 
	{
		if (inTrigger) 
		{
			Log("In Door Trigger");
			if(Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0) && !isAutomatic){
				if (!isOpen) {
					Debug.Log("Requested Door Open");
					if (_isLocked)
					{
						Log("Door is locked");
					}
					else
					{
						isOpen = true;
						Log("Opening Door");
						OpenDoor();
					}
				} else {
					isOpen = false;
					Log("Closing Door");
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
		Message.Publish(new DoorOpened(transform.position));
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
		Message.Publish(new DoorClosed(transform.position));
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
			if (isAutomatic && !_isLocked) {
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
