/*  Filename:           QueueManager.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        March 3, 2023
 *  Description:        Manage the queue and position
 *  Revision History:   February 25, 2023 (Yuk Yee Wong): Initial script.
 *                      February 28, 2023 (Han Bi): Added CheckList behaviour to dequeue list for service
 *                      March 3 (Yuk Yee Wong): Move calculation of arrival time to Utiltiies
 */

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QueueManager : MonoBehaviour
{
    [SerializeField] private Vector3 distance = new Vector3(-1f, 0f, 0f);
    [SerializeField] private ServiceProcess service;
    [SerializeField] private Text meanQueueTime;
    public Queue<CustomerController> queue = new Queue<CustomerController>();
    
    private Vector3 headOfQueuePoint;
    private Text queuingCountLabel;
    private float totalQueueTime = 0f;
    public float queueCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        headOfQueuePoint = GameObject.FindGameObjectWithTag("HeadOfQueuePoint").transform.position;

        // Find queuing count label in current scene
        GameObject queuingCountObj = GameObject.FindGameObjectWithTag("QueuingCountLabel");
        if (queuingCountObj != null)
        {
            queuingCountLabel = queuingCountObj.GetComponent<Text>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckList();
    }

    public void AverageQueueTime(float queuetime)
    {
        totalQueueTime += queuetime;
        double minutes = Math.Ceiling(Math.Ceiling(totalQueueTime/queueCount) / 60);
        double seconds = Math.Ceiling(Math.Ceiling(totalQueueTime/queueCount) % 60);
        meanQueueTime.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    public void Push(CustomerController controller, out Vector3 queuePosition)
    {
        queuePosition = headOfQueuePoint + distance * queue.Count;
        queue.Enqueue(controller);
        queueCount += 1;
        UpdateQueuingCountLabel();
    }

    private void CheckList()
    {
        if(service.IsBusy() == false && queue.Count > 0)
        {
            CustomerController customer = queue.Dequeue();
            UpdateQueuingCountLabel();

            customer.ChangeCustomerState(CustomerController.CustomerState.WalkingToService);
            service.setBusy();

            foreach (CustomerController controller in queue) {
                controller.MoveForward(distance);
            }
        }
    }

    private void UpdateQueuingCountLabel()
    {
        if (queuingCountLabel != null)
        {
            queuingCountLabel.text = queue.Count.ToString();
        }
    }
    
}
