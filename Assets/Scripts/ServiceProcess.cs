/*  Filename:           ServiceProcess.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        February 25, 2023
 *  Description:        Manage, calculate, and record customer service
 *  Revision History:   February 25, 2023 (Yuk Yee Wong): Initial script.
 *  February 28, 2023   Added ServiceCustomer Coroutine, triggerbox behaviour and state properties (Han)
 */

using Meta.Numerics.Statistics.Distributions;
using System.Collections;
using System.Linq;
using Unity.Collections;
using UnityEngine;

public class ServiceProcess : MonoBehaviour
{
    [SerializeField] private float MeanServiceTimeInMinutes = 3f;

    [SerializeField]
    private bool isBusy;


    private void Start()
    {
        isBusy = false;
    }

    private float GetServiceTime()
    {
        NormalDistribution n = new NormalDistribution(MeanServiceTimeInMinutes * 60f, 1);
        return (float)n.InverseLeftProbability(Random.value);
    }


    private IEnumerator ServiceCustomer(CustomerController customer)
    {
        customer.ChangeCustomerState(CustomerController.CustomerState.Servicing);
        yield return new WaitForSeconds(GetServiceTime());
        customer.ChangeCustomerState(CustomerController.CustomerState.Exit);
        isBusy = false;
    }

    public bool IsBusy()
    {
        return isBusy;
    }

    public void setBusy() 
    {
        isBusy = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CustomerController>() != null) {
            
            
            StartCoroutine(ServiceCustomer(other.GetComponent<CustomerController>()));
        }
    }




}
