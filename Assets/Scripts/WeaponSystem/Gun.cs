using UnityEngine;
using PUBGFAKE.Controller.Player;


namespace PUBGFAKE.WeaponSystem.Gun
{
    public class Gun : MonoBehaviour
    {
        [Header("Data")]
        public GunData gunData;

        [Header("Shooting")]
        public GameObject bulletImpact;
        //public GameObject shootEffect;
        public Transform shootPoint;

        public Transform shootDirect;

        bool isReloading;
        float timeBetweenTwoShot;
        float timeSinceLastShot = 1;
        int currentBullet;

        private void Start()
        {
            Init();
        }
        void Init()
        {
            currentBullet = gunData.magSize;
       
            //shootDirect = GameObject.FindGameObjectWithTag("MainCamera").transform;
            isReloading = false;
            timeBetweenTwoShot = 60 / gunData.fireRate;
        }
        bool CanShoot()
        {
            return !isReloading && timeSinceLastShot > timeBetweenTwoShot && currentBullet > 0; 
        }

        private void Update()
        {
            timeSinceLastShot += Time.deltaTime;
        }
        public bool Shoot()
        {
            if (!CanShoot())
            {
                return false;
            }
            RaycastHit hit;
            if (Physics.Raycast(shootDirect.position, shootDirect.forward, out hit, gunData.maxDistance))
            {
                GameObject impactObject = Instantiate(bulletImpact, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactObject, 1f);
                hit.transform.GetComponent<Health>()?.ChangeHealth(-gunData.damage);
            }

            //AudioManager.Instance.PlayAudioAtPoint("Shoot", shootPoint.position);

            currentBullet--;
            timeSinceLastShot = 0;
            return true;
        }
        public void Reload()
        {
            if(currentBullet == gunData.magSize) return;
            isReloading = true;
            currentBullet = gunData.magSize;
            PlayerAnimationBehaviour.instance.UpdateReloadingAnimation(isReloading);
            //AudioManager.Instance.PlayAudioAtPoint("Reload",shootPoint.position);
            Invoke("EndReload", gunData.reloadTime);
        }

        public void Recoil(PlayerMovementBehaviour playerMovementBehaviour)
        {
            playerMovementBehaviour.ApplyRecoilRotation(gunData.minRecoil,gunData.maxRecoil);   
        }

        void EndReload()
        {
            isReloading = false;
            PlayerAnimationBehaviour.instance.UpdateReloadingAnimation(isReloading);
            //AudioManager.Instance.StopAudio("Reload");
        }

    }

}
