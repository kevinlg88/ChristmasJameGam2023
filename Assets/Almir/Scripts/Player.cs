using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private bool dance;
    [SerializeField] private float speedR = 10f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private bool isHolding = false;
    [SerializeField] private List<GameObject> holdingStack = new List<GameObject> {};
    [SerializeField] private int idleState;
    private Animator animator;
    public GameObject playerRightHand;
    public GameObject pickedGameObject;
    private bool isObjectPickableOverlaping;
    private bool isGiftStackableOverlaping;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        idleState = holdingStack.Count;
        animator.SetFloat("idle", idleState);
        HandleMovement();
    }

    private void HandleMovement(){
        
        Vector2 movementVector = gameInput.GetMovementVector();
        Vector3 inputVector = new Vector3(movementVector.x, 0, movementVector.y);
        var vel = inputVector * speed;
        animator.SetFloat("speed", vel.magnitude);
        //bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * 2f , .5f, inputVector);
        //if(canMove){
        transform.position += vel * Time.deltaTime;
        transform.forward = Vector3.Slerp( transform.forward, inputVector, Time.deltaTime * speedR); 
        //}
    }

    public float getSpeed(){
        return speed;
    }

    public void setSpeed(float value){
        speed = value;
    }

    public bool getDancing(){
        return dance;
    }

    public void setHolding(bool value){
        isHolding = value;
    }

    public bool getHolding(){
        return isHolding;
    }

    public void setDancing(bool value){
        dance = value;
        animator.SetBool("dance", dance);
    }
    public bool getItemOverlapping(){
        return isObjectPickableOverlaping;
    }
    public bool getGiftOverlapping(){
        return isGiftStackableOverlaping;
    }

    public List<GameObject> getGiftStack(){
        return holdingStack;
    }
    private async UniTask pickItem(GameObject item){
        await UniTask.Delay(1200);
        item.transform.SetParent(playerRightHand.transform);
        item.transform.localPosition = new Vector3(-0.09729286f, 0.01733261f, 0.03796677f);
        item.GetComponent<Collider>().enabled = false;
        isObjectPickableOverlaping = false;
    }

    private async UniTask throwItem(){
        await UniTask.Delay(1200);
        pickedGameObject.transform.parent = null;
        pickedGameObject.GetComponent<Collider>().enabled = true;
        isHolding = false;
        //item.transform.SetParent(playerRightHand.transform);
        //item.transform.localPosition = new Vector3(-0.09729286f, 0.01733261f, 0.03796677f);
    }
    private async void OnTriggerStay(Collider other) {
        if(other.CompareTag("pickableObject")){
            isObjectPickableOverlaping = true;
            if(gameInput.getHoldItemTrigger()){
                if(isHolding){
                    pickedGameObject.transform.parent = null;
                    animator.SetTrigger("hold");
                    gameInput.setHoldItemTrigger(false);
                    await pickItem(other.gameObject);
                    pickedGameObject.GetComponent<Collider>().enabled = true;
                    pickedGameObject = other.gameObject;
                } else {
                    if(holdingStack.Count > 0){
                        ReleaseGift();
                    }
                    isHolding = true;
                    animator.SetTrigger("hold");
                    gameInput.setHoldItemTrigger(false);
                    await pickItem(other.gameObject);
                    pickedGameObject = other.gameObject;
                }
            }
        }
        if(other.CompareTag("giftStack")){
            isGiftStackableOverlaping = true;
            if(gameInput.getHoldItemTrigger()){
                if(isHolding){
                    pickedGameObject.transform.parent = null;
                    pickedGameObject.GetComponent<Collider>().enabled = true;
                    isHolding = false;
                }
                other.gameObject.transform.SetParent(playerRightHand.transform);
                holdingStack.Add(other.gameObject);
                other.gameObject.transform.localPosition = new Vector3(-0.2047357f , 0.2336554f, 0.2875931f * (holdingStack.Count * holdingStack.Count));
                other.gameObject.GetComponent<Collider>().enabled = false;
                other.gameObject.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                other.gameObject.transform.localEulerAngles = new Vector3(-47.174f,17.448f,-157.837f);
                //other.gameObject.transform.Rotate(new Vector3(-63.831f,-28.512f,-110.564f));
                isGiftStackableOverlaping = false;
                gameInput.setHoldItemTrigger(false);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("pickableObject")){
            isObjectPickableOverlaping = false;
        }
        if(other.CompareTag("giftStack")){
            isGiftStackableOverlaping = false;
        }
    }

    public async void ThrowObject(){
        animator.SetTrigger("throw");
        gameInput.setHoldItemTrigger(false);
        await throwItem();
    }

    public void ReleaseGift(){
        foreach (GameObject gift in holdingStack)
        {
            gift.transform.parent = null;
            gift.GetComponent<Collider>().enabled = true;
            gift.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        holdingStack.Clear();
    }
}
