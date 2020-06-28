using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header ("Stats")]
    public float moveSpeed;
    public float bulbThrowPower;
    [Space]
    [Header("Insertables")]
    public GameObject bulbPrefab;
    private Rigidbody2D rigidBody;
    private Vector3 inputChange;
    private Animator animator;
    private Vector3 mousePositionRelativeToPlayer = Vector3.zero;
    [HideInInspector] public bool hasBulb;


    private void Start()
    {
        hasBulb = true;
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckInputs();
        AnimateAndMovePlayer();
    }
    private void AnimateAndMovePlayer()
    {
        //Checking mouse position relative to player
        Vector3 mouseScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        mousePositionRelativeToPlayer = mouseWorldPosition - transform.position;

        //Changing face direction of player character
        animator.SetFloat("mouseX", mousePositionRelativeToPlayer.x);
        animator.SetFloat("mouseY", mousePositionRelativeToPlayer.y);

        if (inputChange != Vector3.zero)
        {
            MovePlayer();
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    private void MovePlayer()
    {
        rigidBody.MovePosition(transform.position + inputChange * moveSpeed * Time.deltaTime);
    }
    private void CheckInputs()
    {
        inputChange = Vector3.zero;
        inputChange.x = Input.GetAxis("Horizontal");
        inputChange.y = Input.GetAxis("Vertical");

        if (Input.GetMouseButtonDown(0))
        {
            AttemptThrowBulb();
        }
    }
    private void AttemptThrowBulb()
    {
        if (!hasBulb) { return; }
        //Sets direction and force of bulb
        GameObject bulbObj = Instantiate(bulbPrefab, transform.position, Quaternion.identity);
        Vector3 directionToThrow = mousePositionRelativeToPlayer.normalized; //TODO MAKE THIS WORK
        bulbObj.GetComponent<Rigidbody2D>().AddForce(directionToThrow * bulbThrowPower);
        hasBulb = false;
    }
}
