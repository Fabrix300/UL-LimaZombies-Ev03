using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public Transform followAt;
    public float distanceToFollow;
    public EnemySO data;
    public Animator mAnimator;

    /** variables para gestion de vida **/
    public Slider healthSlider;
    private int healthPoints;
    private int maxHealthPoints;
    /*****************************/

    private NavMeshAgent mNavMeshAgent;    

    private void Awake()
    {
        mNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        mNavMeshAgent.speed = data.speed;
        healthPoints = (int) data.health;
        maxHealthPoints = healthPoints;
        healthSlider.maxValue = maxHealthPoints;
        healthSlider.value = healthPoints;
        followAt = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        //mNavMeshAgent.destination = followAt.position;
        float distance = Vector3.Distance(transform.position, followAt.position);
        if (distance <= distanceToFollow)
        {
            mNavMeshAgent.isStopped = false;
            mNavMeshAgent.destination = followAt.position;
            mAnimator.SetTrigger("Walk");
        }
        else
        {
            mAnimator.SetTrigger("Stop");
            mNavMeshAgent.isStopped = true;
        }

    }

    public bool DamageEnemy(int damagePoints)
    {
        healthPoints -= damagePoints;
        healthSlider.value = healthPoints;
        if (healthPoints <= 0)
        {
            Destroy(gameObject);
            return true;
        }
        return false;
    }
}
