using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    PostProcessVolume postProcessVolume;
    [SerializeField] float rgbShiftTime = 0.5f;

    private void Awake()
    {
        postProcessVolume = GetComponent<PostProcessVolume>();
        postProcessVolume.enabled = false;
        StartCoroutine(SubscribeToPlayerHealthChange());
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
    }

    IEnumerator SubscribeToPlayerHealthChange()
    {
        yield return new WaitUntil(() => playerTransform.GetComponent<PlayerController>() != null);

        playerTransform.GetComponent<PlayerController>().OnHealthChange += HandlePlayerHealthChange;
    }

    void HandlePlayerHealthChange(IDamageable entity)
    {
        StartCoroutine(ApplyDamageEffect());
    }

    IEnumerator ApplyDamageEffect()
    {
        postProcessVolume.enabled = true;
        yield return new WaitForSeconds(rgbShiftTime);
        postProcessVolume.enabled = false;
    }
}
