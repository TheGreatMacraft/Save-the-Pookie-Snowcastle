using System.Collections.Generic;
using UnityEngine;

public class ActionImpacts : Impact
{
    private readonly ReadOnlyCollection<Impact> allImpacts;


    public ActionImpacts(ReadOnlyCollection<Impact> allImpacts)
    {
        this.allImpacts = allImpacts;
    }


    public void ApplyOn(GameObject targetGameObject)
    {
        foreach (
            Impact impact 
            in allImpacts.AllElements())
        {                                                                                                         
            impact.ApplyOn(targetGameObject);
        }
    }
}