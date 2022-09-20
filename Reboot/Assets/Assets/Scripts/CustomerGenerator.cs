using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject customerPrefab;
    public AnimationCurve customerSpawnRate;
    [SerializeField, Range(0.0f, 24.0f)]
    private float curveStartTimer;
    private UIManager uIManager;
    private bool startSpawning = false;
    private float spawnTimer=0;
    [SerializeField]
    private float spawnRate;
    // Start is called before the first frame update
    void Start()
    {
        uIManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (uIManager.WakeUpTime >= curveStartTimer)
        {
            startSpawning = true;
        }
        if (startSpawning)
        {
            Spawning();
        }
    }

    private void Spawning()
    {
        float currentTime = (((uIManager.WakeUpTime-curveStartTimer) * 60)+(uIManager.Timer))/600;
        //Debug.Log(currentTime);
        spawnTimer += Time.deltaTime * customerSpawnRate.Evaluate(currentTime);
        Debug.Log(spawnTimer);
        if (spawnTimer >= spawnRate)
        {
            spawnTimer = 0;
            GameObject spawnedCustomer = Instantiate(customerPrefab);
        }

    }
}
