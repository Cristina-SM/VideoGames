using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 12f;
    public float gravity = -19.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public Animator animator;
    Vector3 velocity;
    bool isGrounded;
    CharacterController controller;
	
    private int count;
    public TextMeshProUGUI countText;
	public GameObject winTextObject;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        count = 0;
		SetCountText();
        // Set the text property of the Win Text UI to an empty string, making the 'You Win' (game over message) blank
        winTextObject.SetActive(false);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit Hit;
        Physics.Raycast(transform.position, new Vector3(0,-1000,0), out Hit);

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
        animator.SetFloat("speed", 1f);
    }
    void OnTriggerEnter(Collider other) 
	{
		// ..and if the GameObject you intersect has the tag 'Pick Up' assigned to it..
		if (other.gameObject.CompareTag("pickUp"))
		{
			other.gameObject.SetActive (false);

			// Add one to the score variable 'count'
			count = count + 1;

			// Run the 'SetCountText()' function (see below)
			SetCountText ();
		}
	}
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

		if (count >= 3) winTextObject.SetActive(true);
    }
}

