using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tillScript : MonoBehaviour
{
    PlayerMovement player;
    CustomerMovement currentCustomer;
    UIManager uIManager;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        uIManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void AddCustomer(CustomerMovement customer)
    {
        currentCustomer = customer;
    }

    public void RemoveCustomer()
    {
        currentCustomer = null;
    }
    public bool CustomerCheck(int color)
    {
        if (color == currentCustomer.want)
        {
            currentCustomer.OrderAccepted();
            uIManager.Score = 10;
            return true;
        }
        else
            return false;

    }
    


}
