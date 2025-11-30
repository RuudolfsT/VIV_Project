using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class XRayKeyToggle : MonoBehaviour
{
    public Key xrayKey = Key.X;
    public ScriptableRendererFeature objectHiddenFeature;
    public ScriptableRendererFeature aiHiddenFeature;

    void Update()
    {
        if (Keyboard.current[xrayKey].isPressed)
        {
            objectHiddenFeature.SetActive(true);
            aiHiddenFeature.SetActive(true);
        }
        else
        {
            objectHiddenFeature.SetActive(false);
            aiHiddenFeature.SetActive(false);
        }
    }
}
