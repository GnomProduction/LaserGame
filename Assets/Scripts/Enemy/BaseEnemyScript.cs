using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemyScript : MonoBehaviour
{
    //private Quaternion MyRotation;
    private float Speed;
    private bool attack = false;
    private NavMeshAgent Agent;

    public Rigidbody myRigid;
    public Transform turret;

    private void Start()
    {
        Speed = 0.05f;
        //MyRotation = transform.rotation;
        myRigid = gameObject.GetComponent<Rigidbody>();

        Agent = GetComponent<NavMeshAgent>();

        //Checking for turret
        if (GameObject.FindGameObjectWithTag("Turret") != null)
        {
            turret = GameObject.FindGameObjectWithTag("Turret").transform;
            attack = true;
        }
    }

    private void FixedUpdate()
    {
        if (attack == true)
        {
            AttactTurret();
        }
    }

    private void AttactTurret()
    {
        Agent.SetDestination(turret.position);
       /* transform.LookAt(turret);
        transform.position += transform.forward * Speed;*/
    }

    private void Rest()
    {
        // Debug.Log("No player on the scene");
    }

    public void OnTurretSpawned(object source, TurretSpawnedEventArgs e)
    {
        if (GameObject.FindGameObjectWithTag("Turret") != null)
        {
            turret = GameObject.FindGameObjectWithTag("Turret").transform;
        }
        attack = true;
    }
}
