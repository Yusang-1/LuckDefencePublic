public class Character : Entity, IAttackable, ISkillusable
{
    private bool isAttackable;
    private bool isManaFull;
    
    private SkillController skillController;
    public IDamagable AttackTarget;
    public Platform platform;
    public CharacterStateMachine stateMachine;

    public bool IsAttackable
    {
        get => isAttackable;
        set
        {
            isAttackable = value;
        }
    }

    public bool IsManaFull => isManaFull;
    
    private void Start()
    {
        skillController = new SkillController();
        skillController.SetSkill(this);
    }
    
    private void Update()
    {
        stateMachine?.StateUpdate();
    }

    public override void EntityActivated()
    {
        base.EntityActivated();

        if (stateMachine == null)
        {
            stateMachine = new CharacterStateMachine(this);
        }

        stateMachine.Initialize(stateMachine.IdleState);
    }

    public void GetPlatform(Platform platform)
    {
        this.platform = platform;
    }    

    public void Attack(IDamagable target)
    {
        skillController.UseBasicAttack(target as Entity);
    }
    
    public void DealDamage(IDamagable target)
    {
        target.TakeDamage(BattleData.AttackPoint);

        GetMP(BattleData.GetMPPoint);
    }

    public void GetMP(int amount)
    {
        BattleData.CurrentMP += amount;
        
        if(BattleData.CurrentMP >= BattleData.MaxMp)
        {
            isManaFull = true;
        }
    }

    public void UseSkill(IDamagable target)
    {
        BattleData.CurrentMP = 0;

        isManaFull = false;
        
        skillController.UseSkill(target as Entity);
    }
}
