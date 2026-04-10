using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool
{
    private Projectile[] projectiles;
    
    /// <summary>
    /// projectile Pool 생성
    /// </summary>
    /// <param name="skillData"></param>
    public ProjectilePool(GameObject prefab, int poolCount)
    {
        projectiles = new Projectile[poolCount];
        
        GameObject go;
        for(int i = 0; i < poolCount; i++)
        {
            go = GameObject.Instantiate(prefab);
            go.SetActive(false);
            projectiles[i] = go.GetComponent<Projectile>();            
        }
    }
    
    /// <summary>
    /// projectile오브젝트를 setActive하고 projectile을 반환
    /// </summary>
    /// <returns></returns>
    public Projectile GetProjectile()
    {
        Projectile returnProjectile = null;
        
        foreach(Projectile projectile in projectiles)
        {
            if(projectile.gameObject.activeSelf == false)
            {
                returnProjectile = projectile;
                returnProjectile.gameObject.SetActive(true);
                break;
            }
        }
        
        if(returnProjectile == null)
        {
            Debug.LogWarning("projectile pool 부족");
            return null;
        }
        
        return returnProjectile;
    }
}
