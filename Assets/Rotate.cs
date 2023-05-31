using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotateSpeed = 10f;

    public void Pressed()
    {
        transform.Rotate(0, 0, - Time.deltaTime * rotateSpeed, Space.Self);
    }
}
