using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Metal : MonoBehaviour
{
    public float weight = 5f;
    public bool allomantic = true;
    [HideInInspector]
    public Vector3 moveDirection = Vector3.zero;
    [HideInInspector]
    private Rigidbody rigidbody1;
    
    private BoxCollider collider1;
    public bool pinned = false;
    [SerializeField]
    private bool pushed = false;

    private bool wallHit;
    private Vector3 wallHitLocation;
    private Vector3 direction = new Vector3(0, 0, 0);
    private float speed = 0;
    private float appliedGravity = 0;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody1 = GetComponent<Rigidbody>();
        collider1 = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (speed <= .01)
        {
            pushed = false;
        }
        else
        {
            pushed = true;
        }
        speed = 0;
    }

    public void Push(Vector3 direction1, float speed1, float appliedGravity1)
    {
        pushed = true;
        direction = direction1;
        speed = speed1;
        appliedGravity = appliedGravity1;
        Vector3 move = direction1 * speed1;
        if (appliedGravity > 20)
        {
            moveDirection.x = move.x;
            moveDirection.y -= 20 * Time.deltaTime * appliedGravity1;
            moveDirection.z = move.z;
        }
        else
        {
            moveDirection = move;
        }

        rigidbody1.AddForce((moveDirection *20 * Time.deltaTime), ForceMode.VelocityChange);
    }
    
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            return;
        }
        if (pushed)
        {
            pinned = true;
        }
        else
        {
            pinned = false;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        pinned = false;
    }
}
