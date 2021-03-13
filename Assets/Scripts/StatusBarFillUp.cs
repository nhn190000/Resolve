using UnityEngine;
using UnityEngine.UI;

public class StatusBarFillUp : MonoBehaviour
{
    public PlayerController playerHealth;
    public Image fillImage;

    private Slider _slider;

    void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    void Update()
    {
        float fillValue = playerHealth.currentHealth / playerHealth.maxHealth;
        _slider.value = fillValue;
    }
}
