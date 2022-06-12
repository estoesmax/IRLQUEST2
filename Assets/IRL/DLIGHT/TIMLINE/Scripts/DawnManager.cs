using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Manages all factors to be modified to make a Dawn effect
/// </summary>
[ExecuteAlways]
public class DawnManager : MonoBehaviour
{
#pragma warning disable 0649
    [Header("Tuning")] 
    [Range(0,1)]
    [SerializeField] float dayPercentage;
    [SerializeField] Vector2 exposureFromTo = new Vector2(.23f, 2f);

    [Header("Dependencies")]
    [SerializeField] Material skyboxMaterial;

    [SerializeField] Volume dawnVolume;
    static readonly int Exposure = Shader.PropertyToID("_Exposure");
#pragma warning restore 0649
    void Update()
    {
        if (dawnVolume == null || skyboxMaterial == null)
        {
            return;
        }
        skyboxMaterial.SetFloat(Exposure, dayPercentage.Remap(0,1,exposureFromTo.x, exposureFromTo.y));
        dawnVolume.weight = dayPercentage;
    }
}
