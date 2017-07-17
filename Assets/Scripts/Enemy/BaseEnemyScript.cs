using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IBaseEnemy
{
    void AttactTurret();
}

public class BaseEnemyScript : MonoBehaviour, IBaseEnemy
{
    public bool attack = false;
    
    protected NavMeshAgent Agent;
    protected float Speed;

    public uint Life { get; set; }
    public Rigidbody myRigid;
    public Transform turret;

    private void Start()
    {
        Speed = 0.05f;
        Life = 100;
        myRigid = gameObject.GetComponent<Rigidbody>();
        Agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        if (attack == true)
        {
            AttactTurret();
        }
    }

    public void AttactTurret()
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
