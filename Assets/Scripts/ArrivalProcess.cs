/*  Filename:           ArrivalProcess.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        March 1, 2023
 *  Description:        Generate, calculate, and record customer arrival
 *  Revision History:   February 25, 2023 (Yuk Yee Wong): Initial script.
 *                      February 28, 2023 (Han Bi) Updated customer prefab to GameObject (from CharacterController)
 *                      March 3, 2023 (Yuk Yee Wong): Move calculation of arrival time to Utiltiies
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrivalProcess : MonoBehaviour
{
    [Header("Generated")]
    [SerializeField] private GameObject customerPrefab;
    [SerializeField] private Transform customerContainer;
    [SerializeField] private bool generateArrivals = true;

    // Auto loaded at start
    // external
    private Transform spawnPointTransform;
    private Text arrivalCountLabel;
    private CountDownTimer countDownTimer;
    [SerializeField] private Text meanArrivalTimeLabel;

    private int arrivalCount = 0;
    private List<float> arrivalRecord = new List<float>();
    private float sum = 0;

    // Start is called before the first frame update
    void Start()
    {
        spawnPointTransform = GameObject.FindGameObjectWithTag("CustomerSpawnPoint").transform;

        // Find arrival count label in current scene
        GameObject arrivalCountObj = GameObject.FindGameObjectWithTag("ArrivalCountLabel");
        if (arrivalCountObj != null)
        {
            arrivalCountLabel = arrivalCountObj.GetComponent<Text>();
        }

        // Find mean arrival time label in current scene
        GameObject meanArrivalTimeObj = GameObject.FindGameObjectWithTag("MeanArrivalTimeLabel");
        if (meanArrivalTimeObj != null)
        {
            meanArrivalTimeLabel = meanArrivalTimeObj.GetComponent<Text>();
        }

        // Find next arrival time label in current scene
        GameObject nextArrivalTimeObj = GameObject.FindGameObjectWithTag("NextArrivalCountDownTimerLabel");
        if (nextArrivalTimeObj != null)
        {
            countDownTimer = nextArrivalTimeObj.GetComponent<CountDownTimer>();
        }
    }

    public void StartArrival()
    {
        StartCoroutine(GenerateArrivals());
    }

    private IEnumerator GenerateArrivals()
    {
        while (generateArrivals)
        {
            float nextArrivalInterval = Utilities.GetArrivalIntervalInSeconds(arrivalCount);
            if (nextArrivalInterval != Mathf.Infinity)
            {
                if (countDownTimer != null)
                {
                    countDownTimer.CountDown(nextArrivalInterval);
                }

                // Log
                {
                    Debug.Log($"Interarrival time of customer #{arrivalCount} for {Utilities.ArrivalDistribution} is {Utilities.GetFormattedTime(nextArrivalInterval)}");
                }

                yield return new WaitForSeconds(nextArrivalInterval);

                GameObject customerGO = Instantiate(customerPrefab, spawnPointTransform.position, Quaternion.identity, customerContainer);
                customerGO.GetComponent<CustomerController>().name = $"Customer #{arrivalCount}";
                customerGO.GetComponent<CustomerController>().customerIndex = arrivalCount;

                arrivalCount++;
                arrivalRecord.Add(nextArrivalInterval);
                sum += nextArrivalInterval;


                if (arrivalCountLabel != null)
                {
                    arrivalCountLabel.text = arrivalCount.ToString();
                }
                if (meanArrivalTimeLabel != null)
                {
                    meanArrivalTimeLabel.text = Utilities.GetFormattedTime(sum / arrivalCount);
                }
            }
            else
            {
                generateArrivals = false;
            }
        }
    }
}
