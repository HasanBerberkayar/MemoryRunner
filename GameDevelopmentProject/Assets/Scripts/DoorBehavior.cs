using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    bool canEnter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        canEnter = true;
    }

    private void OnTriggerExit(Collider other)
    {
        canEnter = false;
    }
}
