using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name=="Win") {
            Debug.Log("You Won!");
        }
    }
}
