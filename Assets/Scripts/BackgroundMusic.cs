using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic instance;
    AudioSource source;
    int currentScene;
    float minVolume = 0.4f;
    float maxVolume = 1.0f;
    
    void Awake()
    {
        if (instance == null)
        instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
    }
    
    void Start()
    {
        source = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;

        if (currentScene == 0)
        source.volume = maxVolume;
        else
        source.volume = minVolume;
    }
}