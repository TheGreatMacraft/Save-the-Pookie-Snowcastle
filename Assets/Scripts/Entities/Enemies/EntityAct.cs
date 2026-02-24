using UnityEngine;

public class EntityAct : MonoBehaviour
{
    // External Objects Necessary
    public EntityAIBase AIBaseScript;
    
    // Variables used in Script
    public ActionHandler actionHandler;
    
    protected void Start()
    {
        SetupComponents();
    }

    protected virtual void SetupComponents()
    {
        // AI Base Script
        AIBaseScript = GetComponent<EntityAIBase>();
        
        // Actions Script
        actionHandler = GetComponentInChildren<ActionHandler>();
    }

    protected void Update()
    {
        // Cancel if:
        if(AIBaseScript.currentTarget == null   // Target is Null
           || AIBaseScript.currentState != EntityState.Acting // Current State isn't Acting
           ) {return;}

        // Act out the Action
        actionHandler.ActionExecutionOrder();
    }
}