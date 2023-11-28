using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float _bulletSpeed;
    float _lifeTime;
    int _dmg;
    int _canPeirceAmount;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * _bulletSpeed * Time.deltaTime);
    }
    public void SetStats(float bulletSpeed, float lifeTime, int dmg, int canPeirceAmount)
    {
        _bulletSpeed = bulletSpeed;
        _lifeTime = lifeTime;
        _dmg = dmg;
        _canPeirceAmount = canPeirceAmount;

        Invoke("DestroyProjectile", _lifeTime);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable entity = other.gameObject.GetComponent<IDamageable>();
        if (entity != null)
        {
            entity.TakeDamage(_dmg);

            _canPeirceAmount--;
            if (_canPeirceAmount <= 0)
            {
                DestroyProjectile();
            }
        }
        else
        {
            DestroyProjectile();
        }
    }
    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
