using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Speeds")]
    public float maxSpeed = 8f;
    public float WalkSpeed = 2f;
    public float JumpForce = 5f;
    public float Acceleration = 5f;
    public float AccelInJump = 3f;


    private int layerGround;

    private Vector3 normalScale;
    private DirectionState _directionState = DirectionState.Right;
    private Transform _transform;
    private Rigidbody2D _rigidbody;

    private bool isSitting = false;

    private bool isGrounded = false;


    // ставим нач значения
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

        MoveHorizontal(x);
        MoveVertical(y);

    }


    void FixedUpdate()
    {

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        // если касаемся земли, то поднимаем флаг

        if (coll.gameObject.layer == layerGround)
        {
            //_animatorController.SetBool("isJump", false);
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        // если не касаемся земли, то убираем флаг
        if (coll.gameObject.layer == layerGround)
        {
            isGrounded = false;
        }
    }

    public void Jump()
    {
        // если не в воздухе
        if (isGrounded)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, JumpForce);        // конечная скорость
        }
    }

    public void StandUp()
    {
        _transform.localScale = normalScale;                                    // возвращаемся к нормальной высоте
        if (isSitting && isGrounded) _transform.position += normalScale.y / 4 * Vector3.up;   // если объкт сидел, то поднимаем объект

        isSitting = false;                                                      // убираем флаг сидячего положения
    }

    public void SitDown()
    {
        _transform.localScale = new Vector3(normalScale.x, normalScale.y / 2, normalScale.z);   // уменьшаем высоту

        if (!isSitting && isGrounded) _transform.position -= normalScale.y / 4 * Vector3.up;    // если объкт стоял, то опускаем объект

        isSitting = true;                                                                       // поднимаем флаг сидячего положения
    }

    public void MoveHorizontal(float x)
    {
        float speed;                                                        // текущая скорость персонажа
        _rigidbody.AddForce(x * (isGrounded ? Acceleration: AccelInJump) * Vector2.right * WalkSpeed);  // добавляем силу
        // Ограничение скорости
        if (isSitting)
            // в присяде
            speed = Mathf.Clamp(_rigidbody.velocity.x, -WalkSpeed, WalkSpeed);
        else
            // в обычном состоянии
            speed = Mathf.Clamp(_rigidbody.velocity.x, -maxSpeed, maxSpeed);
        
        _rigidbody.velocity = new Vector2(speed, _rigidbody.velocity.y);    // конечная скорость
    }

    public void MoveVertical(float y)
    {
        if (y > 0) Jump();                  // прыгаем
        else if (y < 0) SitDown();          // приседаем

        if (isSitting && y >= 0) StandUp(); // если не сидим, то встаём
    }


    /*public void MoveRight()
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
*/
    
    enum DirectionState
    {
        Right,
        Left
    }
}
