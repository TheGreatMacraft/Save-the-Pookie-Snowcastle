using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BehaviourGnome : MonoBehaviour
{
    public EnemyHealth enemyHealthScript;
    private Action hurtHandler;
    public Rigidbody2D rb;

    public float movementSpeed = 5f;
    public float attackCooldown = 3f;
    private enum TargetType { Player, Defense };
    private TargetType targetType;
    public GameObject target;

    public bool isActing = false;
    public bool nearbyTarget = false;
    public bool scared = false;

    //Pick a New Target
    private GameObject PickTarget(List<GameObject> list, TargetType targetType, GameObject avoidTarget = null)
    {
        float min = Mathf.Infinity;
        GameObject chosen = null;

        foreach (var el in list)
        {
            if (el == avoidTarget)
                continue;

            float distance = Vector2.Distance(el.transform.position, transform.position);
            if (distance < min)
            {
                min = distance;
                chosen = el;
            }
        }

        if (chosen != null)
            this.targetType = targetType;

        return chosen;
    }

    //Move Towards Object or Position
    private void MoveTowards(GameObject target)
    {
        Vector2 moveDir = (target.transform.position - transform.position).normalized;
        rb.MovePosition(transform.position + (Vector3)(moveDir * movementSpeed * Time.fixedDeltaTime));
    }


    //Attempt Disarming the Trap
    private IEnumerator AttemptDisarming(GameObject target)
    {
        GameObject disarmTarget = target;

        float timer = 0f;
        float endTime = disarmTarget.GetComponent<DefenseBasics>().timeToDisable;

        while (timer < endTime)
        {
            if (scared)
            {
                if (DefenseTracker.instance.registeredElements.Count > 1)
                    this.target = PickTarget(DefenseTracker.instance.registeredElements, TargetType.Defense, disarmTarget);
                else if (PlayerTracker.instance.anyElementsRegistred())
                    this.target = PickTarget(PlayerTracker.instance.registeredElements, TargetType.Player);
            }

            timer += Time.deltaTime;
            yield return null;
        }

        DisarmedDefense(target);
    }


    //If Defense was Successfully Disarmed
    private void DisarmedDefense(GameObject target)
    {
        DefenseTracker.instance.registeredElements.Remove(target);
        target.GetComponent<DefenseBasics>().isDisabled = true;

        if (DefenseTracker.instance.anyElementsRegistred())
            this.target = PickTarget(DefenseTracker.instance.registeredElements, TargetType.Defense);
        else if (PlayerTracker.instance.anyElementsRegistred())
        {
            this.target = PickTarget(PlayerTracker.instance.registeredElements, TargetType.Player);
        }

        isActing = false;
        nearbyTarget = false;
    }


    //Attacking the Target Player
    private IEnumerator AttackPlayer(GameObject target)
    {
        while (nearbyTarget)
        {
            yield return new WaitForSeconds(attackCooldown);
            Debug.Log("Gnome hurts player!");
        }
    }

    //Handling the Nearby Target Check
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.root.gameObject == target)
            nearbyTarget = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.root.gameObject == target)
            nearbyTarget = false;
    }

    private void Start()
    {
        hurtHandler = () => scared = true;
        enemyHealthScript.Hurt += hurtHandler;

        if (DefenseTracker.instance.anyElementsRegistred())
            target = PickTarget(DefenseTracker.instance.registeredElements, TargetType.Defense);
        else if (PlayerTracker.instance.anyElementsRegistred())
            target = PickTarget(PlayerTracker.instance.registeredElements, TargetType.Player);
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            if (DefenseTracker.instance.anyElementsRegistred())
                target = PickTarget(DefenseTracker.instance.registeredElements, TargetType.Defense);
            else if (PlayerTracker.instance.anyElementsRegistred())
                target = PickTarget(PlayerTracker.instance.registeredElements, TargetType.Player);
            else
                return;
        }

        if (scared)
        {
            if (PlayerTracker.instance.anyElementsRegistred())
            {
                target = PickTarget(PlayerTracker.instance.registeredElements, TargetType.Player);
                targetType = TargetType.Player;
            }
        }

        if (!nearbyTarget && target != null)
        {
            MoveTowards(target);
        }
    }

    private void Update()
    {
        if (nearbyTarget)
        {
            if (!isActing)
            {
                isActing = true;

                if (targetType == TargetType.Defense)
                {
                    StartCoroutine(AttemptDisarming(target));
                }
                else if (targetType == TargetType.Player)
                {
                    StartCoroutine(AttackPlayer(target));
                }
            }
        }
    }

    private void OnDestroy()
    {
        enemyHealthScript.Hurt -= hurtHandler;
    }
}