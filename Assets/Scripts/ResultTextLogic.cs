using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultTextLogic : MonoBehaviour
{
    public LevelController levelController;
    private TMP_Text resultText;

    // Start is called before the first frame update
    void Start()
    {
        resultText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (levelController.gameStatus != "InProgress")
        {
            resultText.text = levelController.gameStatus;
        }
    }
}
