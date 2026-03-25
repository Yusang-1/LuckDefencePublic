using UnityEngine;

public abstract class EntityMover : MonoBehaviour
{
    protected Entity entity;    
    protected Vector3 directionVector;    

    public virtual void Initialize(Entity entity)
    {
        this.entity = entity;        
    }

    public abstract Vector3 GetDestinationVector(Vector3 vec);
    

    public abstract void Move();
}
