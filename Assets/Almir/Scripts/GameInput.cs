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
    private void Awake() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Dances.performed += ctx => dance = ctx.ReadValueAsButton();
    }
    public Vector2 GetMovementVector(){
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();

        /*if(Input.GetAxis("Vertical") > 0f){
            inputVector.z = +1;
        }
        if(Input.GetAxis("Vertical") < 0f){
            inputVector.z = -1;
        }
        if(Input.GetAxis("Horizontal") > 0f){
            inputVector.x = +1;
        }
        if(Input.GetAxis("Horizontal") < 0f){
            inputVector.x = -1;
        }*/
        if(playerInputActions.Player.Run.IsPressed()){
            if(player.getDancing()){
                player.setDancing(false);
            }
            player.setSpeed(6f);
        } else { 
            player.setSpeed(2f); 
            }
        if(dance){
            if(player.getDancing()){
                player.setDancing(false);
            } else {
                player.setDancing(true);
            }
            dance = false;
        }

        inputVector = inputVector.normalized;

        return inputVector;
   }
}
