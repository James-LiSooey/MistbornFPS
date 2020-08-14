using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PushMovement : MovementType
{
    [SerializeField]
    private GameObject pushTarget;
    [SerializeField]
    private float minForceDistance = 10f;
    [SerializeField]
    private float pushForceModifier =2f;
    GameObject vaultHelper;

    /*
    void CreateVaultHelper()
    {
        vaultHelper = new GameObject();
        vaultHelper.transform.name = "_Vault Helper";
    }

    void SetVaultHelper()
    {
        vaultHelper.transform.position = vaultOver;
        vaultHelper.transform.rotation = Quaternion.LookRotation(vaultDir);
    }

    public override void SetPlayerComponents(PlayerMovement move, PlayerInput input)
    {
        base.SetPlayerComponents(move, input);
        CreateVaultHelper();
    }
    */

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
            metal.Push(move, -movement.walkSpeed * pushSpeed, appliedGravity);
        }
        else
        {
            movement.Move(move, movement.walkSpeed * pushSpeed, appliedGravity);
        }
    }

    public override void Check(bool canInteract)
    {
        if (!canInteract) return;
        if (playerStatus == changeTo) return;

        if (!(pushTarget && playerInput.push)) return;



        //movement.controller.height = player.info.radius;
        player.ChangeStatus(changeTo, IK);

    }
    /*
    float movementAdjust = (Vector3.ClampMagnitude(movement.controller.velocity, 16f).magnitude / 16f);
        float checkDis = player.info.radius + movementAdjust;

        //is there a vault layer in front? and did the player push jump?
        if (player.hasObjectInfront(checkDis, vaultLayer) && playerInput.Jump())
        {
            //determines landing position pt1
            if (Physics.SphereCast(transform.position + (transform.forward * (player.info.radius - 0.25f)), 0.25f, transform.forward, out var sphereHit, checkDis, vaultLayer))
            {
                //determines landing position pt2
                if (Physics.SphereCast(sphereHit.point + (Vector3.up * player.info.halfheight), player.info.radius, Vector3.down, out var hit, player.info.halfheight - player.info.radius, vaultLayer))
                {
                    Debug.DrawRay(hit.point + (Vector3.up * player.info.radius), Vector3.up * player.info.halfheight);
                    //Check above the point to make sure the player can fit
                    if (Physics.SphereCast(hit.point + (Vector3.up * player.info.radius), player.info.radius, Vector3.up, out var trash, player.info.halfheight))
                        return; //If cannot fit the player then do not vault

                    //Check in-front of the vault to see if something is blocking
                    Vector3 fromPlayer = transform.position;
                    Vector3 toVault = hit.point + (Vector3.up * player.info.radius);
                    fromPlayer.y = toVault.y;

                    Vector3 dir = (toVault - fromPlayer);
                    if (Physics.SphereCast(fromPlayer, player.info.radius / 2f, dir.normalized, out var trash2, dir.magnitude + player.info.radius))
                        return; //If we hit something blocking the vault, then do nothing

                    vaultOver = hit.point;
                    vaultDir = transform.forward;
                    SetVaultHelper();

                    movement.controller.height = player.info.radius;
                    player.ChangeStatus(changeTo, IK);
                }
            }
        }
    */

}


