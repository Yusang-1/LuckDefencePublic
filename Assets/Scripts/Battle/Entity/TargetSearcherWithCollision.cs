using UnityEngine;

public class TargetSearcherWithCollision : MonoBehaviour
{
    [SerializeField] private Platform platform;
    private BoxCollider2D boxCollider2D;
    [SerializeField] LayerMask layerMask;

    private Entity trackingTarget;
    private float range;
    
    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (platform.Target == null)
        {
            //SerachTarget();
        }
        else
        {
            //TrackTarget();
        }
    }

    public void Initialize(Character character)
    {
        range = character.Data.AttackRange;
        boxCollider2D.edgeRadius = range + transform.localScale.x / 2;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (platform.EntityCount == 0) return;
        if(trackingTarget != null) return;

        Entity tempTarget = null;
        Debug.Log(00);
        if (1 << (collision.transform.gameObject.layer) == layerMask.value)
        {
            tempTarget = collision.GetComponent<Entity>();
            trackingTarget = tempTarget;            
        }

        Debug.Log(11);
        platform.SetTarget(tempTarget);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(trackingTarget == null) return;

        if (1 << (collision.transform.gameObject.layer) == layerMask.value)
        {
            if (trackingTarget == collision.GetComponent<Entity>())
            {                
                trackingTarget = null;
                platform.SetTarget(null);
                Debug.Log(22);
            }
        }
    }

    private void SerachTarget()
    {
        if (platform.EntityCount == 0) return;

        Entity tempTarget = null;

        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, range, Vector2.zero,0, layerMask);
        
        if(hits.Length > 0 )
        {
            Debug.Log(1);
            tempTarget = hits[0].collider.GetComponent<Entity>();
        }

        platform.SetTarget(tempTarget);        
    }

    private void TrackTarget()
    {
        if (Vector3.Distance(transform.position, platform.Target.transform.position) > range)
        {
            platform.SetTarget(null);
            boxCollider2D.edgeRadius = 0;
        }
    }
}
