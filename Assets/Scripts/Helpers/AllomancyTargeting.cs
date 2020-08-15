using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllomancyTargeting : MonoBehaviour
{
    public List<Transform> screenTargets = new List<Transform>();
    public Transform target;
    public Material targetMaterial;
    private Material baseMaterial;
    private Renderer renderer;

    private PushMovement pushmovement;
    // Start is called before the first frame update
    void Start()
    {
        if (screenTargets.Count > 0)
        {
            target = screenTargets[targetIndex()];
        }

        pushmovement = GetComponent<PushMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        
        if (screenTargets.Count > 0)
        {
            if (!target)
            {
                target = screenTargets[targetIndex()];
            }
                renderer = target.gameObject.GetComponent<Renderer>();
                renderer.material = baseMaterial;

            target = screenTargets[targetIndex()];
            renderer = target.gameObject.GetComponent<Renderer>();
            baseMaterial = renderer.material;
            renderer.material = targetMaterial;
            pushmovement.pushTarget = target.gameObject;
        }

    }

    public int targetIndex()
    {
        float[] distances = new float[screenTargets.Count];

        for (int i = 0; i < screenTargets.Count; i++)
        {
            distances[i] = Vector2.Distance(Camera.main.WorldToScreenPoint(screenTargets[i].position), new Vector2(Screen.width / 2, Screen.height / 2));
        }

        float minDistance = Mathf.Min(distances);
        int index = 0;

        for (int i = 0; i < distances.Length; i++)
        {
            if (minDistance == distances[i])
                index = i;
        }

        return index;

    }
}
