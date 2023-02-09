using UnityEngine;



namespace PUBGFAKE.Controller.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovementBehaviour : MonoBehaviour
    {
        [Header("Movement Settings")]
        CharacterController characterController;
        public float movementSpeed = 3f;
        Vector3 movementDirection;
        Vector3 playerVelocity = Vector3.zero;
        [Space]


        [Header("Jump Settings")]
        public LayerMask whatIsGround;
        public Transform checkGroundPosition;
        public float radiusCheckGround = 0.1f;
        public float gravityValue = -30f;
        public float jumpHeight = 2.0f;


        [Header("Rotate Settings")]
        public Transform targetCamera;
        public float rotateSpeed = 100f;
        public float lookSpeed = 10f;


        Vector2 mouseDelta = Vector2.zero;
        float xRotation = 0f;




        bool isGrounded = true;
        bool jump = false;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
        }

        public void UpdateMovementData(Vector3 newMovementDirection)
        {
            movementDirection = newMovementDirection;
            movementDirection.Normalize();
        }
        public void UpdateJumpData(bool isJumping)
        {
            jump = isJumping;
        }
        public void UpdateRotateData(Vector2 mouseDelta)
        {
            this.mouseDelta = mouseDelta;
        }


        private void Update()
        {
            CheckGround();
            Jump();
            MoveThePlayer();
            RotatePlayer();
        }

        void CheckGround()
        {
            isGrounded = Physics.CheckSphere(checkGroundPosition.position, radiusCheckGround, whatIsGround);
        }


        void MoveThePlayer()
        {
            Vector3 targetMove = transform.right * movementDirection.x + transform.forward * movementDirection.z;
            // Move on X - Z
            characterController.Move(targetMove * movementSpeed * Time.deltaTime);
        }

        private void Jump()
        {
            if (isGrounded)
            {
                // stop our velocity dropping infinitely when grounded
                if (playerVelocity.y <= 0.0f)
                {
                    playerVelocity.y = -0.2f;
                }

                // Jump
                if (jump)
                {
                    // the square root of H * -2 * G = how much velocity needed to reach desired height
                    playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityValue);
                    PlayerAnimationBehaviour.instance.UpdateJumpAnimation(true);
                }
            }
            else
            {
                jump = false;
                PlayerAnimationBehaviour.instance.UpdateJumpAnimation(false);
            }
            if (!isGrounded)
            {
                playerVelocity.y += gravityValue * Time.deltaTime;
            }
            characterController.Move(playerVelocity * Time.deltaTime);
        }
        void RotatePlayer()
        {
            transform.Rotate(Vector3.up * rotateSpeed * mouseDelta.x * Time.deltaTime,Space.World);
            xRotation -= mouseDelta.y * lookSpeed * Time.deltaTime;
            xRotation = Mathf.Clamp(xRotation, -45, 45);
            targetCamera.localRotation = Quaternion.Euler(xRotation, 0, 0);
        }

        public void ApplyRecoilRotation(float minRecoil, float maxRecoil)
        {
            float recoilDistance = Random.Range(minRecoil, maxRecoil);

            xRotation -= recoilDistance;

            xRotation = Mathf.Clamp(xRotation, -45, 45);
        }
    }

}

