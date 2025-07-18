// Unity
using UnityEngine;

[DisallowMultipleComponent]
public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Update()
    {
        TraceTarget();
    }

    private void TraceTarget()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }
}
