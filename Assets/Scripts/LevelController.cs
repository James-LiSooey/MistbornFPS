using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    public float timer;
    public float timeLimit = 100f;

    public string gameStatus = "InProgress";

    private void Start()
    {
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > timeLimit && gameStatus == "InProgress")
        {
            gameStatus = "Failure";
        }
    }


}
