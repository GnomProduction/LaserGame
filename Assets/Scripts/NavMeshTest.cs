using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshTest : BaseEnemyScript
{
   /* private NavMeshTest Instance;

    private void Start()
    {
        Instance = this;
        if(GameObject.FindGameObjectWithTag("Turret") != null)
        {
            Instance.GetComponent<BaseEnemyScript>().turret = GameObject.FindGameObjectWithTag("Turret").transform;
        }
    }

    public override void AttactTurret(Transform turret)
    {
        Instance.Agent.SetDestination(turret.transform.position);
    }

    private void FixedUpdate()
    {
        if (Instance.attack)
        {
            AttactTurret(Instance.GetComponent<BaseEnemyScript>().turret);
        }
    }*/
}
