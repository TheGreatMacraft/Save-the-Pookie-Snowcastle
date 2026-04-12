Since the code is becoming more and more complex, I have decided to start writing this documentation. It's job is to:
- provide a *quick lookup* over how and what certain objects are doing
- *prevent logic fallacies* by providing clear purpose of each object, ensuring that its relationships with other objects is cohesive

# World Space
Unity uses an object called **Vector3** to define a *3D vector* or a *3D Coordinates*. This technically makes sence, since a location in a 3D environment is simply a 3d Vector pointing from the origin (0,0,0) towards itself.
![[geogebra-export.png|400]]

Even though this does make sense, mathematically speaking, it would be better to separate vector definition from the location, for the sake of **clarity**. In game development, we treat Game Object locations differently than vectors, we define them differently and we execute different operations on them - they are ==not== the same.

For the sake of this clarity, I implemented different wrappers, that limit our ability of what we can do with Location or a Vector. This limitations are good, because they ensure that Location is treated and used as a Location - if it would not meet our needs, that hints at it not being the right definition for the current situation:

## Location
Location is a very easy to define. It is simply a collection of *float* values, that describe a Location in the *World Space* - coordinates.

The equals method is just an override, that allows us to compare if Locations are the same / have the same Coordinate values.

```cs
public interface Location  
{  
    Vector3 Coordinates();  
    
    bool Equals(Location other)  
        => Coordinates().Equals(other.Coordinates());  
}
```
## Rotation
Rotation is a bit more complex. As you can see, we have an interface *RotationDefinition* and a class *Rotation*.

Class takes in a definition of a rotation -  however it is implemented, it doesn't really care, as long as it can access it's Quaternion. Then the Rotation class can calculate the degrees on z-axis of the Rotation - **Note: This functionality doesn't make sense for 3D environment**.

Interface presents the ability to define each Rotation differently - some may be constant set Rotations, while some may reflect the ever changing rotation of a Game Object.

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
