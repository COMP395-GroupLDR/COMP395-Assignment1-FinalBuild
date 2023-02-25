/*  Filename:           ServiceProcess.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        February 25, 2023
 *  Description:        Manage, calculate, and record customer service
 *  Revision History:   February 25, 2023 (Yuk Yee Wong): Initial script.
 */

using Meta.Numerics.Statistics.Distributions;
using UnityEngine;

public class ServiceProcess : MonoBehaviour
{
    [SerializeField] private float MeanServiceTimeInMinutes = 3f;

    private float GetServiceTime()
    {
        NormalDistribution n = new NormalDistribution(MeanServiceTimeInMinutes * 60f, 1);
        return (float)n.InverseLeftProbability(Random.value);
    }
}
