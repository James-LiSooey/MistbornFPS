using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuLogic : MonoBehaviour
{
    public void ResetScene() {
        SceneManager.LoadScene("James Demo Scene");
    }
}
