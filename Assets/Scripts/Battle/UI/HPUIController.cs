using UnityEngine;

public class HPUIController : MonoBehaviour
{
    [SerializeField] HPUI hpUI;
    [SerializeField] MPUI mpUI;

    [SerializeField] Vector3 upVector;

    private Entity entity;

    public bool IsMatched => hpUI.IsMatched || mpUI.IsMatched;

    private void Start()
    {
        hpUI.HPZero += ResetUI;
    }

    private void LateUpdate()
    {
        transform.position = Camera.main.WorldToScreenPoint(entity.transform.position + upVector);
    }

    public void matchEntity(Entity entity)
    {
        this.entity = entity;

        if (entity is IDamagable)
        {
            entity.BattleData.HPChanged += SetHP;
            hpUI.matchEntity(entity);
            hpUI.gameObject.SetActive(true);            
        }
        if (entity is ISkillusable)
        {
            (entity.BattleData as ISkillUsableData).MPChanged += SetMP;
            mpUI.matchEntity(entity);
            mpUI.gameObject.SetActive(true);
        }        
    }

    public void ResetUI()
    {
        if(hpUI.IsMatched)
        {
            entity.BattleData.HPChanged -= SetHP;
            hpUI.ResetUI();
        }
        if(mpUI.IsMatched)
        {
            (entity.BattleData as ISkillUsableData).MPChanged -= SetMP;
            mpUI.ResetUI();
        }
    }
    
    private void SetHP(int hp)
    {
        if(gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }
        
        hpUI.OnSetUI(hp);
    }
    
    private void SetMP(int mp)
    {
        if(gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }
        
        mpUI.OnSetUI(mp);
    }
}
