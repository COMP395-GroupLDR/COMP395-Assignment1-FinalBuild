/*  Filename:           ServiceProcess.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        February 25, 2023
 *  Description:        Manage, calculate, and record customer service
 *  Revision History:   February 25, 2023 (Yuk Yee Wong): Initial script.
 *  February 28, 2023   Added ServiceCustomer Coroutine, triggerbox behaviour and state properties (Han)
 */

using Meta.Numerics.Statistics.Distributions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ServiceProcess : MonoBehaviour
{
    [SerializeField] private float meanServiceTimeInMinutes = 3f;

    [SerializeField] private bool isBusy;

    private Text serviceCountLabel;
    [SerializeField] private Text meanServiceTimeLabel;

    private int serviceCount = 0;
    private List<float> arrivalRecord = new List<float>();
    private float sum = 0;

    private void Start()
    {
        isBusy = false;

        // Register callback
        ServiceTimeSlider ServiceTimeSlider = FindObjectOfType<ServiceTimeSlider>();
        if (ServiceTimeSlider != null)
        {
            ServiceTimeSlider.OnValueChangeCallback = (value) => { meanServiceTimeInMinutes = value; };
            ServiceTimeSlider.value = meanServiceTimeInMinutes;
        }

        // Find arrival count label in current scene
        GameObject serviceCountObj = GameObject.FindGameObjectWithTag("ServiceCountLabel");
        if (serviceCountObj != null)
        {
            serviceCountLabel = serviceCountObj.GetComponent<Text>();
        }

        // Find mean arrival time label in current scene
        GameObject meanServiceTimeObj = GameObject.FindGameObjectWithTag("MeanServiceTimeLabel");
        if (meanServiceTimeObj != null)
        {
            meanServiceTimeLabel = meanServiceTimeObj.GetComponent<Text>();
        }
    }

    private float GetServiceTime()
    {
        NormalDistribution n = new NormalDistribution(meanServiceTimeInMinutes * 60f, 1);
        return (float)n.InverseLeftProbability(Random.value);
    }


    private IEnumerator ServiceCustomer(CustomerController customer)
    {
        customer.ChangeCustomerState(CustomerController.CustomerState.Servicing);
        float serviceTime = GetServiceTime();
        yield return new WaitForSeconds(serviceTime);

        customer.ChangeCustomerState(CustomerController.CustomerState.Exit);

        serviceCount++;
        arrivalRecord.Add(serviceTime);
        sum += serviceTime;


        if (serviceCountLabel != null)
        {
            serviceCountLabel.text = serviceCount.ToString();
        }
        if (meanServiceTimeLabel != null)
        {
            meanServiceTimeLabel.text = (sum / serviceCount).ToString("0.00") + " sec";
        }

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
