using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement : MonoBehaviour
{
    private float _speed = 35f;
    private float _horizonatalInput;
    private Rigidbody2D _rb;
    private float _verticalInput;

    // Start is called before the first frame update
    void Start()
    {
        //assign the rigidbody component
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //Inputs and velocity
        _horizonatalInput = Input.GetAxisRaw("Horizontal");
        float horizontalMovement = _horizonatalInput * _speed * Time.deltaTime;
        _rb.velocity = new Vector2(horizontalMovement, _rb.velocity.y);

        _verticalInput = Input.GetAxisRaw("Vertical");
        float verticalMovement = _verticalInput * _speed * Time.deltaTime;
        _rb.velocity = new Vector2(_rb.velocity.x, verticalMovement);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
