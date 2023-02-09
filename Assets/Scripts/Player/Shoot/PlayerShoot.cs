using UnityEngine;
using Cinemachine;
using PUBGFAKE.WeaponSystem.Gun;


namespace PUBGFAKE.Controller.Player
{
    public class PlayerShoot : MonoBehaviour
    {
        public Transform gunHolder;
        public CinemachineVirtualCamera virtualCam;
        public GameObject scope;

        PlayerMovementBehaviour playerMovementBehaviour;

        Gun currentGun;
        int selectedGun = 0;
        bool isShooting = false;
        bool isScoping = false;


        private void Start()
        {
            playerMovementBehaviour = GetComponent<PlayerMovementBehaviour>();
            
            SelectWeapon();
        }

        private void Update()
        {
            if (currentGun.gunData.typeGun != ETypeGun.SnipperGun)
            {
                isScoping = true;
                Scope();
            }
            if (isShooting) Shoot();
        }

        void SelectWeapon()
        {
            int i = 0;
            foreach (Transform t in gunHolder)
            {
                if (i == selectedGun)
                {
                    t.gameObject.SetActive(true);
                    currentGun = t.GetChild(0).GetComponent<Gun>();
                }
                else t.gameObject.SetActive(false);
                i++;
            }
        }

        void Shoot()
        {
            if(currentGun.Shoot())
            {
                currentGun.Recoil(playerMovementBehaviour);
            }
        }

        public void UpdateShooting(bool shoot)
        {
            isShooting = shoot;
        }
        public void Reload()
        {
            currentGun.Reload();
        }
        public void Scope()
        {
            isScoping = !isScoping;
            CinemachineVirtualCamera virtualCamera = virtualCam.GetComponent<CinemachineVirtualCamera>();
            if (isScoping)
            {
                virtualCamera.m_Lens.FieldOfView = 5;
                virtualCamera.m_Lens.NearClipPlane = 2.5f;
            }
            else
            {
                virtualCamera.m_Lens.FieldOfView = 75;
                virtualCamera.m_Lens.NearClipPlane = 0.001f;
            }
            scope.SetActive(isScoping);
        }
        public void SelectWeapon(int choice)
        {
            selectedGun = choice;
            SelectWeapon();
        }        
    }
}

