using UnityEngine;

public class NullTarget : Target
{
    private readonly Location nullLocation = new NullLocation();
    
    public void Hit(Impact impact, Terminable disposableHitter) {}
    public Vector3 Coordinates() => nullLocation.Coordinates();
    public bool IsTaggedAs(string Tag) => false;
}