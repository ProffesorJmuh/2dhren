using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public double asd = 0.0;
    private Transform transform;

    [Header("Speeds")]
    public float maxSpeed = 5f;
    public float WalkSpeed = 3f;
    public float JumpForce = 10f;
    public float Acceleration = 5f;

    private int layerGround;

    private Vector3 normalScale;
    private DirectionState _directionState = DirectionState.Right;
    private Transform _transform;
    private Rigidbody2D _rigidbody;



    private bool isGrounded = false;

    void Awake()
    {
        _transform = gameObject.GetComponent<Transform>();
        layerGround = LayerMask.NameToLayer("Ground");
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }


    // Start is called before the first frame update
    void Start()
    {
        normalScale = _transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        var x = Input.GetAxis(axisName: "Horizontal");
        var y = Input.GetAxis(axisName: "Vertical");

        if(x != 0)
        {
            MoveHorizontal(x);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }


    void FixedUpdate()
    {
        
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log(coll.gameObject.layer);

        if (coll.gameObject.layer == layerGround)
        {
            //_animatorController.SetBool("isJump", false);
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        Debug.Log(coll.gameObject.layer);
        if (coll.gameObject.layer == layerGround)
        {
            isGrounded = false;
        }
    }

    public void StandUp()
    {
        _transform.localScale = normalScale;
    }

    public void SitDown()
    {
        _transform.localScale = new Vector3(normalScale.x, normalScale.y / 2, normalScale.z);
    }

    public void MoveHorizontal(float x)
    {
        _rigidbody.AddForce(x * Vector2.right * Acceleration);
        // Ограничение скорости
        var speed = Mathf.Clamp(_rigidbody.velocity.x, -maxSpeed, maxSpeed);
        _rigidbody.velocity = new Vector2(speed, _rigidbody.velocity.y);
    }

    public void MoveVertical(float y)
    {
        if (y > 0) ;
    }


    public void MoveRight()
    {

        if (_directionState == DirectionState.Left)
        {
            ChangeDirection();
        }
        //_animatorController.Play("Walk");
        if (Mathf.Abs(_rigidbody.velocity.x) < maxSpeed)
        {
            _rigidbody.AddForce(Vector2.right * WalkSpeed);
        }

    }

    public void MoveLeft()
    {

        if (_directionState == DirectionState.Right)
        {
            ChangeDirection();
        }
        //_animatorController.Play("Walk");
        if (Mathf.Abs(_rigidbody.velocity.x) < maxSpeed)
        {
            _rigidbody.AddForce(-Vector2.right * WalkSpeed);
        }

    }

    public void ChangeDirection()
    {
        _transform.localScale = new Vector3(-_transform.localScale.x,
            _transform.localScale.y, _transform.localScale.z);
        _directionState = _directionState == DirectionState.Left ?
            DirectionState.Right : DirectionState.Left;
    }

    public void Jump()
    {
        if (isGrounded)
        {
            _rigidbody.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }
    }
    enum DirectionState
    {
        Right,
        Left
    }
}
