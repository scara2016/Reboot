using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerMovement : MonoBehaviour
{
    NavMeshAgent navmeshAgent;
    
    private Transform returnPos;
    CustomerGenerator customerGenerator;
    int queueNumber;
    BoxCollider collider;
    bool AtTillFlag=false;
    MeshRenderer meshRenderer;
    public int want;
    
    enum CustomerState
    {
        gettingTill,
        movingToTill,

        ordering,
        waiting,
        returning,
        movingNext
    }
    private CustomerState customerState;
    private bool orderAcceptedFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        returnPos = GameObject.FindGameObjectWithTag("Exit").GetComponent<Transform>();
        meshRenderer = GetComponent<MeshRenderer>();
        want = (int)Random.Range(0, 2.9f);
        if (want == 0)
        {
            meshRenderer.material.color = Color.blue;
        }
        else if(want == 1)
        {
            meshRenderer.material.color = Color.green;
        }
        else
        {
            meshRenderer.material.color = Color.black;
        }
        collider = GetComponent <BoxCollider>();
        customerGenerator = FindObjectOfType<CustomerGenerator>().GetComponent<CustomerGenerator>();
        navmeshAgent = GetComponent<NavMeshAgent>();
        customerState = CustomerState.gettingTill;
    }

    // Update is called once per frame
    void Update()
    {
        switch (customerState)
        {
            case CustomerState.gettingTill:
                navmeshAgent.SetDestination(customerGenerator.GetPosition(out queueNumber,this));
                customerState = CustomerState.movingToTill;
                
                break;
            case CustomerState.movingToTill:
                if (customerGenerator.MoveToNextFlag)
                {
               //     customerState = CustomerState.movingNext;
                }
                if (AtTillFlag)
                {
                    customerState = CustomerState.ordering;
                    break;
                }
                break;
            case CustomerState.movingNext:
                
                break;
            case CustomerState.ordering:
                if (orderAcceptedFlag) { 
                customerGenerator.RemoveCustomer(this);
                navmeshAgent.SetDestination(Vector3.zero);
                customerState = CustomerState.returning;
                    }
                break;
            case CustomerState.returning:
                navmeshAgent.SetDestination(returnPos.position);
                break;
        }
    }

    public void OrderAccepted()
    {
        orderAcceptedFlag = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Till")
        {
            AtTillFlag = true;
            other.transform.GetComponent<tillScript>().AddCustomer(this);
        }
        if(other.gameObject.tag == "Exit")
        {
            Destroy(this.gameObject);
        }
    }

    public void MoveCustomer(Vector3 destination)
    {
        navmeshAgent.SetDestination(destination);
    }
}
