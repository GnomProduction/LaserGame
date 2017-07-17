using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1Script : BaseEnemyScript
{
    private static Enemy1Script Instance { get; set; }

    private void Start()
    {
        Instance = this;
        Instance.Speed = 0.05f;
        Instance.Life = 120;
        Instance.Agent = GetComponent<NavMeshAgent>();

        //Checking for turret
        if (GameObject.FindGameObjectWithTag("Turret") != null)
        {
            turret = GameObject.FindGameObjectWithTag("Turret").transform;
            Instance.attack = true;
        }
    }
}
