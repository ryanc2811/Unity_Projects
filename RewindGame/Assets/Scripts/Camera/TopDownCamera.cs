using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    public Transform target;
    [SerializeField]
    private float height = 30f;
    [SerializeField]
    private float distance = 40f;
    [SerializeField]
    private float angle = 45f;
    
    private Vector3 refVelocity;
    [SerializeField]
    public float smoothSpeed = .5f;

    // Start is called before the first frame update
    void Start()
    {
        HandleCamera();
    }

    // Update is called once per frame
    void Update()
    {
        HandleCamera();
    }
    protected virtual void HandleCamera()
    {
        if(!target)
        {
            return;
        }
        
        Vector3 worldPosition = (Vector3.forward * -distance + Vector3.up * height);
        Vector3 rotateVector = Quaternion.AngleAxis(angle, Vector3.up)*worldPosition;
        Vector3 flatTargetPosition = target.position;
        flatTargetPosition.y = 0f;
        Vector3 finalPosition = flatTargetPosition + rotateVector;
        transform.position = Vector3.SmoothDamp(transform.position,finalPosition,ref refVelocity, smoothSpeed);
        transform.LookAt(target.position);
    }   
}
