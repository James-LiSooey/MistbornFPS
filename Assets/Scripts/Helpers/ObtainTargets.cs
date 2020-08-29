using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObtainTargets : MonoBehaviour
{

    private AllomancyTargeting allomancyTargeting;
    // Start is called before the first frame update
    void Start()
    {
        allomancyTargeting = GetComponentInParent<AllomancyTargeting>();
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Metal"))
        {
            var metalObject = other.gameObject.GetComponent<Metal>();
            if (metalObject.allomantic)
            {
                allomancyTargeting.screenTargets.Add(other.transform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        allomancyTargeting.screenTargets.Remove(other.transform);
    }

    public void AddTarget(GameObject targetGameObject)
    {
        allomancyTargeting.screenTargets.Add(targetGameObject.transform);
    }

    public void RemoveTarget(GameObject targetGameObject)
    {
        allomancyTargeting.screenTargets.Remove(targetGameObject.transform);
    }
}
