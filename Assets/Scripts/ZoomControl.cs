using UnityEngine;
using Cinemachine;

public class ZoomControl : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float specialAreaOrthoSize = 10f;
    public float transitionDuration = 1f;

    private float defaultOrthoSize;
    private float startOrthoSize;
    private float targetOrthoSize;
    private float transitionTimer;

    private void Start()
    {
        defaultOrthoSize = virtualCamera.m_Lens.OrthographicSize;
        targetOrthoSize = defaultOrthoSize;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            targetOrthoSize = specialAreaOrthoSize;
            startOrthoSize = virtualCamera.m_Lens.OrthographicSize;
            transitionTimer = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            targetOrthoSize = defaultOrthoSize;
            startOrthoSize = virtualCamera.m_Lens.OrthographicSize;
            transitionTimer = 0f;
        }
    }

    private void Update()
    {
        if (virtualCamera.m_Lens.OrthographicSize != targetOrthoSize)
        {
            transitionTimer += Time.deltaTime;
            float t = Mathf.Clamp01(transitionTimer / transitionDuration);
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(startOrthoSize, targetOrthoSize, t);
        }
    }
}
