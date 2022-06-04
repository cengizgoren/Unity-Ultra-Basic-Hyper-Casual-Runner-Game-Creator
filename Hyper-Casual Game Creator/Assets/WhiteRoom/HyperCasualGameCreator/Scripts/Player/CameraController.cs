using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform starterPos;
    [SerializeField] private float xLimit = .3f;
    [SerializeField] private float followSmoothness = .5f;
    [SerializeField] private Vector3 positionOffset;
    private void Update()
    {
        if (target != null) return;
        
        var player = FindObjectOfType<PlayerController>().transform;
        if (player != null) target = player;
    }
    
    private void LateUpdate()
    {
        if (target == null) return;
        Follow();
    }

    private void Follow()
    {
        var pos = target.position + positionOffset;
        pos.x = Mathf.Clamp(pos.x,-xLimit,xLimit);
        pos.y = Mathf.Clamp(pos.y, 1.5f, pos.y);

        transform.position = Vector3.Lerp(transform.position,pos,followSmoothness);
        transform.eulerAngles = new Vector3(23,0,0);
    }

    private void Look()
    {
        transform.position = starterPos.position;
        transform.rotation = starterPos.rotation;
    }
}