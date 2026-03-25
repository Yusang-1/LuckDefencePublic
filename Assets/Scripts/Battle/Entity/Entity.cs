using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private EntitySO data;
    [SerializeField] private EntityMover mover;
    private BattleEntityData battleData;
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

    public virtual void EntityActivated()
    {
        //Debug.Log($"{gameObject.name} activated");
        if(battleData == null)
        {
            battleData = new BattleEntityData(data, this);
        }
        else
        {
            battleData.UpdateData(data, this);
        }
    }
}
