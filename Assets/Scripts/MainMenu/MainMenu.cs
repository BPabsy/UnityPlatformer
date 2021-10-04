using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _cloudPrefab;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _levelSelect;
    [SerializeField] private GameObject _controls;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnClouds());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && _levelSelect.activeInHierarchy)
        {
            _levelSelect.SetActive(false);
            _mainMenu.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Escape) && _controls.activeInHierarchy)
        {
            _controls.SetActive(false);
            _mainMenu.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && _mainMenu.activeInHierarchy)
        {
            Application.Quit();
            Debug.Log("QUIT");
        }
    }

    IEnumerator SpawnClouds()
    {
        while(true)
        {
            Vector2 posToSpawn = new Vector2(11.8f, Random.Range(-0.75f, 4.3f));
            GameObject newCloud = Instantiate(_cloudPrefab, posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(5.0f, 8.0f));
        }
    }

    public void SelectLevel(string levelName)
    {
        switch(levelName)
        {
            case "W1L1":
                SceneManager.LoadScene("World1_Level1");
                break;
            case "W1L2":
                SceneManager.LoadScene("Test_Level");
                break;
            case "W1L3":
                Debug.Log("Load W1L3");
                break;
            case "W2L1":
                Debug.Log("Load W2L1");
                break;
            case "W2L2":
                Debug.Log("Load W2L2");
                break;
            case "W2L3":
                Debug.Log("Load W2L3");
                break;
            case "W3L1":
                Debug.Log("Load W3L1");
                break;
            case "W3L2":
                Debug.Log("Load W3L2");
                break;
            case "W3L3":
                Debug.Log("Load W3L3");
                break;
        }
    }
}
