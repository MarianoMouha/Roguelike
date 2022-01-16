using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform bowRotation;
    private Vector2 movementDirection, cameraPos, aimPosition;
    public Vector2 lookDir;
    public float movementspeed, ismoving;
    public Camera cam;
    public Animator animator;
    public Shooting shooting;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shooting = GetComponent<Shooting>();
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInputs();

        Animate();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleInputs()
    {
        movementDirection.x = Input.GetAxisRaw("Horizontal");
        movementDirection.y = Input.GetAxisRaw("Vertical");

        ismoving = movementDirection.magnitude;

        if (Input.GetButtonDown("Fire1"))
        {
            shooting.Shoot();
        }

        cameraPos = cam.ScreenToWorldPoint(Input.mousePosition);
        aimPosition = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }
    void HandleMovement()
    {
        rb.MovePosition(rb.position + movementDirection * movementspeed * Time.fixedDeltaTime);

        lookDir = cameraPos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg +90;
        bowRotation.rotation = Quaternion.Euler(0, 0, angle);
    }
    void Animate()
    {
        if (cameraPos != Vector2.zero)
        {
            animator.SetFloat("Horizontal", aimPosition.x);
            animator.SetFloat("Vertical", aimPosition.y);
        }        
        animator.SetFloat("Speed", ismoving);
    }

}
