using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float _bulletSpeed;
    float _lifeTime;
    int _dmg;
    int _canPirceAmount;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * _bulletSpeed * Time.deltaTime);
    }
    public void SetStats(float bulletSpeed, float lifeTime, int dmg, int canPirceAmount)
    {
        _bulletSpeed = bulletSpeed;
        _lifeTime = lifeTime;
        _dmg = dmg;
        _canPirceAmount = canPirceAmount;

        Invoke("DestroyProjectile", _lifeTime);
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
