using UnityEngine;
using UnityEngine.UI;

public class ResolveBar : MonoBehaviour
{
    public PlayerController resolve;
    public Image fillImage;

    private Slider _slider;

    void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    void Update()
    {
        float fillValue = resolve.currentResolvePoint;
        _slider.value = fillValue;
    }
}