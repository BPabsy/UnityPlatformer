using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 1.0f;
    private int _direction;
    private int _idleTime;
    private Rigidbody2D _rb;
    private float _startPosX;
    private float _currentPosX;
    private Animator _anim;
    private SpriteRenderer _spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (_rb == null)
        {
            Debug.LogError("The Enemy RigidBody2D is NULL.");
        }

        _anim = GetComponent<Animator>();
        if (_anim == null)
        {
            Debug.LogError("The Enemy Animator is NULL.");
        }

        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer == null)
        {
            Debug.LogError("The Enemy SpriteRenderer is NULL.");
        }

        _startPosX = transform.position.x;

        StartCoroutine(MoveEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        _currentPosX = transform.position.x;
    }

    IEnumerator MoveEnemy()
    {
        while(true)
        {
            yield return new WaitForSeconds(2);
            _direction = Random.Range(0, 2);
            _idleTime = Random.Range(1, 3);
            Debug.Log(_direction);

            switch(_direction)
            {
                case 0:
                    if (_currentPosX > _startPosX - 1.5f)
                    {
                        _spriteRenderer.flipX = false;
                        _rb.velocity = new Vector2(-1 * _speed, _rb.velocity.y);
                        _anim.SetBool("Walking", true);
                        yield return new WaitForSeconds(_idleTime);
                        _rb.velocity = new Vector2(0 * _speed, _rb.velocity.y);
                        _anim.SetBool("Walking", false);
                    }
                    break;
                case 1:
                    if (_currentPosX < _startPosX + 1.5f)
                    {
                        _spriteRenderer.flipX = true;
                        _rb.velocity = new Vector2(1 * _speed, _rb.velocity.y);
                        _anim.SetBool("Walking", true);
                        yield return new WaitForSeconds(_idleTime);
                        _rb.velocity = new Vector2(0 * _speed, _rb.velocity.y);
                        _anim.SetBool("Walking", false);
                    }
                    break;
            }
        }
    }
}
