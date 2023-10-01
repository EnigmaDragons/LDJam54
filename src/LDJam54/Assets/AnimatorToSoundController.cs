using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorToSoundController : MonoBehaviour
{
    private Animator ani;
    [SerializeField] float walkSpeed = 1.0f;
    [SerializeField] float runSpeed = 1.5f;

    void Start()
    {
        ani = gameObject.GetComponent<Animator>();
    }

    public void WalkAnimationStart()
    {
        ani.SetBool("isMoving", true);
    }

    public void WalkAnimationStop()
    {
        ani.SetBool("isMoving", false);
    }

    public void StartRunning()
    {
        ani.speed = runSpeed;
    }

    public void StopRunning()
    {
        ani.speed = walkSpeed;
    }
}
