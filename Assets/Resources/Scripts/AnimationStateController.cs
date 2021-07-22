using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    public static float startTime;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey("i"))
        {
            animator.SetBool("isGuarding", true);
        }
        else
        {
            animator.SetBool("isGuarding", false);
        }
    }
}
