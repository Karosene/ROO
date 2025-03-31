using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolicemanScript : MonoBehaviour
{
    public float speed;

    private Vector2 _dir;
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        _dir.x = Input.GetAxis("Horizontal");  

        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            GetComponent<SpriteRenderer>().transform.localScale = new Vector3( 1, 1);
        } else if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)){
            GetComponent<SpriteRenderer>().transform.localScale = new Vector3( -1, 1);
        }
    }

    private void FixedUpdate()
    {
        if(_dir.x == 0) {
            _rb.velocity = Vector2.zero;
        }
        else{
            _rb.AddForce(_dir * speed);
        }
    }
}
