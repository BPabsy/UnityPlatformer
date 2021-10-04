using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Vector2 _startPos;
    [SerializeField] private float _speed;
    [SerializeField] private float _runSpeedIncrease = 2.0f;
    [SerializeField] private float _jump = 8.5f;
    [SerializeField] private int _lives = 3;
    private float _buttonTime = 0.3f;
    private float _jumpTime;
    private bool _jumping;
    [SerializeField] private int _score = 0;
    [SerializeField] private bool _isGrounded = false;
    private Rigidbody2D _rb;
    private UIManager _uiManager;
    private Animator _anim;
    private SpriteRenderer _spriteRenderer;
    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _speed = 5.5f;

        _rb = GetComponent<Rigidbody2D>();
        if(_rb == null)
        {
            Debug.LogError("The Player RigidBody2D is NULL.");
        }

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL.");
        }

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("The Game Manager is NULL.");
        }

        _anim = GetComponent<Animator>();
        if (_anim == null)
        {
            Debug.LogError("The Player Animator is NULL.");
        }

        _spriteRenderer = GetComponent<SpriteRenderer>();
        if(_spriteRenderer == null)
        {
            Debug.LogError("The Player SpriteRenderer is NULL.");
        }

        //STARTING POSITION
        _startPos = new Vector2(-7.0f, -3.488f);
        transform.position = _startPos; 
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        LivesCount();
    }

    void Movement()
    {
        float directionPC = Input.GetAxis("Horizontal");
        //PLAYER INPUT

#if UNITY_ANDROID
        float direction = CrossPlatformInputManager.GetAxis("Horizontal");
        //WALK
        _rb.velocity = new Vector2(direction * _speed, _rb.velocity.y);
        _anim.SetFloat("VelocityX", _rb.velocity.x);
        if (_rb.velocity.x > 0)
        {
            _spriteRenderer.flipX = false;
        }

        if (_rb.velocity.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
        //JUMP
        if (CrossPlatformInputManager.GetButtonDown("Jump") && _isGrounded == true)
        {
             _rb.velocity = new Vector2(_rb.velocity.x, _jump * 2);
            _anim.SetBool("Grounded", false);
        }

#endif

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            _rb.velocity = new Vector2(directionPC * _speed, _rb.velocity.y);
            _anim.SetFloat("VelocityX", _rb.velocity.x);
            if(_rb.velocity.x > 0)
            {
                _spriteRenderer.flipX = false;
            }

            if (_rb.velocity.x < 0)
            {
                _spriteRenderer.flipX = true;
            }
        }
        else
        {
            _anim.SetFloat("VelocityX", _rb.velocity.x);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _speed += _runSpeedIncrease;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _speed -= _runSpeedIncrease;
        }

        //VARIABLE JUMP LOGIC
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded == true)
        {
            _jumping = true;
            _jumpTime = 0;
        }
        if(_jumping)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jump);
            _jumpTime += Time.deltaTime;
            _anim.SetBool("Grounded", false);
        }
        if(Input.GetKeyUp(KeyCode.Space) || _jumpTime > _buttonTime)
        {
            _jumping = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //CHECK IF PLAYER IS IN CONTACT WITH GROUND, JUMP ENABLED
        if(other.tag == "Ground")
        {
            _isGrounded = true;
            _anim.SetBool("Grounded", true);
        }
        //CHECK IF PLAYER HAS FALLEN IN WATER
        if(other.tag == "Water")
        {
            --_lives;
            transform.position = _startPos;
        }
        //CHECK IF PLAYER HAS HIT ENEMY
        if(other.tag == "Enemy")
        {
            --_lives;

        }
        //CHECK IF PLAYER HAS REACHED GOAL
        if(other.tag == "Finish")
        {
            _speed = 0f;
            _runSpeedIncrease = 0f;
            _gameManager.LevelEnd();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        //CHECK IF PLAYER HAS LEFT GROUND, JUMP DISABLED
        if(other.tag == "Ground")
        {
            _isGrounded = false;
        }
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

    public void LivesCount()
    {
        _uiManager.UpdateLives(_lives);
        if(_lives == 0)
        {
            Debug.Log("Lives = 0 RESTART LEVEL");
            _lives = 3;
        }
    }

}
