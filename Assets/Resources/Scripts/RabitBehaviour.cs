using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabitBehaviour : MonoBehaviour
{
    public float moveSpeed = 6;

    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask obstacleMask;
    public LayerMask foodMask;

    Rigidbody rigidBody;
    Vector3 moveingPos;
    float dirNum;

    private float waitTime;
    public float startWaitTime = 5f;

    //public Transform[] moveSpot;
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

        FindVisibleObsticles(newLock);

        if (Vector3.Distance(transform.position, newLock) < 0.2f)
        {
            if(waitTime >= 0)
            {
                animator.SetBool("isRunning", false);
                waitTime -= Time.deltaTime;
                print("[Waiting]");
            }
            else
            {
                //moveingPos = new Vector3(Random.Range(-300, 450), transform.position.y, Random.Range(-300, 400));
                //randomSpot = Random.Range(0, moveSpot.Length);
                moveingPos = new Vector3(Random.Range(minX, maxX), transform.position.y, Random.Range(minZ, maxZ));
                waitTime = startWaitTime;
                print("[Randomized location]");
            }
            
            //Range is off
        }
        else
        {
            rigidBody.MovePosition(rigidBody.position + (transform.forward * moveSpeed) * Time.deltaTime);
            animator.SetBool("isRunning", true);
        }

    }

    void FindVisibleObsticles(Vector3 Location)
    {
        Collider[] obstaclesInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, obstacleMask);

        if (obstaclesInViewRadius.Length > 0) 
        {
            for (int i = 0; i < obstaclesInViewRadius.Length; i++)
            {
                Transform target = obstaclesInViewRadius[i].transform;
                Vector3 dirToTarget = (target.position - transform.position);
                if (Vector3.Angle(transform.forward, dirToTarget.normalized) < viewAngle / 2)
                {
                    float dstToTarget = Vector3.Distance(transform.position, target.position);

                    if (dstToTarget > 5f)
                    {
                        transform.LookAt(Location);
                        print("[Object Detected; Dist to Target : " + dstToTarget + "]");
                    }
                    else
                    {
                        //float angle = Vector3.Angle(transform.position, dirToTarget);
                        print("[Collided; Angle to the Target : " + dirToTarget + "]");
                        //Vector3 accDir = new Vector3(dirToTarget.x * 2, 0, dirToTarget.z * 2);
                        //transform.LookAt(Location - (accDir));
                        dirNum = AngleDir(transform.forward, dirToTarget, transform.up);

                        if (dirNum == -1)
                        {
                            transform.Rotate(Vector3.up * 100f * Time.deltaTime);
                        }
                        else if(dirNum == 1)
                        {
                            transform.Rotate(Vector3.up * -100f * Time.deltaTime);
                        }
                        

                        //print("[Collided; Dist to Target : " + dstToTarget + "]");
                    }
                }
            }
        }
        else
        {
            transform.LookAt(Location);
        }

    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);

        if (dir > 0f)
        {
            return 1f;
        }
        else if (dir < 0f)
        {
            return -1f;
        }
        else
        {
            return 0f;
        }
    }

}
