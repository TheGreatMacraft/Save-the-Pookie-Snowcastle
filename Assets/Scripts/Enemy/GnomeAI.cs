using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class GnomeAI : MonoBehaviour
{
    
    // Variables to be Assigned in Inspector
    

    // External Objects Necessary
    public EnemyAIBase aiScript;
    public EnemyAttackBase attackScript;
    public EnemyMovementBase movementScript;
    public EnemyHealth enemyHealthScript;
    
    public Rigidbody2D rb;

    // Variables used in Script
    public bool scared;

    public bool isActing;
    private Action hurtHandler;
    private TargetType targetType;

    private void Start()
    {
        hurtHandler = () => scared = true;
        enemyHealthScript.Hurt += hurtHandler;

        if (DefenseTracker.instance.anyElementsRegistred())
            target = PickTarget(DefenseTracker.instance.registeredElements, TargetType.Defense);
        else if (PlayerTracker.instance.anyElementsRegistred())
            target = PickTarget(PlayerTracker.instance.registeredElements, TargetType.Player);
    }

    private void Update()
    {
        if (nearbyTarget)
            if (!isActing)
            {
                isActing = true;

                if (targetType == TargetType.Defense)
                    StartCoroutine(AttemptDisarming(target));
                else if (targetType == TargetType.Player) StartCoroutine(AttackPlayer(target));
            }
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
            if (PlayerTracker.instance.anyElementsRegistred())
            {
                target = PickTarget(PlayerTracker.instance.registeredElements, TargetType.Player);
                targetType = TargetType.Player;
            }

        if (!nearbyTarget && target != null) MoveTowards(target);
    }

    private void OnDestroy()
    {
        enemyHealthScript.Hurt -= hurtHandler;
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

    //Pick a New Target
    private GameObject PickTarget(List<GameObject> list, TargetType targetType, GameObject avoidTarget = null)
    {
        var min = Mathf.Infinity;
        GameObject chosen = null;

        foreach (var el in list)
        {
            if (el == avoidTarget)
                continue;

            var distance = Vector2.Distance(el.transform.position, transform.position);
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
        var disarmTarget = target;

        var timer = 0f;
        var endTime = disarmTarget.GetComponent<DefenseBasics>().timeToDisable;

        while (timer < endTime)
        {
            if (scared)
            {
                if (DefenseTracker.instance.registeredElements.Count > 1)
                    this.target = PickTarget(DefenseTracker.instance.registeredElements, TargetType.Defense,
                        disarmTarget);
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
            this.target = PickTarget(PlayerTracker.instance.registeredElements, TargetType.Player);

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

    private enum TargetType
    {
        Player,
        Defense
    }
}
*/