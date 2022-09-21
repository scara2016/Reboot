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
    [SerializeField]
    private int queueSize=5;
    private bool moveToNextFlag = false;
    public bool MoveToNextFlag
    {
        get
        {
            return moveToNextFlag;
        }
    }
    List<Vector3> queuePositions;
    public List<Vector3> QueuePositions
    {
        get
        {
            return queuePositions;
        }
    }

    public List<CustomerMovement> customersInQueue;

    int takenPositions = 0;
    // Start is called before the first frame update
    void Start()
    {
        queuePositions = new List<Vector3>();
        uIManager = FindObjectOfType<UIManager>();
        Transform till = GameObject.FindGameObjectWithTag("Till").GetComponent<Transform>();
        Transform entrance = GameObject.FindGameObjectWithTag("Entrance").GetComponent<Transform>();
        for (int i = 0; i < queueSize; i++)
        {
            queuePositions.Add(Vector3.Lerp(till.position, entrance.position, (float)(i + 1) / (float)queueSize));
        }
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
        if (AvailablePosition())
        {
            float currentTime = (((uIManager.WakeUpTime - curveStartTimer) * 60) + (uIManager.Timer)) / 600;
            spawnTimer += Time.deltaTime * customerSpawnRate.Evaluate(currentTime);
            if (spawnTimer >= spawnRate)
            {
                spawnTimer = 0;
                GameObject spawnedCustomer = Instantiate(customerPrefab);
                spawnedCustomer.transform.SetParent(transform);
            }
        }
    }
    public bool AvailablePosition()
    {
        if (takenPositions >= queueSize)
            return false;
        else
            return true;
    }

    public Vector3 GetPosition(out int queueNumber, CustomerMovement customer)
    {
        Vector3 destination = queuePositions[takenPositions];
        takenPositions++;
        queueNumber = takenPositions-1;
        customersInQueue.Add(customer);
        return destination;
    }
    public void RemoveCustomer(CustomerMovement customer)
    {
        customersInQueue.Remove(customer);
        takenPositions--;
        for(int i = 0; i < customersInQueue.Count; i++)
        {
            customersInQueue[i].MoveCustomer(queuePositions[i]);
        }

    }
    
}
