using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSaveData
{
    public float[] position;
    public float[] rotation;

    public PlayerSaveData(PlayerController playerController)
    {
        position = new float[3];
        rotation = new float[3];

        position[0] = playerController.SpawnPoint.position.x;
        position[1] = playerController.SpawnPoint.position.y;
        position[2] = playerController.SpawnPoint.position.z;
                      
        rotation[0] = playerController.SpawnPoint.rotation.x;
        rotation[1] = playerController.SpawnPoint.rotation.y;
        rotation[2] = playerController.SpawnPoint.rotation.z;

    }  
}
