using UnityEngine;

public class FollowUICanvas : MonoBehaviour
{
    public Transform targetCamera;
    public Vector3 offset = new Vector3(0.3f, 0.3f, 1.2f);

    void LateUpdate()
    {
        if (targetCamera == null) return;

        // 카메라 기준 위치 고정
        transform.position = targetCamera.position + targetCamera.rotation * offset;

        // 항상 카메라를 향하도록 회전
        transform.rotation = Quaternion.LookRotation(transform.position - targetCamera.position);
    }
}