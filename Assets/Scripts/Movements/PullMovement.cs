using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PullMovement : MovementType
{

    public GameObject pullTarget;
    [SerializeField]
    private float minForceDistance = 10f;
    [SerializeField]
    private float pullForceModifier = 2f;
    [SerializeField]
    private float pullForce = 10f;
    [SerializeField]
    private Vector3 pullHeightAdjustment = new Vector3(0, 2, 0);
    GameObject vaultHelper;

    public override void Movement()
    {
        if (!playerInput.pull)
        {
            player.ChangeStatus(Status.walking);
            return;
        }

        Metal metal = pullTarget.GetComponent<Metal>();


        Vector3 fromPullTarget = pullTarget.transform.position;
        Vector3 toPlayer = transform.position;
        Vector3 dir = toPlayer - fromPullTarget;

        if (metal.weight >= 6f)
        {
            fromPullTarget = pullTarget.transform.position + pullHeightAdjustment;
            toPlayer = transform.position;
            dir = toPlayer - fromPullTarget;
        }

        float pullSpeed = Mathf.Clamp(Mathf.Pow(minForceDistance / dir.magnitude, 1.5f), .01f, pullForceModifier);
        
        if(dir.magnitude < 1f)
        {
            pullSpeed = dir.magnitude;
        }
        
        Vector3 move = dir.normalized;

        var appliedGravity = 0f;

        if (pullSpeed < 2) appliedGravity = 1 - pullSpeed;

        if (metal.weight < 6f)
        {
            metal.Push(move, pullForce * pullSpeed * .2f, appliedGravity);
            if (metal.pinned)
            {
                movement.Move(move, -pullForce * pullSpeed, appliedGravity);
            }
            else
            {
                movement.Move(toPlayer, 0, 1);
            }

        }
        else
        {
            movement.Move(move, -pullForce * pullSpeed, appliedGravity);
        }
    }

    public override void Check(bool canInteract)
    {
        if (!canInteract) return;
        if (playerStatus == changeTo) return;

        if (!pullTarget) return;
        if (!playerInput.pull) return;

        player.ChangeStatus(changeTo, IK);
    }
}
