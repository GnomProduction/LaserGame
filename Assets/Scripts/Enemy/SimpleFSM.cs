using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFSM : FSM
{
    public enum FSMState
    {
        None,
        Patrol,
        Chase,
        Attack,
        Dead,
    }

    public FSMState curState;

    private float curSpeed;

    private float curRotSpeed;

    private bool bDead;
    private int health;
    public float triggerDistance = 100.0f;

    protected override void Initialize()
    {
        curState = FSMState.Patrol;
        curSpeed = 10.0f;
        curRotSpeed = 100.0f;

        bDead = false;
        elapsedTime = 0.0f;
        attackRate = 3.0f;
        health = 100;

        pointList = GameObject.FindGameObjectsWithTag("WandarPoint");

        FindNextPoint();

        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");

        if(objPlayer)
            playerTransform = objPlayer.transform;

        if (!playerTransform)
            Debug.Log("Player doesnt exist!");
    }

    protected override void FSMUpdate()
    {
        if(playerTransform != null)
        {
            switch (curState)
            {
                case FSMState.Patrol:
                    UpdatePatrolState();
                    break;
                case FSMState.Chase:
                    UpdateChaseState();
                    break;
                case FSMState.Attack:
                    UpdateAttackState();
                    break;
                case FSMState.Dead:
                    UpdateDeadState();
                    break;
            }
        }
        
        else
        {
            UpdatePatrolState();
        }
        elapsedTime += Time.deltaTime;

        if (health <= 0)
        {
            curState = FSMState.Dead;
        }

        Debug.Log(curState);
    }

    protected void UpdatePatrolState()
    {
        if (Vector3.Distance(transform.position, destPos) <= 10.0f)
        {
            Debug.Log("Reached to the destinations point\n Calculating the next point");

            FindNextPoint();
        }
        if(playerTransform != null)
        {
            if (Vector3.Distance(transform.position, playerTransform.position) <= 300.0f &&
            playerTransform != null)
            {
                Debug.Log("Switch to chase position");
                curState = FSMState.Chase;
            }
        }
        

        //Debug.Log(Vector3.Distance(transform.position, playerTransform.position));

        Quaternion targetRotation = Quaternion.LookRotation(destPos - transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * curRotSpeed);

        transform.Translate(Vector3.forward * Time.deltaTime * curSpeed);
    }

    protected void UpdateChaseState()
    {
        destPos = playerTransform.position;

        float dist = Vector3.Distance(transform.position, playerTransform.position);

        if (dist <= 200.0f)
        {
            curState = FSMState.Attack;
        }
        else if (dist >= 300.0f)
        {
            curState = FSMState.Patrol;
        }

        transform.Translate(Vector3.forward * Time.deltaTime * curSpeed);
    }

    private void UpdateAttackState()
    {
        destPos = playerTransform.position;

        float dist = Vector3.Distance(transform.position, playerTransform.position);

        if (dist >= 200.0f && dist <= 300.0f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(destPos - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * curRotSpeed);
            transform.Translate(Vector3.forward * Time.deltaTime * curSpeed);
            curState = FSMState.Attack;
        }
        else if (dist >= triggerDistance)
        {
            curState = FSMState.Patrol;
        }
    }

    private void UpdateDeadState()
    {
        if (!bDead)
        {
            bDead = true;
        }
    }

    protected void FindNextPoint()
    {
        Debug.Log("Finding next point");
        int rndIndex = UnityEngine.Random.Range(0, pointList.Length);
        float rndRadius = 10.0f;
        Vector3 rndPosition = Vector3.zero;
        destPos = pointList[rndIndex].transform.position + rndPosition;

        if (IsInCurrentRange(destPos))
        {
            rndPosition = new Vector3(UnityEngine.Random.Range(-rndRadius, rndRadius), 0.0f, UnityEngine.Random.Range(-rndRadius, rndRadius));
            destPos = pointList[rndIndex].transform.position + rndPosition;
        }
    }

    protected bool IsInCurrentRange(Vector3 pos)
    {
        float xPos = Mathf.Abs(pos.x - transform.position.x);
        float zPos = Mathf.Abs(pos.z - transform.position.z);

        if (xPos <= 50 && zPos <= 50)
            return true;

        return false;
    }
}
