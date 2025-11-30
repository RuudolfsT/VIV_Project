using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ToggleVisiblePass : MonoBehaviour
{
    public ScriptableRendererFeature objectVisibleFeature;

    void Start()
    {
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        while (true)
        {
            objectVisibleFeature.SetActive(!objectVisibleFeature.isActive);
            renderer.shadowCastingMode = objectVisibleFeature.isActive ? UnityEngine.Rendering.ShadowCastingMode.On : UnityEngine.Rendering.ShadowCastingMode.Off;
            yield return new WaitForSeconds(1f);
        }
    }
}
