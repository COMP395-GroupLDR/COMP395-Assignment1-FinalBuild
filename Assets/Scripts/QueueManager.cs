/*  Filename:           QueueManager.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        February 25, 2023
 *  Description:        Manage the queue and position
 *  Revision History:   February 25, 2023 (Yuk Yee Wong): Initial script.
 */

using System.Collections.Generic;
using UnityEngine;

public class QueueManager : MonoBehaviour
{
    [SerializeField] private Vector3 distance = new Vector3(-1f, 0f, 0f);
    
    private Queue<CustomerController> queue = new Queue<CustomerController>();
    private Vector3 headOfQueuePoint;

    // Start is called before the first frame update
    void Start()
    {
        headOfQueuePoint = GameObject.FindGameObjectWithTag("HeadOfQueuePoint").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Push(CustomerController controller, out Vector3 queuePosition)
    {
        queuePosition = headOfQueuePoint + distance * queue.Count;
        queue.Enqueue(controller);
    }
}
