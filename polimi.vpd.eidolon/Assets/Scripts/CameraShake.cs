using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{

    private Vector3 originPosition;
    private bool shaking;
    private Transform tranformPlaceholder;

    public float ShakeDecay;
    public float ShakeIntensity;

    private float currentShakeIntensity;

    void OnEnable()
    {
        tranformPlaceholder = transform;
    }

    void Update()
    {
        if (!shaking)
            return;

        if (currentShakeIntensity > 0f)
        {
            tranformPlaceholder.localPosition = originPosition + Random.insideUnitSphere * currentShakeIntensity;
            currentShakeIntensity -= ShakeDecay;
        }
        else
        {
            Debug.Log("stopped shaking");
            shaking = false;
        }
    }

    void Shake()
    {
        if (!shaking)
            originPosition = tranformPlaceholder.localPosition;
        
        shaking = true;
        currentShakeIntensity = ShakeIntensity;
    }
}
