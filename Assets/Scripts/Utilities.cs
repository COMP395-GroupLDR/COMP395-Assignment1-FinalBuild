/*  Filename:           Utilities.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        March 3, 2023
 *  Description:        Refractor from first build script by Han Bi
 *  Revision History:   March 3, 2023 (Yuk Yee Wong): Initial script.
 */

using Meta.Numerics.Statistics.Distributions;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Utilities : MonoBehaviour
{
    public enum Distribution
    {
        Constant,
        Random,
        Exponential,
        Normal,
        Observed
    }

    // Load from File
    public static Customer[] Customers;

    // Arrival
    public static Distribution ArrivalDistribution = Distribution.Observed;
    public static float MinInterarrivalTimeInMinutes = Mathf.Infinity; // calculated from the data
    public static float MaxInterarrivalTimeInMinutes = 0f; // calculated from the data
    public static float InterArrivalTimeInMinutes = 0f; // calculated from the data

    // Service
    public static Distribution ServiceDistribution = Distribution.Observed;
    public static float MinServiceTimeInMinutes = Mathf.Infinity; // calculated from the data
    public static float MaxServiceTimeInMinutes = 0f; // calculated from the data
    public static float MeanServiceTimeInMinutes = 0f; // calculated from the data

    void Start()
    {
        LoadData();

        UpdateArrivalDistribution(Distribution.Observed);
        UpdateServiceDistribution(Distribution.Observed);
    }

    public static void UpdateArrivalDistribution(Distribution distribution)
    {
        ArrivalDistribution = distribution;
        GameObject arrivalDistributionLabelObj = GameObject.FindGameObjectWithTag("ArrivalDistributionLabel");
        if (arrivalDistributionLabelObj != null)
        {
            arrivalDistributionLabelObj.GetComponent<Text>().text = $"Interarrival Time Distribution: {ArrivalDistribution}";
        }
    }

    public static void UpdateServiceDistribution(Distribution distribution)
    {
        ServiceDistribution = distribution;
        GameObject serviceDistributionLabelObj = GameObject.FindGameObjectWithTag("ServiceDistributionLabel");
        if (serviceDistributionLabelObj != null)
        {
            serviceDistributionLabelObj.GetComponent<Text>().text = $"Service Time Distribution: {ServiceDistribution}";
        }
    }

    public static float GetArrivalIntervalInSeconds(int index)
    {
        switch (ArrivalDistribution)
        {
            case Distribution.Constant:
                return InterArrivalTimeInMinutes * 60f;
            case Distribution.Random: 
                return UnityEngine.Random.Range(MinInterarrivalTimeInMinutes, MaxInterarrivalTimeInMinutes) * 60f;
            case Distribution.Exponential:
                return -Mathf.Log(1 - UnityEngine.Random.value) / (1 / (InterArrivalTimeInMinutes * 60f));
            case Distribution.Normal:
                NormalDistribution n = new NormalDistribution(InterArrivalTimeInMinutes * 60f, 1);
                return (float)n.InverseLeftProbability(UnityEngine.Random.value);
            case Distribution.Observed:
                if (Customers.Length > index)
                {
                    return Customers[index].interarrivalTime * 60f;
                }
                else
                {
                    return Mathf.Infinity;
                }
            default:
                return Mathf.Infinity;
        }

    }

    public static float GetServiceTimeInSeconds(int index)
    {
        switch (ServiceDistribution)
        {
            case Distribution.Constant:
                return MeanServiceTimeInMinutes * 60f;
            case Distribution.Random:
                return UnityEngine.Random.Range(MinServiceTimeInMinutes, MaxServiceTimeInMinutes) * 60f;
            case Distribution.Exponential:
                return -Mathf.Log(1 - UnityEngine.Random.value) / (1 / (MeanServiceTimeInMinutes * 60f));
            case Distribution.Normal:
                NormalDistribution n = new NormalDistribution(MeanServiceTimeInMinutes * 60f, 1);
                return (float)n.InverseLeftProbability(UnityEngine.Random.value);
            case Distribution.Observed:
                if (Customers == null)
                {
                    LoadData();
                }

                if (Customers.Length > index)
                {
                    return Customers[index].serviceTime * 60f;
                }
                else
                {
                    return Mathf.Infinity;
                }
            default:
                return Mathf.Infinity;
        }

    }

    private static void LoadData()
    {
        TextAsset data = (TextAsset)Resources.Load("CustomerData");

        char[] whitespace = new char[] { ' ', '\t', '\n' };
        string text = data.text;
        string[] values = data.text.Split(whitespace);

        Customers = new Customer[(values.Length) / 3];

        int counter = 0;
        float totalInterarrivalTime = 0f;
        float totalServiceTime = 0f;

        for (int i = 0; i < (values.Length - 3); i += 3)
        {
            Customers[counter] = new Customer(int.Parse(values[i]), float.Parse(values[i + 1]), float.Parse(values[i + 2]));

            if (MinInterarrivalTimeInMinutes > Customers[counter].interarrivalTime)
            {
                MinInterarrivalTimeInMinutes = Customers[counter].interarrivalTime;
            }
            if (MaxInterarrivalTimeInMinutes < Customers[counter].interarrivalTime)
            {
                MaxInterarrivalTimeInMinutes = Customers[counter].interarrivalTime;
            }
            if (MinServiceTimeInMinutes > Customers[counter].serviceTime)
            {
                MinServiceTimeInMinutes = Customers[counter].serviceTime;
            }
            if (MaxServiceTimeInMinutes < Customers[counter].serviceTime)
            {
                MaxServiceTimeInMinutes = Customers[counter].serviceTime;
            }
            totalInterarrivalTime += Customers[counter].interarrivalTime;
            totalServiceTime += Customers[counter].serviceTime;

            counter++;
        }

        InterArrivalTimeInMinutes = totalInterarrivalTime / Customers.Length;
        MeanServiceTimeInMinutes = totalServiceTime / Customers.Length;

        // Find observed mean arrival label in current scene
        GameObject observedMeanArrivalObj = GameObject.FindGameObjectWithTag("ObservedMeanArrival");
        if (observedMeanArrivalObj != null)
        {
            observedMeanArrivalObj.GetComponent<Text>().text = GetFormattedTime(InterArrivalTimeInMinutes * 60f);
        }

        // Find observed mean service label in current scene
        GameObject observedMeanServiceObj = GameObject.FindGameObjectWithTag("ObservedMeanService");
        if (observedMeanServiceObj != null)
        {
            observedMeanServiceObj.GetComponent<Text>().text = GetFormattedTime(MeanServiceTimeInMinutes * 60f);
        }

        Debug.Log($"MinArrivalTimeInMinutes {MinInterarrivalTimeInMinutes}");
        Debug.Log($"MaxArrivalTimeInMinutes {MaxInterarrivalTimeInMinutes}");
        Debug.Log($"InterArrivalTimeInMinutes {InterArrivalTimeInMinutes}");
        Debug.Log($"MinServiceTimeInMinutes {MinServiceTimeInMinutes}");
        Debug.Log($"MaxServiceTimeInMinutes {MaxServiceTimeInMinutes}");
        Debug.Log($"MeanServiceTimeInMinutes {MeanServiceTimeInMinutes}");
    }

    public static string GetFormattedTime(float timeInSeconds)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeInSeconds);
        return $"{timeSpan.Minutes:00}:{timeSpan.Seconds:00}";
    }
}
