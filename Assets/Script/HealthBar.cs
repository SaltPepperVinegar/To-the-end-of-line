using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private Camera camera;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    public void UpdateHealthBar(float currentValue, float maxValue ){
        slider.value = currentValue/maxValue;
    }

    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {   
        if (camera != null){
            transform.rotation = camera.transform.rotation;
            transform.position = target.position+offset;

        }
    }
}
