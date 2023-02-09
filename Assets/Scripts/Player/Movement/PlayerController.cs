using UnityEngine;
using UnityEngine.InputSystem;


namespace PUBGFAKE.Controller.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Sub Behaviours")]
        public PlayerMovementBehaviour playerMovementBehaviour;
        public PlayerAnimationBehaviour playerAnimationBehaviour;
        public PlayerShoot playerShoot;

        [Header("Input Settings")]
        public PlayerInput playerInput;
        private Vector3 rawInputMovement;
        private Vector2 mouseDelta;
        private bool isShooting = false;




        //INPUT SYSTEM ACTION METHODS --------------

        //This is called from PlayerInput; when a joystick or arrow keys has been pushed.
        //It stores the input Vector as a Vector3 to then be used by the smoothing function.

        public void OnMovement(InputAction.CallbackContext value)
        {
            Vector2 inputMovement = value.ReadValue<Vector2>();
            rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
        }
        public void OnJump(InputAction.CallbackContext value)
        {
            if (value.started) playerMovementBehaviour.UpdateJumpData(true);
        }
        public void OnRotate(InputAction.CallbackContext value)
        {
            Vector2 lookDirect = value.ReadValue<Vector2>();
            lookDirect.Normalize();
            playerMovementBehaviour.UpdateRotateData(lookDirect);
        }
        public void OnShoot(InputAction.CallbackContext value)
        {
            if (value.phase != InputActionPhase.Canceled)
            {
                isShooting = true;
            }
            else isShooting = false;
        }
        public void Reload(InputAction.CallbackContext value)
        {
            
            if(value.phase == InputActionPhase.Started)
                playerShoot.Reload();
        }
        public void Scope(InputAction.CallbackContext value)
        {
            if (value.phase == InputActionPhase.Started)
            {
                playerShoot.Scope();
            }
        }
        public void SwitchWeapon(InputAction.CallbackContext value)
        {
            if (value.phase == InputActionPhase.Started)
            {
                playerShoot.SelectWeapon(int.Parse(value.control.name) - 1);
            }
        }

        void UpdateMovement()
        {
            playerMovementBehaviour.UpdateMovementData(rawInputMovement);


        }
        void UpdateRotate()
        {
            playerMovementBehaviour.UpdateRotateData(mouseDelta);
        }
        void UpdateShoot()
        {
            playerShoot.UpdateShooting(isShooting);
        }
        
        private void Update()
        {
            UpdateMovement();
            UpdateRotate();
            playerAnimationBehaviour.UpdateMoveVelocityAnimation(rawInputMovement);
            UpdateShoot();
        }
    }

}
