using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Chest : MonoBehaviour
{
    private Player _player;
    private Animator _openAnim;
    private Animator _revealTreasure;
    [SerializeField] private GameObject _treasure;
    private bool _collected = false;
    private bool _chestInRange = false;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.LogError("The Player is NULL.");
        }

        _openAnim = GetComponent<Animator>();
        if(_openAnim == null)
        {
            Debug.LogError("The Chest Animator is NULL.");
        }

        _revealTreasure = GameObject.Find("Treasure").GetComponent<Animator>();
        if(_revealTreasure == null)
        {
            Debug.LogError("The Treasure Animator is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_ANDROID
        if (_chestInRange && CrossPlatformInputManager.GetButtonDown("Interact") && _collected == false)
        {
            OpenChest();
        }
#endif
        if (_chestInRange && Input.GetKeyDown(KeyCode.E) && _collected == false)
        {
            OpenChest();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _chestInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _chestInRange = false;
        }
    }

    public void OpenChest()
    {
        _collected = true;
        _openAnim.SetTrigger("OpenChest");
        _revealTreasure.SetTrigger("RevealTreasure");
        _player.AddScore(10);
        StartCoroutine(DestroyOpenChest());
    }

    IEnumerator DestroyOpenChest()
    {
        var rendererChest = this.GetComponent<SpriteRenderer>();
        var rendererTreasure = _treasure.GetComponent<SpriteRenderer>();
        yield return new WaitForSeconds(2.5f);
        rendererChest.enabled = !rendererChest.enabled;
        rendererTreasure.enabled = !rendererTreasure.enabled;
        yield return new WaitForSeconds(0.2f);
        rendererChest.enabled = !rendererChest.enabled;
        rendererTreasure.enabled = !rendererTreasure.enabled;
        yield return new WaitForSeconds(0.3f);
        rendererChest.enabled = !rendererChest.enabled;
        rendererTreasure.enabled = !rendererTreasure.enabled;
        yield return new WaitForSeconds(0.2f);
        rendererChest.enabled = !rendererChest.enabled;
        rendererTreasure.enabled = !rendererTreasure.enabled;
        yield return new WaitForSeconds(0.3f);
        Destroy(this.gameObject);
    }
}
