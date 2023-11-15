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

    [SerializeField] float burstSpeed = 0;
    float _burstSpeed;

    [Header("Bullet stats")]
    [SerializeField] float bulletSpeed = 3;

    [SerializeField] float lifeTime = 3;

    [SerializeField] int dmg = 1;

    [SerializeField] int canPirceAmount = 0;
    #endregion

    [Header("Effects")]
    //[SerializeField] AudioSource gunSound;
    [SerializeField] Recoil recoil;
    void Update()
    {
        RotateGun();

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
    void RotateGun()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = rotation;
    }
    void Shoot()
    {
        recoil.AddRecoil();
        //gunSound.Play();
        SpawBullet();
    }
    void SpawBullet()
    {
        firePoint.Rotate(0, 0, Random.Range(-bulletSpread, bulletSpread));
        GameObject bulletClone = Instantiate(projectile, firePoint.position, firePoint.rotation);
        bulletClone.GetComponent<Bullet>().SetStats(bulletSpeed, lifeTime, dmg, canPirceAmount);

        firePoint.rotation = transform.rotation;
    }
}

public interface IWeaponType
{
    void Attack(Vector3 shootingDir);
}
