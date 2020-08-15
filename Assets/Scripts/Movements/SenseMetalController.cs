using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenseMetalController : MovementType
{

    [SerializeField]
    GameObject renderTarget;

    Color c = Color.blue;

    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    void Update()
    {
        if(playerInput.SenseMetal()) {
            LineRenderer lRend = renderTarget.AddComponent<LineRenderer>();
            lRend.SetPosition(0, transform.position);
            lRend.SetPosition(1, renderTarget.transform.position);
            lRend.startWidth = 0.05f;
            lRend.endWidth = 0.05f;
            lRend.material = new Material(Shader.Find("Sprites/Default"));
            lRend.startColor = c;
            lRend.endColor = c;
        } else {
            Debug.Log("SenseMetal is false.");
        }
    }
}
