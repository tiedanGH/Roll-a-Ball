using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Renderer playerRenderer;
    public float speed;
    public int winNeed;

    enum Door
    {
        WEST,
        EAST,
        NORTH,
        SOUTH,
    };
    public GameObject westDoor;
    public GameObject eastDoor;
    public GameObject northDoor;
    public GameObject southDoor;

    public GameObject winPickup;

    public Text countText;
    public Text winText;

    public float gravityStrength = 9.8f;
    
    private Rigidbody rb;

    private int count;
    private int progress;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = progress = 0;
        SetCountText();
        SetWinText("");
    }
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(h, 0f, v);
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pick Up")) {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
            CheckDoorProgress();
        }

        if (other.CompareTag("Win"))
        {
            other.gameObject.SetActive(false);
            SetWinText("You Win!");
        }

        if (other.CompareTag("NorthGravity"))
        {
            EnterNorthWall();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NorthGravity"))
        {
            ExitNorthWall();
        }
    }

    void EnterNorthWall()
    {
        Physics.gravity = new Vector3(0f, 0f, gravityStrength * -1f);
        rb.useGravity = true;
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }

    void ExitNorthWall()
    {
        Physics.gravity = new Vector3(0f, -gravityStrength, 0f);
        transform.rotation = Quaternion.identity;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count == winNeed)
        {
            winPickup.SetActive(true);
        }
    }

    void SetWinText(string text)
    {
        winText.text = text;
    }

    void CheckDoorProgress()
    {
        if (count < (progress + 1) * 8) return;

        switch (progress)
        {
            case 0:
                OpenDoor(Door.WEST, new Color(1f, 0.55f, 0.1f));
                break;
            case 1:
                OpenDoor(Door.EAST, new Color(0.6f, 0.35f, 1f));
                break;
            case 2:
                OpenDoor(Door.NORTH, new Color(0.78f, 0.53f, 0.26f));
                break;
            case 3:
                OpenDoor(Door.SOUTH, new Color(0.9f, 0.22f, 0.2f));
                break;
        }

        progress++;
    }

    void OpenDoor(Door door, Color playerColor)
    {
        switch (door)
        {
            case Door.WEST:
                westDoor.SetActive(false);
                break;
            case Door.EAST:
                eastDoor.SetActive(false);
                break;
            case Door.NORTH:
                northDoor.SetActive(false);
                break;
            case Door.SOUTH:
                southDoor.SetActive(false);
                break;
        }

        ChangePlayerColor(playerColor);
    }

    void ChangePlayerColor(Color color)
    {
        playerRenderer.material.color = color;
    }
}
