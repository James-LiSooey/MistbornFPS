using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointLogic : MonoBehaviour
{
    public Transform checkPointSpawn;
    private Collider colliderCheckPoint;

    private void Start()
    {
         colliderCheckPoint = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            var playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.SpawnPoint = checkPointSpawn;

            colliderCheckPoint.enabled = false;
        }
    }
}
