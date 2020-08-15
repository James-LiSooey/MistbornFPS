using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenseMetalController : MonoBehaviour
{

    [SerializeField]
    GameObject renderTarget;

    Color c = Color.blue;

    PlayerInput playerInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInput.SenseMetal()) {
            //LineRenderer lRend = gameObject.AddComponent(typeof(LineRenderer)) as LineRenderer;
            LineRenderer lRend = gameObject.AddComponent<LineRenderer>();
            lRend.SetPosition(0, transform.position);
            lRend.SetPosition(1, renderTarget.transform.position);
            lRend.material = new Material(Shader.Find("Sprites/Default"));
            lRend.startColor = c;
            lRend.endColor = c;
        } else {

        }
    }
}
