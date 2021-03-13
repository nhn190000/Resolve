using UnityEngine;
using UnityEngine.UI;

public class TowerBar : MonoBehaviour
{
    public Tower towerHealth;
    public Image fillImage;

    private Slider _slider;

    void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    void Update()
    {
        float fillValue = towerHealth.currentTowerHealth / towerHealth.towerHealth;
        _slider.value = fillValue;
    }
}
