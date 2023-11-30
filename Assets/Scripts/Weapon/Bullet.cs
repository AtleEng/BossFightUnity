using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameObject _owner;
    float _bulletSpeed;
    float _lifeTime;
    int _dmg;
    int _canPeirceAmount;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * _bulletSpeed * Time.deltaTime);
    }
    public void SetStats(GameObject owner, float bulletSpeed, float lifeTime, int dmg, int canPeirceAmount)
    {
        _owner = owner;
        _bulletSpeed = bulletSpeed;
        _lifeTime = lifeTime;
        _dmg = dmg;
        _canPeirceAmount = canPeirceAmount;

        Invoke("DestroyProjectile", _lifeTime);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == _owner) { return; }

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
