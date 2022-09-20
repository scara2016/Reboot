using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerMovement : MonoBehaviour
{
    NavMeshAgent navmeshAgent;
    Transform till;
    Transform waitingPos;
    Transform returnPos;
    Transform[] waitingLine;
    enum CustomerState
    {
        movingToTill,
        ordering,
        waiting,
        returning
    }
    private CustomerState customerState;
    // Start is called before the first frame update
    void Start()
    {
        navmeshAgent = GetComponent<NavMeshAgent>();
        customerState = CustomerState.movingToTill;
    }

    // Update is called once per frame
    void Update()
    {
        switch (customerState)
        {
            case CustomerState.movingToTill:
                break;
            case CustomerState.ordering:
                break;
            case CustomerState.waiting:
                break;
            case CustomerState.returning:
                break;
        }
    }
}
