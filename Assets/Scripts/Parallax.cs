using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float _length;
    private float _startPos;
    [SerializeField] private float _parallaxEffect;
    [SerializeField] private GameObject _cam;
    
    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (_cam.transform.position.x * (1 - _parallaxEffect));
        float dist = (_cam.transform.position.x * _parallaxEffect);

        transform.position = new Vector2(_startPos + dist, transform.position.y);

        if (temp > _startPos + _length)
        {
            _startPos += _length;
        }
        else if (temp < _startPos - _length)
        {
            _startPos -= _length;
        }
    }
}
