using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabitBehaviour : MonoBehaviour
{
    public float moveSpeed = 6;

    Rigidbody rigidBody;
    Vector3 moveingPos;
    private float waitTime;
    public float startWaitTime = 3f;

    //public Transform[] moveSpot;
    public Transform moveSpot;
    //private int randomSpot;

    public float minX, maxX, minZ, maxZ;

    Animator animator;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        waitTime = startWaitTime;

        //randomSpot = Random.Range(0, moveSpot.Length);
        moveingPos = new Vector3(Random.Range(minX, maxX), transform.position.y, Random.Range(minZ, maxZ));
        //moveingPos = new Vector3(moveSpot[randomSpot].position.x, transform.position.y, moveSpot[randomSpot].position.z);
    }

    void Update()
    {
        Vector3 newLock = new Vector3(moveingPos.x, transform.position.y, moveingPos.x);
        transform.LookAt(newLock);

        if (Vector3.Distance(transform.position, newLock) < 0.2f)
        {
            if(waitTime >= 0)
            {
                animator.SetBool("isRunning", false);
                waitTime -= Time.deltaTime;
            }
            else
            {
                //moveingPos = new Vector3(Random.Range(-300, 450), transform.position.y, Random.Range(-300, 400));
                //randomSpot = Random.Range(0, moveSpot.Length);
                moveingPos = new Vector3(Random.Range(minX, maxX), transform.position.y, Random.Range(minZ, maxZ));
                waitTime = startWaitTime;
            }
            
            //Range is off
        }
        else
        {
            rigidBody.MovePosition(rigidBody.position + (transform.forward * moveSpeed) * Time.deltaTime);
            animator.SetBool("isRunning", true);
        }

    }
}
