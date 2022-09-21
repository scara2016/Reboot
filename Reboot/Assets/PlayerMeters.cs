using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class PlayerMeters : MonoBehaviour
{
    private PlayerMovement playerMovement;
    [SerializeField]
    private ParticleSystem blueSmokeSystem;
    [SerializeField]
    private ParticleSystem greenSmokeSystem;
    private float blueMeter=100;
    public float blueDepSpeed=1;
    private float greenMeter=100;
    public float greenDepSpeed=1;
    public float smokeRate;
    public float BlueMeter
    {
        get
        {
            return blueMeter;
        }
        set
        {
            blueMeter = value;
        }
    }
    public float GreenMeter
    {
        get
        {
            return greenMeter;
        }
        set
        {
            greenMeter = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        blueMeter -= Time.deltaTime * blueDepSpeed;
        greenMeter -= Time.deltaTime * greenDepSpeed;
        if (blueMeter <= 50)
        {
            playerMovement.speedModifier = 0.5f+(blueMeter / 100);
            blueSmokeSystem.Play();
            EmissionModule emission = blueSmokeSystem.emission; 
            emission.rateOverTime = new ParticleSystem.MinMaxCurve((1 - playerMovement.speedModifier)*smokeRate);
        }
        else
        {
            playerMovement.speedModifier = 1;
            blueSmokeSystem.Clear();
        }
        if (greenMeter <= 50)
        {
 //           playerMovement.speedModifier = 0.5f + (greenMeter / 100);
            greenSmokeSystem.Play();
            
            EmissionModule emission = greenSmokeSystem.emission;
            emission.rateOverTime = new ParticleSystem.MinMaxCurve((1 - playerMovement.speedModifier) * smokeRate);
        }
        else
        {
            playerMovement.speedModifier = 1;
            greenSmokeSystem.Clear();
        }
    }

}
