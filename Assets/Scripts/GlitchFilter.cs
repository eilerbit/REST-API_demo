using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchFilter : SimpleFilter
{
    [SerializeField, Range(0, 1)] private float colorIntensity = 1f;
    [SerializeField, Range(0, 1)] private float noiseIntensity = 1f;
    [SerializeField, Range(0, 1)] private float flipIntensity = 1f;

    [SerializeField] private Texture2D noiseTexture;

    private float flickerTimer;
    private float flickerTime = 0.5f;

    private float flipUpTimer;
    private float flipDownTimer;
    private float flipUpTime = 0.5f;
    private float flipDownTime = 0.5f;

    protected override void UseFilter(RenderTexture source, RenderTexture destination)
    {
        flipUpTimer += Time.deltaTime * flipIntensity;
        if (flipUpTimer > flipUpTime)
        {
            if (Random.value < 0.1f * flipIntensity)
                material.SetFloat("_FlipUp", Random.value * flipIntensity);
            else material.SetFloat("_FlipUp", 0);

            flipUpTimer = 0;
            flipUpTime = Random.value * 0.1f;
        }

        flipDownTimer += Time.deltaTime * flipIntensity;
        if (flipDownTimer > flipDownTime)
        {
            if (Random.value < 0.1f * flipIntensity)
                material.SetFloat("_FlipDown", 1 - Random.value * flipIntensity);
            else material.SetFloat("_FlipDown", 1);

            flipDownTimer = 0;
            flipDownTime = Random.value * 0.1f;
        }

        if(flipIntensity == 0)
        {
            material.SetFloat("_FlipUp", 0);
            material.SetFloat("_FlipDown", 1);
        }

        material.SetTexture("_NoiseTex", noiseTexture);

        if(Random.value < 0.05 * noiseIntensity)
        {
            material.SetFloat("_DisplacementFactor", Random.value * noiseIntensity);
            material.SetFloat("_NoiseScale", 1 - Random.value - noiseIntensity);
        }

        else
        {
            material.SetFloat("_DisplacementFactor", 0);
        }

        flickerTimer += Time.deltaTime * colorIntensity;

        if(flickerTimer > flickerTime)
        {
            material.SetVector("_ColorDirection", Random.insideUnitCircle);
            material.SetFloat("_ColorRadius", Random.Range(-3f, 3f) * colorIntensity);
            flickerTimer = 0;
            flickerTime = Random.value;
        }

        if(colorIntensity == 0)
        {
            material.SetFloat("_ColorRadius", 0);
        }

        Graphics.Blit(source, destination, material);
    }
}
