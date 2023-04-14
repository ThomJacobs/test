using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class Healthbar : MonoBehaviour 
{
    public int maxHealth;
    public UnityEngine.Events.UnityEvent onEmpty;
    [SerializeField] private Slider slider;
    private const int MINIMUM_HEALTH = 0;

    void Awake()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    public void Reduce(float p_value)
    {
        slider.value -= p_value;
        if (slider.value == slider.minValue) onEmpty.Invoke();
    }

    public int Health
    {
        set
        {
            slider.value = value;
        }
        get => (int)slider.value;
    }

}

