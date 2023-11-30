using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    #region varibles
    [Header("Components")]
    [SerializeField] GameObject projectile;
    [SerializeField] Transform firePoint;

    [Header("Weapon Stats")]
    [SerializeField] float timeBtwShoots = 0.1f;
    float _timeBtwShoots;

    [SerializeField] bool isWeaponAutomatic = true;

    [SerializeField] int amountOfBullets = 1;
    [Range(0, 180)]
    [SerializeField] float bulletSpread = 0;

    [SerializeField] float burstSpeed = 0;
    float _burstSpeed;

    [SerializeField] float recoil = 0.5f;

    [Header("Bullet stats")]
    [SerializeField] float bulletSpeed = 3;

    [SerializeField] float lifeTime = 3;

    [SerializeField] int dmg = 1;

    [SerializeField] int canPirceAmount = 0;
    #endregion

    void Update()
    {
        _timeBtwShoots -= Time.deltaTime;
        _burstSpeed -= Time.deltaTime;

        if (Input.GetMouseButton(0) && isWeaponAutomatic == true)
        {
            FireGun();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            FireGun();
        }
        RotateGun();
    }

    void RotateGun()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = rotation;
    }

    void FireGun()
    {
        if (_timeBtwShoots <= 0)
        {
            _timeBtwShoots = timeBtwShoots;
        }
    }
    void SpawBullet()
    {
        firePoint.Rotate(0, 0, Random.Range(-bulletSpread, bulletSpread));
        GameObject bulletClone = Instantiate(projectile, firePoint.position, firePoint.rotation);
        bulletClone.GetComponent<Bullet>().SetStats(gameObject, bulletSpeed, lifeTime, dmg, canPirceAmount);

        firePoint.rotation = transform.rotation;
    }

    public void Attack1()
    {
        print("Hej");
    }
}
