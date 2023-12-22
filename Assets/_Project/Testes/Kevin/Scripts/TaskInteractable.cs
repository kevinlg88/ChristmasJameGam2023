using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    List<GameObject> itemsInside;
    public GameObject InputItem_Property
    {
        set
        {
            if(VerifyItemList(value))
                itemsInside.Add(value);
        }
    }
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
            WaitTimeAsync();
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

    bool VerifyItemList(GameObject item)
    {
        //if item type == inputType
        if(CheckRecipe()){}
        return false;
    }

    bool CheckRecipe()
    {
        return false;
    }

    async UniTask WaitTimeAsync()
    {
        isCrafting = true;
        await UniTask.Delay(waitTime);
        OutputItem();
    }

    void OutputItem()
    {
        isCrafting = false;
        itemsInside.Clear();
        //outputItem = outputPrefab;
    }

}
