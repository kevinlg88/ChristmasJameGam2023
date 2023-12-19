using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskInteractable : MonoBehaviour, IInteractable
{
    [Header("Task Settings")]
    public ResourceType inputType;
    public ResourceType outputType;
    public float waitTime;

    bool canInteract = false;


//#### MonoBehaviour Methods ####

    void OnTriggerEnter(Collider other)
    {
        canInteract = true;
    }

    void OnTriggerExit(Collider other)
    {
        canInteract = false;
    }

//#### Interact Methods ####
    public void Interact()
    {
        if(canInteract)
            Debug.Log("Interagiu");
    }

    //Import UniTask Package in the project
    //TODO: wait for wait time until finish the Task


}
