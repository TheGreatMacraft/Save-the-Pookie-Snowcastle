using UnityEngine;

public sealed class SlashCall : 
    ActionCall
{
    private readonly ColliderSensor sensor;
    private readonly Payload slashPayload;

    
    public SlashCall(
        ColliderSensor sensor,
        Payload slashPayload
    )
    {
        this.sensor = sensor;
        this.slashPayload = slashPayload;
    }
    

    public void Call()
    {
        slashPayload.Deliver(sensor.ObjectsInCollider());
    }
}