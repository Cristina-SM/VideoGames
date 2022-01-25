using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 12f;
    public float gravity = -19.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public GameObject InGameMenu;

    private Animator animator;
    Vector3 velocity;
    bool isGrounded;
    CharacterController controller;

    private int count;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    bool InGameMenuOpened;



    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        count = 0;
        SetCountText();
        // Set the text property of the Win Text UI to an empty string, making the 'You Win' (game over message) blank
        winTextObject.SetActive(false);
        animator = gameObject.transform.Find("PlayerModel").GetComponent<Animator>();
        InGameMenuOpened = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("q"))
        {
            if (InGameMenuOpened)
            {
                
                InGameMenu.SetActive(false);
                countText.gameObject.SetActive(true);
                Cursor.lockState = CursorLockMode.Locked;
                InGameMenuOpened = false;
            }
            else
            {
                InGameMenu.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                countText.gameObject.SetActive(false);
                InGameMenuOpened = true;


            }
        }
        RaycastHit Hit;
        Physics.Raycast(transform.position, new Vector3(0, -1000, 0), out Hit);

        if (Hit.distance < 0.07f) isGrounded = true;
        else isGrounded = false;

        if (isGrounded && velocity.y < 0) velocity.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (WantsToWalk()) animator.SetBool("isWalking", true);
        else animator.SetBool("isWalking", false);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("pickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 7) winTextObject.SetActive(true);
    }

    bool WantsToWalk()
    {
        return Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d");
    }
}

