using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class BlurControl : MonoBehaviour
{

    public Camera MainCamera;
    public GameObject Target;

    private float multiplyingFactor;
    private Vector3 targetPosition;

    // Use this for initialization
    void Start()
    {
        targetPosition = Target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Eidolon"))
        {
            //multiplyingFactor = Vector3.Distance(source.transform.position, targetPosition);
            MainCamera.GetComponent<BlurOptimized>().enabled = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        GameObject source = other.gameObject;
        if (source.name.Equals("Eidolon"))
        {
            var distance = Vector3.Distance(source.transform.position, targetPosition);
            //MainCamera.GetComponent<BlurOptimized>().blurSize = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Equals("Eidolon"))
            MainCamera.GetComponent<BlurOptimized>().enabled = false;
    }
}
