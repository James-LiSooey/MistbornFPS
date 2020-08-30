using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuLogic : MonoBehaviour
{
    [SerializeField]
    Button defaultButton;

    private void Start() {
        if(defaultButton) {
            defaultButton.Select();
        }
    }

    public void ResetScene() {
        SceneManager.LoadScene("James Demo Scene");
    }
}
