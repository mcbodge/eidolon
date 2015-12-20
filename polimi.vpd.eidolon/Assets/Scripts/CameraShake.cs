using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{

    private Vector3 originPosition;
    private Quaternion originRotation;
    private bool shaking;
    private Transform tranformPlaceholder;

    public float ShakeDecay;
    public float ShakeIntensity;

    void OnEnable()
    {
        tranformPlaceholder = transform;
    }

    void Update()
    {
        if (!shaking)
            return;

        if (ShakeIntensity > 0f)
        {
            tranformPlaceholder.localPosition = originPosition + Random.insideUnitSphere * ShakeIntensity*2;
            ////tranformPlaceholder.localRotation = new Quaternion(
            ////originRotation.x + Random.Range(-ShakeIntensity, ShakeIntensity) * .1f,
            ////originRotation.y + Random.Range(-ShakeIntensity, ShakeIntensity) * .1f,
            ////originRotation.z + Random.Range(-ShakeIntensity, ShakeIntensity) * .1f,
            ////originRotation.w + Random.Range(-ShakeIntensity, ShakeIntensity) * .2f);
            ShakeIntensity -= ShakeDecay;
        }
        else
        {
            Debug.Log("stopped shaking");
            shaking = false;
            ////tranformPlaceholder.localPosition = originPosition;
            ////tranformPlaceholder.localRotation = originRotation;
        }
    }

    void Shake()
    {
        if (!shaking)
        {
            originPosition = tranformPlaceholder.localPosition;
            originRotation = tranformPlaceholder.localRotation;
        }
        shaking = true;
        ShakeIntensity = .1f;
        ShakeDecay = 0.002f;
    }
}
