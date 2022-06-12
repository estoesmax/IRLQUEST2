using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class DignidadLightsController : MonoBehaviour
{
#pragma warning disable 0649
    [Range(0,1)] 
    [SerializeField] float progress = 0f;
    [Header("Tuning Light Meshes")]
    [OnValueChanged("InstantiateLights")]
    [SerializeField] int totalLights = 36;
    [SerializeField] float positionRadius = 2f;
    [SerializeField] float lightMaxRadius = .1f;

    [Header("Tuning Pointlights")] 
    [SerializeField] float maxIntensity;
    
    [Header("Dependencies")]
    [SerializeField] GameObject lightPrefab;
    [SerializeField] Light[] pointLights;
#pragma warning restore 0649
    float TAU = 6.28318530718f;
    List<DignidadLightMesh> _lightInstances;
    List<DignidadLightMesh> lightInstances
    {
        get
        {
            if (_lightInstances == null)
                _lightInstances = new List<DignidadLightMesh>();
            return _lightInstances;
        }
    }
    
    void InstantiateLights()
    {
        transform.Clear();
        lightInstances.Clear();
        for (int i = 0; i < totalLights; i++)
        {
            GameObject newLight = Instantiate(lightPrefab, transform, true);
            lightInstances.Add(newLight.GetComponent<DignidadLightMesh>());
        }

        pointLights[0].transform.localPosition = new Vector3(0, 30, positionRadius);
        pointLights[1].transform.localPosition = new Vector3(positionRadius, 30, 0);
        pointLights[2].transform.localPosition = new Vector3(0, 30, -positionRadius);
        pointLights[3].transform.localPosition = new Vector3(-positionRadius, 30, 0);

        UpdateLights();
        
        
    }


    void UpdateLights()
    {
        UpdateLightMeshes();
        UpdatePointLights();
    }

    void UpdatePointLights()
    {
        
        for (int i = 0; i < 4; i++)
        {
            pointLights[i].intensity = GetIntensityForPointlight(i);
        }
    }

    void UpdateLightMeshes()
    {
        if (transform.childCount == 0 || lightInstances.Count == 0)
        {
            return;
        }

        float absoluteProgress = progress * totalLights;
        for (int i = 0; i < totalLights; i++)
        {
            float x = positionRadius * Mathf.Sin((TAU / (float) totalLights) * i);
            float z = positionRadius * Mathf.Cos((TAU / (float) totalLights) * i);
            if (lightInstances[i] == null)
            {
                Debug.Log($"lightInstances[{i}] null");
                continue;
            }

            lightInstances[i].transform.localPosition = new Vector3(x, 0, z);
            lightInstances[i].maxRadius = lightMaxRadius;

            float intensity;
            if (i < Mathf.FloorToInt(absoluteProgress))
            {
                intensity = 1f;
            }
            else if (i >= Mathf.CeilToInt(absoluteProgress))
            {
                intensity = 0f;
            }
            else
            {
                intensity = absoluteProgress.Frac();
            }

            lightInstances[i].intensity = intensity;
        }
    }

    float GetIntensityForPointlight(int pointlightIndex)
    {
        int from = pointlightIndex*(totalLights / 4);
        int to = (pointlightIndex + 1)*totalLights / 4;
        float sum = 0;
        for (int i = from; i < to; i++)
        {
            sum += lightInstances[i].intensity;
        }

        return (sum / 4f).Remap(0,1,0,maxIntensity);
    }


    void OnEnable()
    {
        InstantiateLights();
        
    }

    void Update()
    {
        UpdateLights();
    }
}
