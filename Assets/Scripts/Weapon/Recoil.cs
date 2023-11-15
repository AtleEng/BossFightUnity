using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    Vector3 currentRot, currentPos, targetRot, targetPos, gunStartPos;
    Transform camTransform;

    public float snappiness, returnAmount, recoil;
    void Start()
    {
        gunStartPos = transform.localPosition;
        camTransform = Camera.main.transform;
    }
    void Update()
    {
        targetRot = Vector3.Lerp(targetRot, Vector3.zero, Time.deltaTime * returnAmount);
        currentRot = Vector3.Slerp(currentRot, targetRot, Time.fixedDeltaTime * snappiness);

        transform.localRotation = Quaternion.Euler(currentRot);
        camTransform.localRotation = Quaternion.Euler(currentRot);

        Kickback();
    }
    public void AddRecoil()
    {
        targetPos -= transform.right * recoil;
    }
    void Kickback()
    {
        targetPos = Vector3.Lerp(targetPos, gunStartPos, Time.deltaTime * returnAmount);
        currentPos = Vector3.Lerp(currentPos, targetPos, Time.fixedDeltaTime * snappiness);
        transform.localPosition = currentPos;
    }
}
