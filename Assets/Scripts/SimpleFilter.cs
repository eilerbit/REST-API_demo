using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SimpleFilter : MonoBehaviour
{
    [SerializeField] private Shader shader;
    [SerializeField] private int durationInSeconds = 2;

    protected Material material;

    private bool useFilter = false;
        
    private void Awake()
    {
        material = new Material(shader);        
    }

    public virtual void Initialize(IEffectTrigger trigger)
    {
        trigger.OnClick += Use;
    }

    public void Use()
    {        
        StartCoroutine(ActivateFilter());              
    }

    IEnumerator ActivateFilter()
    {
        useFilter = !useFilter;

        yield return new WaitForSeconds(durationInSeconds);

        useFilter = !useFilter;
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (useFilter) UseFilter(source, destination);

        else Graphics.Blit(source, destination);
    }

    protected virtual void UseFilter(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }
}
