using UnityEngine;


namespace PUBGFAKE.Controller.Player
{
    public class PlayerAnimationBehaviour : MonoBehaviour
    {
        public static PlayerAnimationBehaviour instance;

        Animator animator;
        public Vector3 withWeaponRotation;

        [Space(35)]
        public Transform mesh;
        bool isHoldingWeapon = true;

        private void Start()
        {
            if(instance == null) instance = this;

            animator = GetComponentInChildren<Animator>();

            SetRotateDefaultOfMesh();
        }

        public void UpdateMoveVelocityAnimation(Vector3 velocity)
        {
            animator.SetFloat("xVel",velocity.x);
            animator.SetFloat("yVel",velocity.z);

        }

        public void UpdateJumpAnimation(bool isJumping)
        {
            animator.SetBool("isJumping", isJumping);
        }

        public void UpdateReloadingAnimation(bool isReloading)
        {
            animator.SetBool("isReloading", isReloading);
        }

        void SetRotateDefaultOfMesh()
        {
            if (isHoldingWeapon)
                mesh.localRotation = Quaternion.Euler(withWeaponRotation);
            else mesh.localRotation = Quaternion.Euler(0, 0, 0);
        }

    }

}
