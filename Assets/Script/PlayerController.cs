using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float walkSpeed = 5.0f;
    public float gravity = -9.81f;
    public float minJumpHeight = 2.0f;
    public float runSpeed = 8.0f;
    public float maxJumpHeight = 4.0f;

    bool isGrounded;
    bool isJumping;
    CharacterController controller;
    float vertical,Timer;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Timer = 0 ;


    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;

        Vector3 movement = transform.right * Input.GetAxisRaw("Horizontal") +
        transform.forward * Input.GetAxisRaw("Vertical");

        if (isGrounded)
        {
            isJumping = false;
            if(Input.GetKeyDown(KeyCode.Space))
            {
                isJumping = true;
                vertical = Mathf.Sqrt(-2f * gravity * minJumpHeight);
                Timer = 0;        
            }
            else
            {
                vertical = -10f;
            }
        }
        else
        {
            if(isJumping)
            {
                vertical = Mathf.Sqrt(-3f * gravity * minJumpHeight);
                Timer += Time.deltaTime;
            if (Timer > (maxJumpHeight - minJumpHeight) / Mathf.Sqrt(-2f * gravity * minJumpHeight) || !Input.GetKey(KeyCode.Space))
            {
                   isJumping = false;
            }
            }
            else
            {
                 vertical += gravity * Time.deltaTime;
            }
            }
         

        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        
        Vector3 velocity = movement.normalized * speed + vertical * Vector3.up;
        controller.Move(velocity * Time.deltaTime);

    }
}
