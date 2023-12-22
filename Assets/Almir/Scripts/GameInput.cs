using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameInput : MonoBehaviour
{
    [SerializeField] private Player player;
    private PlayerInputActions playerInputActions;
    public bool runPressed;
    public bool dance;
    public bool itemHoldTrigger;
    private void Awake() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Dances.performed += ctx => dance = ctx.ReadValueAsButton();
        playerInputActions.Player.HoldItem.performed += ctx => itemHoldTrigger = ctx.ReadValueAsButton();
    }
    public Vector2 GetMovementVector(){
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();

        if(playerInputActions.Player.Run.IsPressed() && player.getGiftStack().Count == 0){
            if(player.getDancing()){
                player.setDancing(false);
            }
            player.setSpeed(6f);
        } else { 
            player.setSpeed(2f); 
            }
        if(dance && player.getGiftStack().Count == 0){
            if(player.getDancing()){
                player.setDancing(false);
            } else {
                player.setDancing(true);
            }
            dance = false;
        }
        if(itemHoldTrigger){
            if(player.getHolding() && !player.getItemOverlapping() && !player.getGiftOverlapping()){
                player.ThrowObject();
            } else if (player.getGiftStack().Count > 0 && !player.getHolding() && !player.getItemOverlapping() && !player.getGiftOverlapping()){
                player.ReleaseGift();
            }
            else if (!player.getHolding() && !player.getItemOverlapping() && !player.getGiftOverlapping()){
                itemHoldTrigger = false;
            }
        }

        inputVector = inputVector.normalized;

        return inputVector;
   }
    public bool getHoldItemTrigger(){
        return itemHoldTrigger;
    }

    public void setHoldItemTrigger(bool value){
        itemHoldTrigger = value;
    }
}
