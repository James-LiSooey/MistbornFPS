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
    
    private MeshCollider collider1;
    public bool pinned = false;
    public bool equiped = false;
    [SerializeField]
    private bool pushed = false;
    public Transform equipTransform;
    private bool wallHit;
    private Vector3 wallHitLocation;
    private Vector3 direction = new Vector3(0, 0, 0);
    private float speed = 0;
    private float appliedGravity = 0;

    public List<GameObject> collisionsGOList;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody1 = GetComponent<Rigidbody>();
        collider1 = GetComponent<MeshCollider>();
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
        if (equiped)
        {
            transform.position = equipTransform.position;
        }
    }

    private void FixedUpdate()
    {
        if (equiped)
        {
            pinned = false;
            return;
        }

        if(moveDirection == Vector3.zero)
        {
            pinned = false;
            return;
        }

        RaycastHit rayHit;

        if (Physics.Raycast(transform.position, moveDirection.normalized, out rayHit, 2f)){
            pinned = true;
        }

        rigidbody1.AddForce((moveDirection * 20 * Time.deltaTime), ForceMode.VelocityChange);

        moveDirection = Vector3.zero;
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
    }

    public void EquipObject()
    {
        equiped = true;
        rigidbody1.isKinematic = true;
        rigidbody1.velocity = Vector3.zero;
        collider1.enabled = false;
        pinned = false;
    }

    public void EquipObject(Transform equipTransformTarget)
    {
        pinned = false;
        equiped = true;
        rigidbody1.isKinematic = true;
        rigidbody1.velocity = Vector3.zero;
        collider1.enabled = false;
        equipTransform = equipTransformTarget;
        transform.position = equipTransform.position;
    }

    public void UnEquipObject()
    {
        if (equiped)
        {
            equiped = false;
            rigidbody1.isKinematic = false;
            collider1.enabled = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            collisionsGOList.Add(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collisionsGOList.Contains(collision.gameObject))
        {
            collisionsGOList.Remove(collision.gameObject);
        }

    }
}
