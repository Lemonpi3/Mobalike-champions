using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.AI;

public class CharecterAI : MonoBehaviour
{
    public Transform target;
    
    public PlayerAIstate aiState = PlayerAIstate.Idle;
    
    float moveSpeed;
    float rotateSpeedMovement = 0.075f;
    float rotateVelocity;
    float nextAttack = 0;

    float attackRange;
    TypeOfAttack typeOfAttack;
    CharecterStats charecterStats;

    NavMeshAgent agent;
    public enum PlayerAIstate { Idle, Moveing, Attacking , Interacting}

    // Start is called before the first frame update
    void Start()
    {
        charecterStats = GetComponent<CharecterStats>();
        moveSpeed = charecterStats.moveSpeed;
        attackRange = charecterStats.attackRange;
        typeOfAttack = charecterStats.typeOfAttack;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit _hit;
            if (Physics.Raycast(_ray, out _hit, 100))
            {
                if (_hit.collider.tag == "Ground")
                {
                    target = null;
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.collider.tag == "Ground")
                {
                    aiState = PlayerAIstate.Moveing;
                    Move(hit.point);
                }

                if (hit.collider.tag == "Interactable")
                {
                    target = hit.collider.transform;
                    aiState = PlayerAIstate.Interacting;
                }

                if (hit.collider.gameObject.layer != LayerMask.NameToLayer(charecterStats.team) && hit.collider.gameObject.layer != LayerMask.NameToLayer("Friendly") && hit.collider.tag != "Ground")
                {
                    target = hit.collider.transform;
                    aiState = PlayerAIstate.Attacking;
                }
                
            }
        }

        AIStateMachine();
    }


    public void AIStateMachine(){
        switch (aiState)    
        {
            case PlayerAIstate.Idle:
                break;
            case PlayerAIstate.Moveing:
                agent.isStopped = false;
                if(agent.remainingDistance <= 0)
                {
                    aiState = PlayerAIstate.Idle;
                }
                break;
            case PlayerAIstate.Attacking:
                if (target != null)
                {
                    agent.SetDestination(target.transform.position);

                    if (DistanceToTarget(target.transform.position) <= charecterStats.attackRange)
                    {
                        agent.isStopped = true;
                        CharecterStats targetStats = target.GetComponent<CharecterStats>();
                        FaceTarget();
                        Attack(targetStats);
                    }
                }
                else aiState = PlayerAIstate.Idle;
                break;
            case PlayerAIstate.Interacting:
                if (target != null)
                {
                    Move(target.position);
                    FaceTarget();
                    
                    if (DistanceToTarget(target.position) <= 2)
                    {
                        Debug.Log("Interacted with: "+ target.gameObject.name);
                        agent.isStopped = true;
                        aiState = PlayerAIstate.Idle;
                    }
                }
                break;
        }
    }

    public void Move(Vector3 destination){
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
        {
            agent.SetDestination(hit.point);

            Quaternion rotationToLookAt = Quaternion.LookRotation(hit.point - transform.position);
            float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.y, ref rotateVelocity, rotateSpeedMovement * Time.deltaTime);
            transform.eulerAngles = new Vector3(0, rotationY, 0);
        }
    }

    private float DistanceToTarget(Vector3 targetPosition)
    {
        return Vector3.Distance(transform.position, targetPosition);
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        if(direction!=Vector3.zero) 
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    public void Attack(CharecterStats targetStats){
        if (DistanceToTarget(target.transform.position) > charecterStats.attackRange || target ==null )
        {
            aiState = PlayerAIstate.Idle;
        }
        if (Time.time > nextAttack)
        {
            if (charecterStats.typeOfAttack == TypeOfAttack.Melee)
            {
                MeleeAtack(targetStats);
            }
            if (charecterStats.typeOfAttack == TypeOfAttack.Range)
            {
                RangedAttack(targetStats);
            }
        }
    }

    private void MeleeAtack(CharecterStats targetStats)
    {
        nextAttack = Time.time + charecterStats.attackSpeed;
        targetStats.TakeDamage((int)charecterStats.attackDamage);
    }

    private void RangedAttack(CharecterStats targetStats)
    {
        nextAttack = Time.time + charecterStats.attackSpeed;
        GameObject proyectile = Instantiate(charecterStats.autoAttackproyectile, transform.position, Quaternion.identity);
    }
}
