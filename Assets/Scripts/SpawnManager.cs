using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    int numberOfAstroids = 6;
    [SerializeField] GameObject astroid;
    [SerializeField] Transform[] effects;
    
    void Start()
    {
        for (int i = 0; i < numberOfAstroids; i++)
        {
            GameObject obj = Instantiate(astroid, transform.position, Quaternion.identity);
            obj.transform.SetParent(transform);
        }
    }

    public void SpawnEffect(int effectIndex, Vector3 pos)
    {
        Instantiate(effects[effectIndex], pos, Quaternion.identity);

        if (effectIndex == 0) // explosion effect
        FindObjectOfType<AudioManager>().Play("Explosion");

        if (effectIndex == 1) // win effect
        FindObjectOfType<AudioManager>().Play("LevelComplete");
    }
}