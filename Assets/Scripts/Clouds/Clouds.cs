using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    [SerializeField] private float _speed = 0.5f;
    private GameObject _cameraView;
    // Start is called before the first frame update
    void Start()
    {
        _cameraView = GameObject.Find("Main Camera");
        if (_cameraView == null)
        {
            Debug.LogError("The Cloud Camera Reference is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CloudMovement();
    }

    public void CloudMovement()
    {
        transform.Translate(Vector2.left * _speed * Time.deltaTime);
        if (transform.position.x < _cameraView.transform.position.x - 30.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
