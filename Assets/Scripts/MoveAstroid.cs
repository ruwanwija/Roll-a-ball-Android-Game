using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAstroid : MonoBehaviour
{
    float moveSpeed;
    float minMoveSpeed = 4.0f;
    float maxMoveSpeed = 8.0f;
    float rotateSpeed = 100.0f;

    Vector3 targetPos;
    float distance;
    float minDistance = 0.2f;

    float minYpos = 22.0f;
    float maxYpos = 42.0f;
    float xpos = 142.0f;
    
    float scale;
    float minScale = 1.0f;
    float maxScale = 4.0f;
    
    void Start()
    {
        transform.position = new Vector3(Random.Range(-xpos, xpos), Random.Range(minYpos, maxYpos), 0);
        GetRandomPosition();
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed); // get random move speed

        scale = Random.Range(minScale, maxScale);
        transform.localScale = new Vector3(scale, scale, scale); // set random scale
    }
    
    void Update()
    {
        transform.Rotate(Vector3.right * rotateSpeed * Time.deltaTime); // rotate

        distance = Vector3.Distance(transform.position, targetPos); // calculate distance

        if (distance > minDistance)
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        else if (distance <= minDistance)
        GetRandomPosition();
    }

    void GetRandomPosition()
    {
        targetPos = new Vector3(Random.Range(-xpos, xpos), Random.Range(minYpos, maxYpos), 0); // get target position
    }
}