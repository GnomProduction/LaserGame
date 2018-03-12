using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1Script : BaseEnemyScript
{
    private static BaseEnemyScript baseEnemyScript;
    private static Enemy1Script Instance;
    private NavMeshAgent Agent;
    private Rigidbody myRigid;

    private void Start()
    {
        Instance = this;
        Instance.Speed = 0.05f;
        Instance.Life = 120;
        Agent = GetComponent<NavMeshAgent>();
        Instance.movementSpeed = 5.0f;
        Instance.rotSpeed = 10.0f;
        baseEnemyScript = Instance.GetComponent<BaseEnemyScript>();
        myRigid = GetComponent<Rigidbody>();

        //Checking for turret
        if (GameObject.FindGameObjectWithTag("Turret") != null)
        {
            Instance.turret = GameObject.FindGameObjectWithTag("Turret").transform;
            //Instance.Agent.SetDestination(turret.transform.position);
            baseEnemyScript.attack = true;
        }
    }

    private void FixedUpdate()
    {
        if (baseEnemyScript.attack)
        {
            Agent.SetDestination(Instance.turret.position);
            float dist = CheckDistance(Instance.turret.position, this.transform.position);
            if (dist <= 2.0f)
            {
                Agent.isStopped = true;
                myRigid.velocity = Vector3.zero;
            }
        }
    }

    private float CheckDistance(Vector3 playerPosition, Vector3 enemyPosition)
    {
        if (playerPosition != null && enemyPosition != null)
        {
            float dist = Vector3.Distance(playerPosition, enemyPosition);
            //Debug.Log(dist);
            return dist;
        }
        return 0;
    }
}
