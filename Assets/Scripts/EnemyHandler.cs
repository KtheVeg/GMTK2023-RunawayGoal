using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHandler : MonoBehaviour
{
    [SerializeField]
    Animator anim;
    [SerializeField]
    bool isActive = false;
    [SerializeField]
    NavMeshAgent agent;
    [SerializeField]
    Transform player;
    [SerializeField]
    bool toggle = false;
    [SerializeField]
    GameFlow gameFlow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (toggle)
        {
            if (isActive)
            {
                Deactivate();
            } else {
                Activate();
            }
            toggle = false;
            return;
        }
        if (isActive)
        {
            agent.SetDestination(player.position);
        } else {
            agent.SetDestination(transform.position);
        }
    }
    public void Activate()
    {
        isActive = true;
        anim.Play("Walk");
    }
    public void Deactivate()
    {
        isActive = false;
        anim.Play("Idle");
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameFlow.GameOver();
        }
    }
}
