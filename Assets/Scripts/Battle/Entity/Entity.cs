using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] private EntitySO data;
    [SerializeField] private EntityMover mover;
    protected BattleEntityData battleData;
    private BuffController buffController;

    public EntitySO Data => data;
    public EntityMover Mover => mover;
    public BattleEntityData BattleData => battleData;
    public BuffController BuffController => buffController;

    private void Awake()
    {
        mover?.Initialize(this);
    }
    
    private void Start()
    {
        buffController = GetComponent<BuffController>();
        buffController?.Initialize(this);
    }

    public abstract void EntityActivated();
}
