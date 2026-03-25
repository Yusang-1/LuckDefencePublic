using UnityEngine;
using System.Collections;

public class CharacterMover : EntityMover
{    
    private Vector3 destinationPosition;

    public override void Initialize(Entity entity)
    {
        base.Initialize(entity);
    }

    public override Vector3 GetDestinationVector(Vector3 vec)
    {
        destinationPosition = vec;

        return Vector3.zero;
    }

    public override void Move()
    {
        StartCoroutine(MoveCoroutine());
    }

    private IEnumerator MoveCoroutine()
    {
        Vector3 directionVector = (destinationPosition - transform.position).normalized;
        Vector3 moveSpeed = directionVector * entity.Data.MoveSpeed * Time.deltaTime;
        while (true)
        {
            transform.position += moveSpeed;

            if(Vector3.Distance(destinationPosition, transform.position) <= Vector3.Distance(transform.position + moveSpeed, transform.position))
            {
                transform.position = destinationPosition;
                break;
            }

            yield return null;
        }        
    }
}
