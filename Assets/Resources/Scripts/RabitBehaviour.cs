using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabitBehaviour : MonoBehaviour
{
    public float moveSpeed = 6;

    Rigidbody rigidBody;
    Vector3 velocity;
    Vector3 moveingPos;

    Animator animator;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        moveingPos = new Vector3(Random.Range(-300, 450), transform.position.y, Random.Range(-300, 400));
    }

    void Update()
    {
        //if (Input.GetKey("i"))
        //{
        //    animator.SetBool("isGuarding", true);
        //}
        //else
        //{
        //    animator.SetBool("isGuarding", false);
        //}

        Vector3 newLock = new Vector3(moveingPos.x, transform.position.y, moveingPos.x);
        transform.LookAt(newLock);

        if (transform.position.x > (newLock.x-10) && transform.position.x < (newLock.x+10) && transform.position.z > (newLock.z-10) && transform.position.z < (newLock.z+10))
        {
            animator.SetBool("isRunning", false);
            //yield return new WaitForSeconds(3);
            moveingPos = new Vector3(Random.Range(-300, 450), transform.position.y, Random.Range(-300, 400));
            //Range is off
        }
        else
        {
            rigidBody.MovePosition(rigidBody.position + (transform.forward * moveSpeed) * Time.deltaTime);
            animator.SetBool("isRunning", true);
        }

    }
}
