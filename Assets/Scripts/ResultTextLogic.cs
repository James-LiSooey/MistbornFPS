using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultTextLogic : MonoBehaviour
{
    public LevelController levelController;
    private TMP_Text resultText;

    PlayerInput playerInput;

    // Start is called before the first frame update
    void Start()
    {
        resultText = GetComponent<TMP_Text>();
        playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInput.pauseMenuEnabled) {
            resultText.enabled = false;;
        } else {
            resultText.enabled = true;
        }

        if (levelController.gameStatus != "InProgress")
        {
            resultText.text = levelController.gameStatus;
        }
    }
}
