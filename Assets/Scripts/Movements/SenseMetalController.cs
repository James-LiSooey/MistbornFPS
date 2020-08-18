using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenseMetalController : MovementType
{

    [SerializeField]
    Material lineMat;
    public List<GameObject> metalObjects = new List<GameObject>();

    Color c = Color.blue;

    // Start is called before the first frame update
    void Awake()
    {
        foreach(GameObject metalObj in GameObject.FindGameObjectsWithTag("Metal")) {
            metalObjects.Add(metalObj);
            metalObj.AddComponent<LineRenderer>();
            metalObj.GetComponent<LineRenderer>().startWidth = 0.01f;
            metalObj.GetComponent<LineRenderer>().endWidth = 0.01f;
            //metalObj.GetComponent<LineRenderer>().material = new Material(Shader.Find("Sprites/Default"));
            metalObj.GetComponent<LineRenderer>().material = lineMat;
            //metalObj.GetComponent<LineRenderer>().startColor = c;
            //metalObj.GetComponent<LineRenderer>().endColor = c;
            metalObj.GetComponent<LineRenderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("SenseMetal: " + playerInput.SenseMetal());
        if(playerInput.SenseMetal()) {
            foreach(GameObject metalObj in metalObjects) {
                metalObj.GetComponent<LineRenderer>().enabled = true;
                metalObj.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                metalObj.GetComponent<LineRenderer>().SetPosition(1, metalObj.transform.position);
            }
        } else {
            foreach(GameObject metalObj in metalObjects) {
                metalObj.GetComponent<LineRenderer>().enabled = false;
            }
        }
    }
}
