Since the code is becoming more and more complex, I have decided to start writing this documentation. It's job is to:
- provide a *quick lookup* over how and what certain objects are doing
- *prevent logic fallacies* by providing clear purpose of each object, ensuring that its relationships with other objects is cohesive

# World Space
Unity defines Location, Vector the same. Rotation is Quaternion, but sometimes eulter and meh
## Location

```cs
public interface Location  
{  
    Vector3 Coordinates();  
    
    bool Equals(Location other)  
        => Coordinates().Equals(other.Coordinates());  
}
```
## Rotation

```cs
public interface RotationDefinition  
{  
    public Quaternion Quaternion();  
}
```

```cs
public class Rotation  
{  
    private sealed readonly RotationDefinition definition;  
  
  
    public Rotation(RotationDefinition definition)  
    {        
	    this.definition = definition;  
    }    
          
	public Quaternion Quaternion()  
        => definition.Quaternion();
        
    public float Degrees()  
        => Quaternion().eulerAngles.z; 
         
    public bool Equals(Rotation other)  
        => Quaternion().Equals(other.Quaternion());  
}
```
## Vector

```cs
public interface VectorDefinition  
{  
    Vector3 RawVector();  
    Location StartLocation();  
}
```

```cs
public sealed class Vector  
{  
    private readonly VectorDefinition definition;  
      
    public Vector(VectorDefinition definition)  
    {        
	    this.definition = definition;  
    }    
      
    public Location StartLocation()  
        => definition.StartLocation(); 
         
    public Vector3 RawVector()   
		=> definition.RawVector();  
		
    public Vector3 Direction()  
        => RawVector().normalized;  
        
    public float Magnitude()  
        => RawVector().magnitude;
          
    public float AngleInDegrees()  
        => Mathf.Atan2(  
            RawVector().y,  
            RawVector().x  
        ) * Mathf.Rad2Deg;  
        
    public bool Equals(Vector other)  
        => RawVector().Equals(other.RawVector());  
}
```
## Physical Body - Transform Wrapper
## Physical Movement - Rigidbody2D Wrapper 

# Action System
