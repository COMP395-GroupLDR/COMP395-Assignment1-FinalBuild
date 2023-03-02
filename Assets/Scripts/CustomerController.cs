/*  Filename:           CustomerController.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        February 25, 2023
 *  Description:        Customer controller that uses nav mesh agent to move around and updates FSM state
 *  Revision History:   February 25, 2023 (Yuk Yee Wong): Initial script.
 *  February 28, 2023   Updated Enum values and added WalkToService, DoService, DoExit behaviours (Han)
 */

using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CustomerController : MonoBehaviour
{
    public enum CustomerState
    {
        None,
        Arrived,
        Queuing,
        WalkingToService,
        Servicing,
        Exit,
    }

    [HideInInspector] public int customerIndex;

    private float walkSpeed = 1.7f;
    private int idleArmsAnim = 5;
    private int idleLegsAnim = 5;
    private int walkArmsAnim = 1;
    private int walkLegsAnim = 1;
    private int serviceArmsAnim = 5;
    private int serviceLegsAnim = 22;

    // Auto loaded at start
    // internal
    private NavMeshAgent agent;
    private HumanModule humanModule;
    private Animator animator;
    // external
    private Vector3 despawnPoint;
    private Vector3 headOfQueuePoint;
    private Vector3 servicePoint;
    private QueueManager queueManager;

    [Header("Debug")]
    [SerializeField] private CustomerState customerState = CustomerState.None;
    [SerializeField] private Vector3 targetDestination;

    private bool inited = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        humanModule = GetComponentInChildren<HumanModule>();

        despawnPoint = GameObject.FindGameObjectWithTag("CustomerDespawnPoint").transform.position;
        headOfQueuePoint = GameObject.FindGameObjectWithTag("HeadOfQueuePoint").transform.position;
        servicePoint = GameObject.FindGameObjectWithTag("ServicePoint").transform.position;
        queueManager = GameObject.FindGameObjectWithTag("QueueManager").GetComponent<QueueManager>();

        humanModule.RandomAppearance();
        
        agent.speed = walkSpeed;

        ChangeCustomerState(CustomerState.Arrived);

        inited = true;
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (inited && agent != null)
        {
            if (Vector3.Distance(agent.destination, transform.position) < agent.stoppingDistance + 0.1f)
            {
                Idle();
            }
            else
            {
                Walk();
            }
        }
    }

    private void SetAnimator(int arms, int legs)
    {
        animator.SetInteger("arms", arms);
        animator.SetInteger("legs", legs);
    }

    private void Idle()
    {
        SetAnimator(idleArmsAnim, idleLegsAnim);
        agent.SetDestination(transform.position);
    }

    private void Walk()
    {
        SetAnimator(walkArmsAnim, walkLegsAnim);
    }

    private void Service()
    {
        SetAnimator(serviceArmsAnim, serviceLegsAnim);
        agent.SetDestination(transform.position);
    }

    public void ChangeCustomerState(CustomerState state)
    {
        customerState = state;
        FSMCustomer();
    }

    private void FSMCustomer()
    {
        switch (customerState)
        {
            case CustomerState.Arrived:
                DoArrived();
                break;
            case CustomerState.Queuing:
                DoQueuing();
                break;
            case CustomerState.WalkingToService:
                DoWalkToService();
                break;
            case CustomerState.Servicing:
                DoServicing();
                break;
            case CustomerState.Exit:
                DoExit();
                break;
            default:
                Debug.LogWarning($"{customerState} behaviour is not defined, please update ChangeCustomerState");
                break;
        }
    }

    private void DoArrived()
    {
        Debug.Log($"Customer #{customerIndex} arrived at {Time.timeAsDouble.ToString("0.00")}s");
        ChangeCustomerState(CustomerState.Queuing);
    }

    private void DoQueuing()
    {
        queueManager.Push(this, out targetDestination);
        Vector3 direction = (targetDestination - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(direction);
        agent.SetDestination(targetDestination);
        Walk();
    }

    private void DoWalkToService()
    {
        targetDestination = servicePoint; // for debug
        agent.SetDestination(targetDestination);
        Walk();
    }

    private void DoServicing()
    {
        targetDestination = servicePoint; // for debug
        agent.SetDestination(targetDestination);        
        Service();
    }

    private void DoExit()
    {
        targetDestination = despawnPoint; // for debug
        agent.SetDestination(targetDestination);
        Walk();
    }

    public void MoveForward(Vector3 offset)
    {
        targetDestination = targetDestination - offset; // for debug
        agent.SetDestination(targetDestination);
        Walk();
    }

}
