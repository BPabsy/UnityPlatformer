using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Player _player;
    private bool _collected = false;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("The Player is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && this.tag == "BronzeCoin" && _collected == false)
        {
            _collected = true;            
            _player.AddScore(1);
            Destroy(this.gameObject);
        }
        if (other.tag == "Player" && this.tag == "SilverCoin" && _collected == false)
        {
            _collected = true;
            _player.AddScore(5);
            Destroy(this.gameObject);
        }
        if (other.tag == "Player" && this.tag == "GoldCoin" && _collected == false)
        {
            _collected = true;
            _player.AddScore(10);
            Destroy(this.gameObject);
        }
    }
}
