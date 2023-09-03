using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

[RequireComponent(typeof(CharacterController))]
public class RewiredIM : MonoBehaviour
{

    public int playerId = 0;
    public float moveSpeed = 2.0f;

    private Player player;
    private CharacterController controller;
    private Vector3 moveVec;
    private bool jump;
    private Vector3 playerVelocity;
    private bool playerGravityEnabled = true;
    private bool groundedPlayer;

    private float gravityValue = -9.81f;

    private Animator animator;

    private void Awake()
    {
        player = ReInput.players.GetPlayer(playerId);
        controller = GetComponent<CharacterController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0.0f)
        {
            playerVelocity.y = 0.0f;
        }
        moveVec.x = player.GetAxis("Movement");
        jump = player.GetButtonDown("Jump");

        if (jump)
        {
            playerVelocity.y += Mathf.Sqrt(1.0f * -3.0f * gravityValue);
            animator.SetBool("isJumping", true);
           //playerGravityEnabled = false;
            StartCoroutine(StopAnim("isJumping", false, 1.0f));
            
        }


        animator.SetFloat("inputMagnitude", moveVec.x, 0.05f, Time.deltaTime);
        if (moveVec.x != 0.0f)
        {
            controller.Move((moveVec).normalized * moveSpeed * Time.deltaTime);
            //controller.SimpleMove(moveVec * moveSpeed);

            // makes the controller face movement direction
            gameObject.transform.forward = moveVec;
            //animator.SetBool("isMoving", true);
        }

        if (playerGravityEnabled)
        {
            playerVelocity.y += gravityValue * Time.deltaTime;
        }


        controller.Move(playerVelocity * Time.deltaTime);
    }

    IEnumerator StopAnim(string anim, bool value, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        animator.SetBool(anim, value);
    }
}