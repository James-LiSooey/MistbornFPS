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
        Metal metal = pullTarget.GetComponent<Metal>();

        Vector3 fromPullTarget = pullTarget.transform.position + pullHeightAdjustment;
        Vector3 toPlayer = transform.position;
        if (movement.grounded)
        {
            //fromPullTarget.y = toPlayer.y;
        }

        Vector3 dir = toPlayer - fromPullTarget;
        float pullSpeed = Mathf.Clamp(Mathf.Pow(minForceDistance / dir.magnitude, 1.5f), .01f, pullForceModifier);
        if(dir.magnitude < .5)
        {
            pullSpeed = dir.magnitude;
        }
        Vector3 move = dir.normalized;
        if (!playerInput.pull || metal.equiped) player.ChangeStatus(Status.walking);

        var appliedGravity = 0f;
        if (pullSpeed < 2) appliedGravity = 1 - pullSpeed;

        if (metal.weight < 6f)
        {
            metal.Push(move, pullForce * pullSpeed *.2f, appliedGravity);
            //if (metal.pinned)
            //{
            //    movement.Move(move, -pullForce * pullSpeed, appliedGravity);
            //}
            //else
            //{
                movement.Move(toPlayer, 0, 1);
            //}

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

    /*
    public float stoppingDistance = 1.5f;
    
    [SerializeField]
    private LayerMask metalLayer;
    private Vector3 pullPoint;
    private int metalLayerInt;

    Vector3 pullDir;

    GameObject vaultHelper;
    public GameObject cam;

    void CreateVaultHelper()
    {
        vaultHelper = new GameObject();
        vaultHelper.transform.name = "_Vault Helper";
    }

    void SetVaultHelper()
    {
        //vaultHelper.transform.position = vaultOver;
        //vaultHelper.transform.rotation = Quaternion.LookRotation(vaultDir);
    }

    public override void SetPlayerComponents(PlayerMovement move, PlayerInput input)
    {
        base.SetPlayerComponents(move, input);
        //CreateVaultHelper();
    }

    public override void Movement()
    {
        Vector3 dir = pullPoint - transform.position;
        //Vector3 localPos = vaultHelper.transform.InverseTransformPoint(transform.position);
        Vector3 move = dir.normalized;

        if (!playerInput.pull) player.ChangeStatus(Status.walking);

        // if (localPos.z < -(player.info.radius * 2f))
        //     move = dir.normalized;
        // else if (localPos.z > player.info.halfheight)
        // {
        //     movement.controller.height = player.info.height;
        //     player.ChangeStatus(Status.walking);
        // }

        //float distance = Vector3.Distance(destination.position, transform.position);

        movement.Move(move, movement.runSpeed, 0f);
    }

    public override void Check(bool canInteract)
    {
        if (!canInteract) return;
        if (playerStatus == changeTo) return;

        //float movementAdjust = (Vector3.ClampMagnitude(movement.controller.velocity, 50f).magnitude / 16f);
        //float checkDis = player.info.radius + movementAdjust;

        RaycastHit hit;

        if(playerInput.pull) {
            //pullDir = transform.forward;
            //Debug.DrawRay(transform.position, cam.transform.forward, Color.white);
            if(Physics.Raycast(transform.position, transform.forward, out hit, 100f, LayerMask.GetMask("Metal"))) {
            //if(Physics.Raycast(transform.position, transform.forward, out hit)) {
                //Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.yellow);
                pullPoint = hit.point;
                //Debug.Log("Did Hit");
                //Debug.Log("Hit object layer " + hit.transform.gameObject.layer);
                player.ChangeStatus(changeTo, IK);
            } else {
                //Debug.DrawRay(transform.position, transform.forward * 1000, Color.white);
                //Debug.Log("Did not Hit");
            }
        }

        //is there a vault layer in front? and did the player push jump?
        // if (player.hasObjectInfront(checkDis, metalLayer) && playerInput.pull)
        // {
        //     //determines landing position pt1
        //     if (Physics.SphereCast(transform.position + (transform.forward * (player.info.radius - 0.25f)), 0.25f, transform.forward, out var sphereHit, checkDis, metalLayer))
        //     {
        //         //determines landing position pt2
        //         if (Physics.SphereCast(sphereHit.point + (Vector3.up * player.info.halfheight), player.info.radius, Vector3.down, out var hit, player.info.halfheight - player.info.radius, metalLayer))
        //         {
        //             Debug.DrawRay(hit.point + (Vector3.up * player.info.radius), Vector3.up * player.info.halfheight);
        //             //Check above the point to make sure the player can fit
        //             if (Physics.SphereCast(hit.point + (Vector3.up * player.info.radius), player.info.radius, Vector3.up, out var trash, player.info.halfheight))
        //                 return; //If cannot fit the player then do not vault

        //             //Check in-front of the vault to see if something is blocking
        //             Vector3 fromPlayer = transform.position;
        //             Vector3 toVault = hit.point + (Vector3.up * player.info.radius);
        //             fromPlayer.y = toVault.y;

        //             Vector3 dir = (toVault - fromPlayer);
        //             if (Physics.SphereCast(fromPlayer, player.info.radius / 2f, dir.normalized, out var trash2, dir.magnitude + player.info.radius))
        //                 return; //If we hit something blocking the vault, then do nothing

        //             //vaultOver = hit.point;
        //             pullDir = transform.forward;
        //             //SetVaultHelper();

        //             //movement.controller.height = player.info.radius;
        //             player.ChangeStatus(changeTo, IK);
        //         }
        //     }
        // }
    }

    public override IKData IK()
    {
        IKData data = new IKData();
        //data.handPos = vaultOver + (Vector3.up * player.info.radius);
        //data.handEulerAngles = Quaternion.LookRotation(vaultDir - Vector3.up).eulerAngles;
        //data.armElbowPos = vaultOver;
        //data.armElbowPos.y = transform.position.y;
        //data.armElbowPos += Vector3.Cross(vaultDir, Vector3.up) * player.info.radius;
        return data;
    }
}*/
