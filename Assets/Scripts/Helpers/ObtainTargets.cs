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
            allomancyTargeting.screenTargets.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        allomancyTargeting.screenTargets.Remove(other.transform);
    }
}
