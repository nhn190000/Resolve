using UnityEngine;

public class Tower : MonoBehaviour
{
    public float towerHealth;
    [HideInInspector] public float currentTowerHealth;

    void Start() 
    {
        currentTowerHealth = towerHealth;
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (currentTowerHealth > 0)
            {
                currentTowerHealth -= 10;
            }
            Destroy(other.gameObject);
        }    
    }
}
