using UnityEngine;

public enum EntityState
{
    Idle,
    Pursuing,
    Acting,
}

 public abstract class EntityAIBase : MonoBehaviour
 {
     // External Objects Necessary
     protected EntityAct actScript;
     protected Rigidbody2D entityRb;
     
     protected Collider2D currentTargetCollider;
     
     // Variables used in Script
     public string pursuingTargetTag;
     
     [field: SerializeField]
     public EntityState currentState { get; private set; }

     public GameObject currentTarget;
     
     // The condition under which the Current Target Should Be Replaced 
     protected virtual bool ShouldChangeCurrentTarget()
     {
         return (currentTarget == null);
     }
     
     protected virtual void Start()
     {
         SetupComponents();
         
         SetNewTarget();
     }

     protected virtual void Update()
     {
         // If not commited to an Attack, update State
         if(actScript.actionHandler.actionModules[actScript.actionHandler.mainActionName].canAct)
            UpdateCurrentState();
         
         if(ShouldChangeCurrentTarget())
             SetNewTarget();
     }
     
     protected virtual void SetupComponents()
     {
         // Attack Base Script
         if (actScript == null)
             actScript = GetComponent<EntityAct>();

         // Rigidbody
         if (entityRb == null)
             entityRb = GetComponent<Rigidbody2D>();
     }

     public void UpdateCurrentState()
     {
         // If no target exists, stay Idle
         if (currentTarget == null)
         {
             currentState = EntityState.Idle;
             return;
         }
         
         // Set Current State to Acting if Near Enemy, or Pursuing if Not
         currentState = NearbyTarget() ? EntityState.Acting : EntityState.Pursuing;
     }
     
     // Replace Current Target (Given or Find a New Target)
     public virtual void SetNewTarget(GameObject target = null)
     {
         currentTarget = target != null ? target : FindNewTarget();
         
         if(currentTarget != null)
            currentTargetCollider = currentTarget.GetComponent<Collider2D>();
     }
     
     // Find New Target
     protected virtual GameObject FindNewTarget()
     {
         return GameObject.FindGameObjectWithTag(pursuingTargetTag);
     }
     
     // Check if Current Target is within Attack Distance
     public bool NearbyTarget()
     {
         if (currentTarget == null)
             return false;
             
         var distanceToTarget = Vector3.Distance(currentTarget.transform.position, transform.position);
         return  distanceToTarget <= actScript.actionHandler.actionRange || entityRb.IsTouching(currentTargetCollider);
     }
 }
