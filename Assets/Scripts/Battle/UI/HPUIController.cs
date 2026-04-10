using UnityEngine;

public class HPUIController : MonoBehaviour
{
    [SerializeField] HPUI hpUI;
    [SerializeField] MPUI mpUI;

    [SerializeField] Vector3 upVector;

    private Entity entity;

    private bool isMatched;
    public bool IsMatched => isMatched;

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
            entity.BattleData.HPChanged += ActiveUI;
            hpUI.matchEntity(entity);
            hpUI.gameObject.SetActive(true);
        }
        if (entity is ISkillusable)
        {
            mpUI.matchEntity(entity);
            mpUI.gameObject.SetActive(true);
        }

        isMatched = true;
    }

    public void ResetUI()
    {
        hpUI.ResetUI();
        mpUI.ResetUI();
    }
    
    private void ActiveUI(int hp)
    {
        if(gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }
        
        hpUI.OnSetUI(hp);
    }
}
