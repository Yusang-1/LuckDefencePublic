using UnityEngine;

public class Character : Entity, IAttackable, ISkillusable
{
    private bool isAttackable;
    private bool isManaFull;
    
    private BattleCharacterData battleCharacterData => BattleData as BattleCharacterData;
    
    public IDamagable AttackTarget;
    public Platform platform;
    public CharacterStateMachine stateMachine;
    
    private HPSpawner hPSpawner;
    private HPUIController hPUIController;
    
    public bool IsAttackable
    {
        get => isAttackable;
        set
        {
            isAttackable = value;
        }
    }

    public bool IsManaFull => isManaFull;
    
    private void Update()
    {
        stateMachine?.StateUpdate();
    }

    private void OnDisable()
    {
        CharacterList.Deactivated(this);
        if(hPUIController != null)
        {
            hPUIController.ResetUI();            
        }
    }
    
    public override void EntityActivated()
    {            
        if(battleData == null)
        {
            battleData = new BattleCharacterData(Data, this);
        }
        else
        {
            battleData.UpdateData(Data, this);
        }
        
        CharacterList.Activated(this);
        
        if (stateMachine == null)
        {
            stateMachine = new CharacterStateMachine(this);
        }
        
        if (hPSpawner == null)
        {
            hPSpawner = FindAnyObjectByType<HPSpawner>();
        }
        hPUIController = hPSpawner.ActivateHP(this);
        hPUIController.gameObject.SetActive(true);
        
        stateMachine.Initialize(stateMachine.IdleState);
    }

    public void GetPlatform(Platform platform)
    {
        this.platform = platform;
    }    

    public void Attack(IDamagable target)
    {
        battleCharacterData.AttackData.UseSkill(target as Enemy);
    }

    public void GetMP()
    {
        battleCharacterData.CurrentMP += battleCharacterData.GetMPPoint;
        
        if(battleCharacterData.CurrentMP >= battleCharacterData.MaxMp)
        {
            isManaFull = true;
        }
    }
    
    public void GetMP(int amount)
    {
        battleCharacterData.CurrentMP += amount;
        
        if(battleCharacterData.CurrentMP >= battleCharacterData.MaxMp)
        {
            isManaFull = true;
        }
    }

    public void UseSkill(IDamagable target)
    {
        battleCharacterData.CurrentMP = 0;

        isManaFull = false;
        
        battleCharacterData.SkillData.UseSkill(target as Entity);
    }
}
