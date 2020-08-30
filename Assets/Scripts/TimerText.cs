using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerText : MonoBehaviour
{
    private TMP_Text timerText;
    public LevelController levelController;
    public int timer;

    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (levelController.gameStatus == "InProgress")
        {
            timer = (int)levelController.timer;
            timerText.text = timer.ToString();
        }
    }
}
