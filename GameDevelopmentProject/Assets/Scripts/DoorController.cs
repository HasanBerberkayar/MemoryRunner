using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Vector3 openOffset = new Vector3(4, 0, 0);
    public float speed = 2f;
    private bool shouldOpen = false;
    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = transform.position + openOffset;
    }

    void Update()
    {
        if (shouldOpen)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
        }
    }

    public void OpenDoor()
    {
        shouldOpen = true;
    }
}