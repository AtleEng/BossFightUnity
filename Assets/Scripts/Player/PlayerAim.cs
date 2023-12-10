using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] Transform weaponHand;
    Camera cam;
    private void Start()
    {
        cam = Camera.main;
    }
    private void Update()
    {
        RotateHand();
        Flip();
    }
    void RotateHand()
    {
        Vector2 direction = cam.ScreenToWorldPoint(Input.mousePosition) - weaponHand.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //omvandlar en vinkel till rotation
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //sätter rotationen på vapnet
        weaponHand.rotation = rotation;
    }
    void Flip()
    {
        Vector2 v = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        if (v.x > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            weaponHand.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (v.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            weaponHand.localScale = new Vector3(-1f, -1f, 1f);
        }
    }
}
