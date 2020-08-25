using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SenseMetalController : MovementType
{

    [SerializeField]
    Material lineMat;

    public float metalSenseDistance = 25f;
    public List<GameObject> metalObjects = new List<GameObject>();


    private Vector3 lineSpawnPoint;

    // Start is called before the first frame update
    void Awake()
    {
        foreach(GameObject metalObj in GameObject.FindGameObjectsWithTag("Metal")) {
            metalObjects.Add(metalObj);
            metalObj.AddComponent<LineRenderer>();
            metalObj.GetComponent<LineRenderer>().startWidth = 0.001f;
            metalObj.GetComponent<LineRenderer>().endWidth = 0.01f;
            metalObj.GetComponent<LineRenderer>().material = lineMat;
            metalObj.GetComponent<LineRenderer>().enabled = false;
        }
    }

    //Update is called once per frame
    void Update()
    {
        if(playerInput.senseMetal) {
            foreach(GameObject metalObj in metalObjects) {
                float distance = Vector3.Distance(transform.position, metalObj.transform.position);

                if(distance < metalSenseDistance) {
                    lineSpawnPoint = transform.position + transform.forward*0.09f;
                    lineSpawnPoint.y += player.info.halfheight/2;

                    metalObj.GetComponent<LineRenderer>().enabled = true;
                    metalObj.GetComponent<LineRenderer>().SetPosition(0, lineSpawnPoint);
                    metalObj.GetComponent<LineRenderer>().SetPosition(1, metalObj.transform.position);
                } else {
                    metalObj.GetComponent<LineRenderer>().enabled = false;
                }
            }
        } else {
            DisableLines();
        }
    }

    void DisableLines() {
        foreach(GameObject metalObj in metalObjects) {
                metalObj.GetComponent<LineRenderer>().enabled = false;
            }
    }
}
