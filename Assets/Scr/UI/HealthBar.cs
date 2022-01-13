using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private LivingEntity _TargetEntity;
    [SerializeField] private Vector2 _Offset;
    [SerializeField] private Gradient _Color;
    
    private Transform _TargetTransform; 
    private Image _BarImage;
    
    void Start()
    {
        _TargetTransform = _TargetEntity.transform;

        var barTransform = transform.Find("Bar");
        if (barTransform == null)
        {
            throw new System.NullReferenceException("Change bar name");
        }
        _BarImage = barTransform.GetComponent<Image>();

        _TargetEntity.OnDeath += OnDeath;
    }

    void Update()
    {
        var targetPosition = _TargetTransform.position;
        var screenPosition = Camera.main.WorldToScreenPoint(targetPosition);

        transform.position = (Vector2)screenPosition + _Offset;

        var healthNormalized = (float)_TargetEntity.CurrentHealth / (float)_TargetEntity.TotalHealth;
        _BarImage.fillAmount = healthNormalized;

        var color = _Color.Evaluate(healthNormalized);
        _BarImage.color = color;
    }

    void OnDeath()
    {
        gameObject.SetActive(false);
    }
}
