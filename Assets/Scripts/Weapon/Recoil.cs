using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    Vector3 currentPos, targetPos;

    [SerializeField]
    float snappiness, returnAmount, recoil;

    void Update()
    {
        Kickback();
    }
    public void AddRecoil()
    {
        // Calculate the recoil vector in local space
        Vector3 localRecoil = Quaternion.Inverse(transform.parent.rotation) * (-transform.parent.right * recoil);

        // Apply recoil in local space
        targetPos += localRecoil;
    }
    void Kickback()
    {
        targetPos = Vector3.Lerp(targetPos, Vector3.zero, Time.deltaTime * returnAmount);
        currentPos = Vector3.Lerp(currentPos, targetPos, Time.fixedDeltaTime * snappiness);
        transform.localPosition = currentPos;
    }
}
