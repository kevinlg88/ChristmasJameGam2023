using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class TaskInteractable : MonoBehaviour, IInteractable
{
    [Header("Task Settings")]
    public ResourceType inputType;
    public int waitTime;

    [Header("Task Recipes")]
    public List<RecipeDict> recipes;

    [Header("Task State Settings")]

    public GameObject outputItem;

    public bool canTakeItem = false;
    public bool canInteract = false;
    public bool canCraft = true;
    public bool isCrafting = false;

    List<GameObject> itemsInside = new List<GameObject>();
    GameObject player;


//#### MonoBehaviour Methods ####

    void OnTriggerEnter(Collider other)
    {
        player = other.gameObject;
        canInteract = true;
    }

    void OnTriggerExit(Collider other)
    {
        player = null;
        canInteract = false;
    }

//#### Interactable Methods ####
    public void Interact()
    {
        if(canInteract && !canTakeItem && !isCrafting)
        {
            Debug.Log("Interagiu");
            PutItem();
            WaitTime();
        }
        if(canTakeItem)
        {
            GiveItem();
        }
    }
    public void GiveItem()
    {
        //Spawn Item
        //Put item in player hand
    }

    public List<GameObject> GetItems()
    {
        return itemsInside;
    }

    void PutItem()
    {
        //Get Item from the player hand
        //add item in list itemsInside
    }

    void VerifyItemList()
    {

    }

    UniTask WaitTime()
    {
        isCrafting = true;
        UniTask.Delay(waitTime);
        OutputItem();
        return UniTask.CompletedTask;
    }

    void OutputItem()
    {
        isCrafting = false;
        itemsInside.Clear();
        //outputItem = outputPrefab;
    }

}
