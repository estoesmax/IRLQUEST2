using UnityEngine;

/// <summary>
/// Fades the light mesh in a nice way
/// </summary>
[ExecuteAlways]
public class DignidadLightMesh : MonoBehaviour
{
    public float maxRadius;
    [Range(0,1)]
    public float intensity = 1f;
    public float maxLightIntensity = 5f;
    
    Renderer _myRenderer;
    Renderer myRenderer
    {
        get
        {
            if (_myRenderer == null)
                _myRenderer = GetComponent<Renderer>();
            return _myRenderer;
        }
    }

    MaterialPropertyBlock _mpb;
    MaterialPropertyBlock mpb
    {
        get
        {
            if (_mpb == null)
                _mpb = new MaterialPropertyBlock();
            return _mpb;
        }
    }

    static readonly int LightIntensity = Shader.PropertyToID("_LightIntensity");
    
    void Update()
    {
        if (gameObject.IsPrefabAsset())
        {
            return;
        }

        
        float radius = maxRadius * Mathf.Pow(intensity, .2f);
        transform.localScale = new Vector3(radius, 9999f, radius);
        myRenderer.GetPropertyBlock(mpb);
        mpb.SetFloat(LightIntensity, Mathf.Pow(intensity, .2f)*maxLightIntensity);
        myRenderer.SetPropertyBlock(mpb);
    }
}
