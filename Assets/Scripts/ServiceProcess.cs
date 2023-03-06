/*  Filename:           ServiceProcess.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        March 3, 2023
 *  Description:        Manage, calculate, and record customer service
 *  Revision History:   February 25, 2023 (Yuk Yee Wong): Initial script.
 *                      February 28, 2023 (Han Bi): Added ServiceCustomer Coroutine, triggerbox behaviour and state properties
 *                      March 3, 2023 (Yuk Yee Wong): Move calculation of arrival time to Utiltiies
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServiceProcess : MonoBehaviour
{
    [SerializeField] private float meanServiceTimeInMinutes = 3f;

    [SerializeField] private bool isBusy;

    private Text serviceCountLabel;
    private Text meanServiceTimeLabel;
    private CountDownTimer countDownTimer;

    private int serviceCount = 0;
    private List<float> arrivalRecord = new List<float>();
    private float sum = 0;

    private void Start()
    {
        isBusy = false;

        // Find service count label in current scene
        GameObject serviceCountObj = GameObject.FindGameObjectWithTag("ServiceCountLabel");
        if (serviceCountObj != null)
        {
            serviceCountLabel = serviceCountObj.GetComponent<Text>();
        }

        // Find mean service time label in current scene
        GameObject meanServiceTimeObj = GameObject.FindGameObjectWithTag("MeanServiceTimeLabel");
        if (meanServiceTimeObj != null)
        {
            
            meanServiceTimeLabel = meanServiceTimeObj.GetComponent<Text>();
        }

        // Find count down timer in current scene
        GameObject countDownTimerObj = GameObject.FindGameObjectWithTag("ServiceCountDownTimerLabel");
        if (countDownTimerObj != null)
        {
            countDownTimer = countDownTimerObj.GetComponent<CountDownTimer>();
            countDownTimerObj.SetActive(false);
        }
    }

    private IEnumerator ServiceCustomer(CustomerController customer)
    {
        customer.ChangeCustomerState(CustomerController.CustomerState.Servicing);
        float serviceTime = Utilities.GetServiceTimeInSeconds(serviceCount);
        if (countDownTimer != null)
        {
            countDownTimer.CountDown(serviceTime);
        }

        // Log
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(serviceTime);
            Debug.Log($"Service time of customer #{serviceCount} for {Utilities.ServiceDistribution} is {Utilities.GetFormattedTime(serviceTime)}");
        }

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
            meanServiceTimeLabel.text = Utilities.GetFormattedTime(sum / serviceCount);
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
