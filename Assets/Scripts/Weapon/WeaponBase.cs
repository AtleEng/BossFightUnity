using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WeaponBase : MonoBehaviour
{
    #region varibles
    [Header("Components")]
    [SerializeField] GameObject projectile;
    [SerializeField] Transform firePoint;

    [Header("Weapon Stats")]
    [SerializeField] float fireRate = 0.1f;
    float timeBetweenShoots;

    [SerializeField] bool isWeaponAutomatic = true;

    [SerializeField] int amountOfBullets = 1;
    [Range(0, 180)]
    [SerializeField] float bulletSpread = 0;

    [Header("Bullet stats")]
    [SerializeField] float bulletSpeed = 3;

    [SerializeField] float lifeTime = 3;

    [SerializeField] int dmg = 1;

    [SerializeField] int canPeirceAmount = 0;
    #endregion

    [Header("Effects")]
    //[SerializeField] AudioSource gunSound;
    [SerializeField] Recoil recoil;
    [SerializeField] ParticleSystem muzzleFlash;

    Camera cam;
    void Start()
    {
        cam = Camera.main;
    }
    void Update()
    {
        timeBetweenShoots += Time.deltaTime;

        if (timeBetweenShoots >= fireRate)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
                timeBetweenShoots = 0;
            }
            else
            if (Input.GetButton("Fire1") && isWeaponAutomatic)
            {
                Shoot();
                timeBetweenShoots -= fireRate;
            }
        }
    }

    void Shoot()
    {
        recoil.AddRecoil();
        muzzleFlash.Play();
        //gunSound.Play();

        SpawBullet();
    }
    void SpawBullet()
    {
        firePoint.Rotate(0, 0, Random.Range(-bulletSpread, bulletSpread));
        GameObject bulletClone = Instantiate(projectile, firePoint.position, firePoint.rotation);
        bulletClone.GetComponent<Bullet>().SetStats(transform.parent.transform.parent.gameObject, bulletSpeed, lifeTime, dmg, canPeirceAmount);

        firePoint.rotation = transform.rotation;
    }
}