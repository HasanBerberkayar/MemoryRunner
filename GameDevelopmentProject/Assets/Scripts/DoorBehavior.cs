using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    public GameObject text;
    private bool canEnter;
    public GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canEnter)
        {
            text.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E)) {
                player.transform.position = new Vector3(270,2,222);
            }
        }
        else {
            text.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("Player"))
        {
            canEnter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name.Equals("Player"))
        {
            canEnter = false;
        }
    }
}
