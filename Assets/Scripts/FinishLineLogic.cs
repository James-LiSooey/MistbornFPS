using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineLogic : MonoBehaviour
{
    public LevelController levelController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (levelController.timer < levelController.timeLimit)
            {
                levelController.gameStatus = "You Win!";
            }
            else
            {
                levelController.gameStatus = "You Failed";
            }
        }
    }
}
