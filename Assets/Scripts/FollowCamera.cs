using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform rocket;
    float yPos;
    float zPos = -100.0f;
    float minYPos = 4.0f;
    float maxYPos = 40.0f;
    
    void LateUpdate()
    {
        if (rocket != null)
        {
            if (rocket.position.y > minYPos && rocket.position.y <= maxYPos)
            yPos = rocket.position.y;
            
            transform.position = new Vector3(rocket.position.x, yPos, zPos);
        }
        else return;
    }
}