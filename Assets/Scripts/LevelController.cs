using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{

    public float timer;
    public float timeLimit = 100f;

    public string gameStatus = "InProgress";

    PlayerInput playerInput;

    private void Start()
    {
        timer = 0;
        playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if(playerInput.pauseMenuEnabled) return;
        
        timer += Time.deltaTime;

        if (timer > timeLimit && gameStatus == "InProgress")
        {
            gameStatus = "Failure";
        }
    }


}
