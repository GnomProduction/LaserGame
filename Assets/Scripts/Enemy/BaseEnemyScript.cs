using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IBaseEnemy
{
    void AttactTurret(Transform turret);
}

public class BaseEnemyScript : MonoBehaviour, IBaseEnemy
{
    protected float rotSpeed;
    protected float movementSpeed;

    public bool attack = false;

    //protected NavMeshAgent Agent;
    protected float Speed;

    public uint Life { get; set; }
   // public Rigidbody myRigid;
    public Transform turret;

    private void Start()
    {
        Speed = 0.05f;
        Life = 100;
        // myRigid = gameObject.GetComponent<Rigidbody>();
        //Agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        if (attack == true)
        {
            AttactTurret(turret);
        }
        //if (turret != null)
        //{
        //    if (Vector3.Distance(transform.position, turret.position) <= 2.0f)
        //    {
        //        attack = false;
        //        Agent.enabled = false;
        //    }
        //}
    }

    public virtual void AttactTurret(Transform turret)
    {
        /*Quaternion targetRot = Quaternion.LookRotation(turret.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * rotSpeed);
        transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
        transform.LookAt(turret);
         transform.position += transform.forward * Speed;*/
        // Agent.SetDestination(turret.position);
        Debug.Log("Attack");
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
