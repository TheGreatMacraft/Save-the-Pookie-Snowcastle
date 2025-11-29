using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour
{
    private Coroutine repeatCorutine;

    public float hurtDistance = 0f;
    public float hurtCooldown = 3f;
    public float damageAmount = 1f;

    public bool nearbyCastle = false;
    private bool hasRangedAttack;


    private IEnumerator KeepHurtingCastle()
    {
        while (nearbyCastle)
        {
            HealthCastle.Instance.DecreaseHelth(damageAmount);
            yield return new WaitForSeconds(hurtCooldown);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasRangedAttack && collision.gameObject.name == "Castle")
            nearbyCastle = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!hasRangedAttack && collision.gameObject.name == "Castle")
            nearbyCastle = false;
    }
    private void Awake()
    {
        hasRangedAttack = hurtDistance > 0f;
    }

    private void Update()
    {
        if (hasRangedAttack)
        {
            float distance = Vector2.Distance(GameObject.Find("Castle").transform.position, transform.position);
            nearbyCastle = distance <= hurtDistance;
        }

        if (nearbyCastle && repeatCorutine == null)
        {
            repeatCorutine = StartCoroutine(KeepHurtingCastle());
        }
        else if (!nearbyCastle && repeatCorutine != null)
        {
            StopCoroutine(repeatCorutine);
            repeatCorutine = null;
        }

    }
}
