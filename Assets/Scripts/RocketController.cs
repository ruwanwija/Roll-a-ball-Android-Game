using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    Rigidbody rb;
    AudioSource source;
    public Transform[] fireboost;
    float mainThrust = 20.0f;
    float rotationThrust = 160.0f;
    float volume;
    float volumeControlValue = 0.02f;
    float minVolume = 0.0f;
    float maxVolume = 0.5f;
    [HideInInspector] public float maxGasLevel = 100;
    [HideInInspector] public float currentGasLevel;
    int gasReduction = 14;
    bool hasGas;
    bool boostingUp;
    bool boostingDown;
    bool rotateLeft;
    bool rotateRight;
    float time;
    float stayTimeWithoutGas = 2.0f;
    
    void Awake()
    {
        Time.timeScale = 1f;

        source = GetComponent<AudioSource>();
        source.volume = minVolume;
    }
    
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Obstacle"))
        {
            FindObjectOfType<SpawnManager>().SpawnEffect(0, transform.position);
            Destroy(this.gameObject);
            FindObjectOfType<UIManager>().GameOver();
        }

        if (col.gameObject.CompareTag("LandingPad"))
        {
            FindObjectOfType<UIManager>().LevelComplete();
            FindObjectOfType<SpawnManager>().SpawnEffect(1, col.transform.position);
            Destroy(rb);
        }
    }
    
    void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.CompareTag("Gas"))
        {
            currentGasLevel = maxGasLevel;
            Destroy(target.gameObject);
        }
    }
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentGasLevel = maxGasLevel;
        hasGas = true;
    }
    
    void Update()
    {
        if (currentGasLevel > 0)
        hasGas = true;
        else
        hasGas = false;

        if (currentGasLevel > maxGasLevel)
        currentGasLevel = maxGasLevel;
        else if (currentGasLevel < 0)
        currentGasLevel = 0;
        
        if (!FindObjectOfType<UIManager>().levelCompleted)
        {Rotate();}
        
        if (source.volume > maxVolume)
        source.volume = maxVolume;

        if (!hasGas)
        {
            time += Time.deltaTime;
            if (time >= stayTimeWithoutGas)
            FindObjectOfType<UIManager>().GameOver();
        }
        else
        time = 0;
    }
    
    void FixedUpdate()
    {
        if (hasGas)
        {
            source.enabled = true;
            
            if (!FindObjectOfType<UIManager>().levelCompleted)
            {
                if (boostingUp)
                {
                    rb.AddRelativeForce(Vector3.up * mainThrust); // Boost Rocket
                    ReduceGas();
                    ControlThrustSound(true);
                    ActivateThrustFire(true);
                }
                else
                {
                    ControlThrustSound(false);
                    ActivateThrustFire(false);
                }

                if (boostingDown)
                {
                    rb.AddRelativeForce(Vector3.down * mainThrust); // Boost Rocket
                    ReduceGas();
                    ControlThrustSound(true);
                    ActivateThrustFire(true);
                }
                else
                {
                    ControlThrustSound(false);
                    ActivateThrustFire(false);
                }
            }
        }

        else
        {
            source.enabled = false;
            ActivateThrustFire(false);
        }
    }
    
    public void ThrustUp(bool thrust)
    {
        if (thrust)
        boostingUp = true;
        else
        boostingUp = false;
    }

    public void ThrustDown(bool thrust)
    {
        if (thrust)
        boostingDown = true;
        else
        boostingDown = false;
    }
    
    void ActivateThrustFire(bool isActivate)
    {
        if (isActivate)
        {
            fireboost[0].gameObject.SetActive(true);
            fireboost[1].gameObject.SetActive(false);
        }

        else
        {
            fireboost[0].gameObject.SetActive(false);
            fireboost[1].gameObject.SetActive(true);
        }
    }
    
    void ControlThrustSound(bool canActivateSound)
    {
        // if volume is greater or equal than minVolume and volume is less or equal than maxVolume
        if (source.volume >= minVolume && source.volume <= maxVolume)
        {
            if (canActivateSound)
            {
                source.volume += volumeControlValue;
            }

            else
            {
                source.volume -= volumeControlValue;
            }
        }
    }
    
    void Rotate()
    {
        rb.freezeRotation = true; // take manual control of rotation

        if (rotateLeft && !rotateRight)
        transform.Rotate(Vector3.forward * rotationThrust * Time.deltaTime);
        else if (!rotateLeft && rotateRight)
        transform.Rotate(Vector3.back * rotationThrust * Time.deltaTime);

        rb.freezeRotation = false; // resume physics control of rotation
    }

    public void RotateLeft(bool on)
    {
        if (on)
        {
            rotateLeft = true;
            rotateRight = false;
        }
        else
        rotateLeft = false;
    }

    public void RotateRight(bool on)
    {
        if (on)
        {
            rotateLeft = false;
            rotateRight = true;
        }
        else
        rotateRight = false;
    }
    
    void ReduceGas()
    {
        currentGasLevel -= Time.deltaTime * gasReduction;
    }
}