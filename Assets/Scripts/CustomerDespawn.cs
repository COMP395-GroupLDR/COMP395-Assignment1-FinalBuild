/*  Filename:           CustomerDespawn.cs
 *  Author:             Han Bi
 *  Last Update:        February 28, 2023
 *  Description:        Simple script to destory all customer objects that trigger the collider
 *  Revision History:   February 28, 2023  Initial script (Han)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CustomerDespawn : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CustomerController>() != null)
        {
            Destroy(other.gameObject);
        }
    }
}
