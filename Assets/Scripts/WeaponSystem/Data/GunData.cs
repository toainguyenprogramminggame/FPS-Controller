using UnityEngine;

namespace PUBGFAKE.WeaponSystem.Gun
{
    [CreateAssetMenu(fileName = "GunData", menuName = "Data/Gun/GunData", order = 1)]
    public class GunData : ScriptableObject
    {
        [Header("Info")]
        public ETypeGun typeGun;
        

        [Header("Shooting")]
        public float damage;
        public float maxDistance;

        [Header("Reloading")]
        public int magSize;
        public float fireRate;
        public float reloadTime;

        [Header("Recoil")]
        public float maxRecoil;
        public float minRecoil;
    }

    public enum ETypeGun
    {
        ShortGun,
        AkGun,
        SnipperGun
    }
}

