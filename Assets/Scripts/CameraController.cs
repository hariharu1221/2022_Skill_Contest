using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("포지션 오프셋")] public Vector3 posOffset;
    [Header("각도 오프셋")] public Vector3 rotOffset;
    [Header("카메라 속도")] public float speed;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject target;

    private void Awake()
    {
        if (!mainCamera) mainCamera = Camera.main;
        if (!target) target = FindObjectOfType<Player>().gameObject;
    }
    void Update()
    {
        Vector3 pos = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z) + posOffset;
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, pos, Time.deltaTime * speed);
        mainCamera.transform.position = new Vector3(Mathf.Clamp(mainCamera.transform.position.x, -Utils.limit.x + 40, Utils.limit.x - 40),
            mainCamera.transform.position.y, Mathf.Clamp(mainCamera.transform.position.z, -Utils.limit.y + 2, Utils.limit.y - 43));
    }
}
