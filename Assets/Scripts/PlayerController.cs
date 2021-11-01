using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;

    private int desiredLane = 1;  // 0: left, 1: middle, 2: right
    public float laneDistance = 3;  // distance between two lanes

    public float jumpForce;
    public float Gravity = -20.0f;
    public Animator animator;

    public SwipeManager swipeManager;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        direction.z = forwardSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerManager.isGameStarted)
        {
            return;
        }

        animator.SetBool("isGameStarted", true);

        forwardSpeed += 0.1f * Time.deltaTime;

        if (controller.isGrounded)
        {
            if (swipeManager.SwipeUp)
            {
                Jump();
            }
        }
        else
        {
            direction.y += Gravity * Time.deltaTime;
        }

        if (swipeManager.SwipeDown)
        {
            StartCoroutine(Slide());
        }

        // Gather the inputs on which lane we should be
        if (swipeManager.SwipeRight)
        {
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;
        }

        if (swipeManager.SwipeLeft)
        {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
        }

        // Calculate where we should be in the future

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, 80 * Time.deltaTime);
        
    }

    private void FixedUpdate()
    {
        Physics.SyncTransforms();
        controller.Move(direction * Time.fixedDeltaTime);
    }


    private void Jump()
    {
        direction.y = jumpForce;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
            PlayerManager.isGameStarted = false;
        }
    }

    private IEnumerator Slide()
    {
        animator.SetBool("isSliding", true);
        controller.center = new Vector3(0, -0.5f, 0);
        controller.height = 1;
        yield return new WaitForSeconds(1.3f);
        controller.center = Vector3.zero;
        controller.height = 2;
        animator.SetBool("isSliding", false);
    }
}
