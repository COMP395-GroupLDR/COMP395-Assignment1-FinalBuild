/*  Filename:           ArrivalProcess.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        February 25, 2023
 *  Description:        Generate, calculate, and record customer arrival
 *  Revision History:   February 25, 2023 (Yuk Yee Wong): Initial script.
 *  Feb 28, 2023        Updated customer prefab to GameObject (from CharacterController)
 */

using Meta.Numerics.Statistics.Distributions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrivalProcess : MonoBehaviour
{
    [SerializeField] private float interArrivalTimeInMinutes = 5f;
    [SerializeField] private GameObject customerPrefab;
    [SerializeField] private Transform customerContainer;
    [SerializeField] private bool generateArrivals = true;

    // Auto loaded at start
    // external
    private Transform spawnPointTransform;
    private Text arrivalCountLabel;
    [SerializeField] private Text meanArrivalTimeLabel;

    private int arrivalCount = 0;
    private List<float> arrivalRecord = new List<float>();
    private float sum = 0;

    // Start is called before the first frame update
    void Start()
    {
        spawnPointTransform = GameObject.FindGameObjectWithTag("CustomerSpawnPoint").transform;

        // Register callback
        InterarrivalTimeSlider interarrivalTimeSlider = FindObjectOfType<InterarrivalTimeSlider>();
        if (interarrivalTimeSlider != null )
        {
            interarrivalTimeSlider.OnValueChangeCallback = (value) => { interArrivalTimeInMinutes = value; };
            interarrivalTimeSlider.value = interArrivalTimeInMinutes;
        }

        // Find arrival count label in current scene
        GameObject arrivalCountObj = GameObject.FindGameObjectWithTag("ArrivalCountLabel");
        if (arrivalCountObj != null )
        {
            arrivalCountLabel = arrivalCountObj.GetComponent<Text>();
        }

        // Find mean arrival time label in current scene
        GameObject meanArrivalTimeObj = GameObject.FindGameObjectWithTag("MeanArrivalTimeLabel");
        if (meanArrivalTimeObj != null)
        {
            meanArrivalTimeLabel = meanArrivalTimeObj.GetComponent<Text>();
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
            float nextArrivalInterval = GetArrivalInterval();
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
                meanArrivalTimeLabel.text = (sum / arrivalCount).ToString("0.00") + " sec";
            }
        }
    }

    private float GetArrivalInterval()
    {
        NormalDistribution n = new NormalDistribution(interArrivalTimeInMinutes * 60f, 1);
        return (float)n.InverseLeftProbability(Random.value);
    }
}
