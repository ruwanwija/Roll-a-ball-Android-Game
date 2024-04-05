using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLight : MonoBehaviour
{
    [SerializeField] Transform rocket;
    float xPos;
    float xPosBorder = 122.0f;
    float maxXPos;
    float yPos = 30.0f;
    float zPos = -30.0f;
    
    void LateUpdate()
    {
        if (rocket != null)
        {
            if (rocket.position.x > -xPosBorder && rocket.position.x < xPosBorder)
            xPos = rocket.position.x;
            
            transform.position = new Vector3(xPos, yPos, zPos);
        }
        else return;
    }
}