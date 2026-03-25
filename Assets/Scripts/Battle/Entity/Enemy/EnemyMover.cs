using UnityEngine;

public class EnemyMover : EntityMover
{
    private int currentBeaconIndex;

    private void Update()
    {
        Move();
    }

    public override void Initialize(Entity entity)
    {
        base.Initialize(entity);

        currentBeaconIndex = 0;
        directionVector = GetDestinationVector(transform.position);
    }

    public override Vector3 GetDestinationVector(Vector3 position)
    {
        transform.position = BeaconContainer.s_Beacons[currentBeaconIndex].position;

        currentBeaconIndex++;
        if (currentBeaconIndex >= BeaconContainer.s_Beacons.Length)
        {
            currentBeaconIndex = 0;
        }

        return (BeaconContainer.s_Beacons[currentBeaconIndex].position - transform.position).normalized;
    }

    public override void Move()
    {
        Vector3 moveSpeed = directionVector * entity.BattleData.MoveSpeed * Time.deltaTime;
        transform.position += moveSpeed;

        //rigidbody2d.AddForce(moveSpeed * 10000, ForceMode2D.Impulse);

        if (Vector3.Distance(transform.position, BeaconContainer.s_Beacons[currentBeaconIndex].position) <= Vector3.Distance(transform.position, transform.position + moveSpeed))
        {
            directionVector = GetDestinationVector(transform.position);
        }
    }
}
