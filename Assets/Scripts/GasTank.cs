using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasTank : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(Vector3.forward * 100 * Time.deltaTime);
    }
}