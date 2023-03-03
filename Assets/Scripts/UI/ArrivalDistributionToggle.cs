/*  Filename:           ArrivalDistributionToggle.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        March 3, 2023
 *  Description:        
 *  Revision History:   March 3, 2023 (Yuk Yee Wong): Initial script.
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrivalDistributionToggle : Toggle
{
    [SerializeField] private Utilities.Distribution distribution;
    [SerializeField] private List<GameObject> enableObjects;

    protected override void Start()
    {
        base.Start();
        onValueChanged.AddListener(OnToggleValueChanged);
    }

    private void OnToggleValueChanged(bool value)
    {
        if (value)
        {
            Utilities.UpdateArrivalDistribution(distribution);
        }

        foreach (GameObject go in enableObjects)
        {
            go.SetActive(value);
        }
    }
}
