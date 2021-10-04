using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _cloudPrefab;
    private GameObject _cameraView;
    // Start is called before the first frame update
    void Start()
    {
        _cameraView = GameObject.Find("Main Camera");
        if (_cameraView == null)
        {
            Debug.LogError("The Cloud Spawn Manager Camera Reference is NULL.");
        }

        StartCoroutine(SpawnClouds());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnClouds()
    {
        while (true)
        {
            Vector2 posToSpawn = new Vector2(_cameraView.transform.position.x + 10f, Random.Range(2.5f, 3.8f));
            GameObject newCloud = Instantiate(_cloudPrefab, posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(7.0f, 9.0f));
        }
    }
}
