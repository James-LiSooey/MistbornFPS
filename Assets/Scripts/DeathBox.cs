using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var playerMovement = other.gameObject.GetComponent<PlayerMovement>();
            var playerController = other.gameObject.GetComponent<PlayerController>();

            Debug.Log("SpawnPoint: " + playerController.SpawnPoint.position);

            // if(playerController.SpawnPoint == null) {
            //     playerMovement.ResetPositionTo(Vector3.zero);
            // } else {
                playerMovement.ResetPositionTo(playerController.SpawnPoint.position);
            //}
        }
    }
}
