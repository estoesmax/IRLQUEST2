using System;
using UnityEngine;

public static class ExtensionMethods
{
    public static void Clear(this Transform transform)
    {
        foreach (Transform child in transform) {
            GameObject.DestroyImmediate(child.gameObject);
        }
    }
    
    public static bool IsPrefabInstance(this GameObject obj)
    {
        bool is_prefab_instance = false;
#if UNITY_EDITOR
        is_prefab_instance = UnityEditor.PrefabUtility.IsPartOfAnyPrefab(obj)
                             && !UnityEditor.PrefabUtility.IsPartOfPrefabAsset(obj)
                             && UnityEditor.PrefabUtility.IsPartOfNonAssetPrefabInstance(obj);
#endif
        return is_prefab_instance;
    }


    // In project or prefab editor.
    public static bool IsPrefabAsset(this GameObject obj)
    {
        bool is_prefab_asset = false;
#if UNITY_EDITOR
        var stage = UnityEditor.Experimental.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage();
        is_prefab_asset = (stage != null
                           && stage.scene == obj.scene)
                          || (UnityEditor.PrefabUtility.IsPartOfAnyPrefab(obj)
                              && UnityEditor.PrefabUtility.IsPartOfPrefabAsset(obj)
                              && !UnityEditor.PrefabUtility.IsPartOfNonAssetPrefabInstance(obj));
#endif
        return is_prefab_asset;
    }

    public static Color WithAlpha(this Color color, float alpha)
    {
        return new Color(color.r, color.g, color.b, alpha);
    }

    public static Color WithRed(this Color color, float red)
    {
        return new Color(red, color.g, color.b, color.a);
    }

    public static Color WithGreen(this Color color, float green)
    {
        return new Color(color.r, green, color.b, color.a);
    }

    public static Color WithBlue(this Color color, float blue)
    {
        return new Color(color.r, color.g, blue, color.a);
    }

    public static void ResetTransformation(this Transform trans)
    {
        trans.position = Vector3.zero;
        trans.localRotation = Quaternion.identity;
        trans.localScale = new Vector3(1, 1, 1);
    }

    public static void CopyPosRotFrom(this Transform trans, Transform otherTransform)
    {
        trans.position = otherTransform.position;
        trans.rotation = otherTransform.rotation;
    }

    public static Vector3 WithX(this Vector3 v, float x)
    {
        return new Vector3(x, v.y, v.z);
    }

    public static Vector3 WithY(this Vector3 v, float y)
    {
        return new Vector3(v.x, y, v.z);
    }

    public static Vector3 WithZ(this Vector3 v, float z)
    {
        return new Vector3(v.x, v.y, z);
    }

    public static Vector2 WithX(this Vector2 v, float x)
    {
        return new Vector2(x, v.y);
    }

    public static Vector2 WithY(this Vector2 v, float y)
    {
        return new Vector2(v.x, y);
    }

    public static Vector3 WithMagnitude(this Vector3 v, float newMagnitude)
    {
        return v.normalized * newMagnitude;
    }

    public static Vector3 Round(Vector3 v)
    {
        return new Vector3(Mathf.Round(v.x), Mathf.Round(v.y), Mathf.Round(v.z));
    }


    public static void LocalResetTransformation(this Transform trans)
    {
        trans.localPosition = Vector3.zero;
        trans.localRotation = Quaternion.identity;
        trans.localScale = new Vector3(1, 1, 1);
    }

    public static Quaternion WithEulerX(this Quaternion q, float v)
    {
        return Quaternion.Euler(v, q.eulerAngles.y, q.eulerAngles.z);
    }

    public static Quaternion WithEulerY(this Quaternion q, float v)
    {
        return Quaternion.Euler(q.eulerAngles.x, v, q.eulerAngles.z);
    }

    public static Quaternion WithEulerZ(this Quaternion q, float v)
    {
        return Quaternion.Euler(q.eulerAngles.x, q.eulerAngles.y, v);
    }

    public static float NextFloat(this System.Random rand)
    {
        return (float) rand.NextDouble();
    }

    public static float NextFloatRange(this System.Random rand, float minimum, float maximum)
    {
        return rand.NextFloat() * (maximum - minimum) + minimum;
    }

    public static float Remap(this float from, float fromMin, float fromMax, float toMin, float toMax)
    {
        var fromAbs = from - fromMin;
        var fromMaxAbs = fromMax - fromMin;

        var normal = fromAbs / fromMaxAbs;

        var toMaxAbs = toMax - toMin;
        var toAbs = toMaxAbs * normal;

        var to = toAbs + toMin;

        return to;
    }

    public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
    {
        if (val.CompareTo(min) < 0) return min;
        else if (val.CompareTo(max) > 0) return max;
        else return val;
    }

    public static Vector3 ClampMagnitude(this Vector3 v, float min, float max)
    {
        return v.normalized * v.magnitude.Clamp(min, max);
    }

    public static Vector3 ClampMagnitude(this Vector3 v, float max)
    {
        return v.normalized * v.magnitude.Clamp(0, max);
    }

    public static float ApplyLimiter(float value, float ampFactor)
    {
        if (value > 0f)
            return Mathf.Pow(value, 1 - ampFactor);
        else
            return -Mathf.Pow(-value, 1 - ampFactor);

    }

    public static void SetGlobalScale(this Transform transform, Vector3 globalScale)
    {
        transform.localScale = Vector3.one;
        Vector3 lossyScale = transform.lossyScale;
        transform.localScale = new Vector3(globalScale.x / lossyScale.x, globalScale.y / lossyScale.y,
            globalScale.z / lossyScale.z);
    }
    
    public static float Frac(this float value)
    {
        return value-Mathf.Floor(value);
    }
}
