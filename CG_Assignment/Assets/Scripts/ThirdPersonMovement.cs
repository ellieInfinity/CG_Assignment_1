using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] Transform trans;
    [SerializeField] Rigidbody body;
    [SerializeField] Animator anim;

    [SerializeField] float collectibleNumber = 0;
    [SerializeField] float maxCollectibleNumber;

    [SerializeField] TMP_Text collectibleText;

    [SerializeField] string sceneName;
    
    private Vector3 playerInput;
    public Transform cam;

    bool jumpInput;

    bool isGrounded;

    public bool hasJumped;

    bool canJump = true;

    [SerializeField] float speed = 6f;

    [SerializeField] float jumpForce;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    // Update is called once per frame
    void Update()
    {
        playerInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;

        if (Input.GetKey(KeyCode.Space))
        {
            jumpInput = true;
        }
        else
        {
            jumpInput = false;
        }

        MovePlayer();

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            //anim.SetBool("isWalking", true);
        }
        else
        {
            //anim.SetBool("isWalking", false);
        }

        MoveCamera();

        if (hasJumped)
        {
            StartCoroutine(wait());

            if (isGrounded)
            {
                //anim.SetTrigger("Land");
                //FindObjectOfType<AudioManager>().Play("Land");
                hasJumped = false;  
                StartCoroutine(waitLand());
            }
        }

        collectibleText.text = "Collected: " + collectibleNumber.ToString() + "/" + maxCollectibleNumber.ToString();

        if (collectibleNumber >= maxCollectibleNumber)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    void FixedUpdate() 
    {
        if (jumpInput && isGrounded && canJump)
        {
            Jump();
        }
    }

    void MovePlayer()
    {
        Vector3 movePlayer = transform.TransformDirection(playerInput) * speed;

        body.velocity = new Vector3 (movePlayer.x, body.velocity.y, movePlayer.z);
    }

    void MoveCamera()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }
    }

    void Jump()
    {
        //anim.SetTrigger("Jumped");
        //FindObjectOfType<AudioManager>().Play("Jump");
        StartCoroutine(waitJump());
        body.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
        hasJumped = true;
        canJump = false;
    }

    private void OnCollisionEnter(Collision collision) 
    {
        for (int i = 0; i < collision.contacts.Length; i++)
        {
            if (collision.contacts[i].normal.y > 0.5)
            {
                isGrounded = true;
            }
        }
    }

    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.tag == "Collect")
        {
            collectibleNumber++;
            Destroy(trigger.gameObject);
        }
    }

    IEnumerator wait ()
    {
        yield return new WaitForSecondsRealtime(0.5f);  
    }

    IEnumerator waitJump ()
    {
        yield return new WaitForSecondsRealtime(1f);  
    }

    IEnumerator waitLand ()
    {
        yield return new WaitForSecondsRealtime(0.2f);  
        canJump = true;
    }
}
