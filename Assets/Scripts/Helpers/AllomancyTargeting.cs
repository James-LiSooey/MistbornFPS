﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllomancyTargeting : MonoBehaviour
{
    public List<Transform> screenTargets = new List<Transform>();
    public Transform target;
    public Material targetMaterial;
    private Material baseMaterial;
    private Renderer targetRenderer;

    public Transform equipedTransform;
    private PlayerInput playerInput;
    
    private PushMovement pushmovement;
    private PullMovement pullmovement;
    // Start is called before the first frame update
    void Start()
    {
        if (screenTargets.Count > 0)
        {
            target = screenTargets[targetIndex()];
        }

        playerInput = GetComponent<PlayerInput>();
        pushmovement = GetComponent<PushMovement>();
        pullmovement = GetComponent<PullMovement>();
    }

    // Update is called once per frame
    void Update()
    {

        if (equipedTransform && equipedTransform.gameObject)
        {
            var metal = equipedTransform.gameObject.GetComponent<Metal>();
            if (!metal.equiped)
            {
                equipedTransform = null;
            }
        }

        if ((target && playerInput.lockOn) || playerInput.push || playerInput.pull)
        {
            return;
        }

        if(equipedTransform && playerInput.AimEquipped)
        {
            if (!target)
            {
                target = screenTargets[targetIndex()];
            }

            targetRenderer = target.gameObject.GetComponent<Renderer>();
            targetRenderer.material = baseMaterial;

            target = equipedTransform;

            targetRenderer = target.gameObject.GetComponent<Renderer>();
            baseMaterial = targetRenderer.material;
            targetRenderer.material = targetMaterial;
            pushmovement.pushTarget = target.gameObject;
            pullmovement.pullTarget = target.gameObject;

            return;
        }

        if (screenTargets.Count > 0)
        {
            if (!target)
            {
                target = screenTargets[targetIndex()];
            }

            targetRenderer = target.gameObject.GetComponent<Renderer>();
            targetRenderer.material = baseMaterial;

            target = screenTargets[targetIndex()];
            targetRenderer = target.gameObject.GetComponent<Renderer>();
            baseMaterial = targetRenderer.material;
            targetRenderer.material = targetMaterial;
            pushmovement.pushTarget = target.gameObject;
            pullmovement.pullTarget = target.gameObject;
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
            {
                index = i;
            }
        }

        return index;

    }

    public void RemoveTarget(GameObject targetGameObject)
    {
        screenTargets.Remove(targetGameObject.transform);
    }

    public void EquipTarget(GameObject equipGameObject)
    {
        screenTargets.Remove(equipGameObject.transform);
        equipedTransform = equipGameObject.transform;
    }

    public void UnEquipTarget()
    {
        equipedTransform = null;
    }
}
