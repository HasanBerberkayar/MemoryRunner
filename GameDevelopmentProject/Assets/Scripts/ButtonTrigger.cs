using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public DoorController targetDoor;
    private bool isPlayerNearby = false;
    private bool isPressed = false;
    public GameObject PressEText;
    public GameObject DoorOpenedText;
    public float textTimer;
    void Update()
    {
        if (isPlayerNearby && !isPressed)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PressButton();
            }
        }

        if (DoorOpenedText.active == true && textTimer > 0)
        {
            textTimer -= Time.deltaTime;
        }
        else if (DoorOpenedText.active == true && textTimer <= 0)
        {
            DoorOpenedText.SetActive(false);
        }
    }

    void PressButton()
    {
        isPressed = true;
        GetComponent<Renderer>().material.color = Color.green;
        targetDoor.OpenDoor();
        DoorOpenedText.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PressEText.SetActive(true);
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PressEText.SetActive(false);
            isPlayerNearby = false;
        }
    }
}