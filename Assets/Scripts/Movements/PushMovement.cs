using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PushMovement : MovementType
{
    public GameObject pushTarget;
    [SerializeField]
    private float minForceDistance = 10f;
    [SerializeField]
    private float pushForceModifier =2f;
    [SerializeField]
    private float pushForce = 10f;
    GameObject vaultHelper;

    public override void Movement()
    {
        Metal metal = pushTarget.GetComponent<Metal>();

        Vector3 fromPushTarget = pushTarget.transform.position;
        Vector3 toPlayer = transform.position;
        if (movement.grounded)
        {
            fromPushTarget.y = toPlayer.y;
        }

        Vector3 dir = toPlayer - fromPushTarget;
        float pushSpeed = Mathf.Clamp(Mathf.Pow(minForceDistance / dir.magnitude,1.5f), .01f, pushForceModifier);
        Vector3 move = dir.normalized;
        if (!playerInput.push) player.ChangeStatus(Status.walking);

        var appliedGravity = 0f;
        if (pushSpeed < 2) appliedGravity = 1- pushSpeed;

        if (metal.weight < 6f)
        {
            metal.Push(move, -pushForce * pushSpeed * .2f, appliedGravity);
            if (metal.pinned)
            {
                movement.Move(move, pushForce * pushSpeed, appliedGravity);
            }
            else
            {
                movement.Move(toPlayer, 0, 1);
            }

        }
        else
        {
            movement.Move(move, pushForce * pushSpeed, appliedGravity);
        }
    }

    public override void Check(bool canInteract)
    {
        if (!canInteract) return;
        if (playerStatus == changeTo) return;

        if (!pushTarget ) return;
        if (!playerInput.push) return;

        player.ChangeStatus(changeTo, IK);
    }
}


