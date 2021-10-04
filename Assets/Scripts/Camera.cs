using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private GameObject _player;
    [SerializeField] private float xMin;
    [SerializeField] private float xMax;
    [SerializeField] private float yMin;
    [SerializeField] private float yMax;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
        if(_player == null)
        {
            Debug.LogError("The Player is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float x = Mathf.Clamp(_player.transform.position.x, xMin, xMax);
        float y = Mathf.Clamp(_player.transform.position.y, yMin, yMax);

        transform.position = new Vector3(x + 2, y, transform.position.z);
    }
}
