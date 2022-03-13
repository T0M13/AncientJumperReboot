using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameManager gameManager;
    ScriptableHolder platformList;
    public Transform platformParent;
    public Transform baseGround;

    public List<GameObject> platforms;
    public List<GameObject> despawnedPlatforms;

    public int numberOfPlatforms;
    public int numberOfDespawnedPlatforms;
    public float levelWidth;
    [SerializeField] private float levelWidthOffset = 1.5f;
    public float minY;
    public float maxY;
    public float randomizer;

    [SerializeField] Vector3 spawnPosition = new Vector3();
    [SerializeField] private int platformIndex;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (!gameManager)
        {
            Debug.Log("GameManager NOT found");
        }

        platformList = gameManager.GetComponent<ScriptableHolder>();

        levelWidth = ((gameManager.GetScreenWidth(gameManager.screenWidth) / 2) - levelWidthOffset);
    }

    private void Start()
    {
        for (int i = 0; i < numberOfPlatforms; i++)
        {
            SpawnPlatforms();
        }
    }

    private void Update()
    {
        CheckToReposition();
    }



    public void SpawnPlatforms()
    {
        RandomizePlatforms();

        CreatePlatform();
    }

    public void RandomizePlatforms()
    {
        randomizer = Random.Range(1, 15);
        switch (randomizer)
        {
            case 1:
                platformIndex = (int)PlatformScriptableObject.PlatformTypes.breakable;
                break;
            case 2:
                if (randomizer == platformIndex)
                {
                    RandomizePlatforms();
                }
                else
                {
                    platformIndex = (int)PlatformScriptableObject.PlatformTypes.boosted;
                }
                break;
            case 3:
                if (randomizer == platformIndex)
                {
                    RandomizePlatforms();
                }
                else
                {
                    platformIndex = (int)PlatformScriptableObject.PlatformTypes.spikey;
                }
                break;
            default:
                platformIndex = (int)PlatformScriptableObject.PlatformTypes.normal;
                break;
        }
    }

    public void CreatePlatform()
    {
        GameObject platformPrefab = platformList.platformHolder.platformScriptableObject[platformIndex].platformPrefab;
        spawnPosition.y += Random.Range(minY, maxY);
        spawnPosition.x = Random.Range(-levelWidth, levelWidth);
        GameObject platformClone = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        platforms.Add(platformClone);
        platformClone.transform.parent = platformParent;
    }

    public void DespawnPlatform(GameObject _platform)
    {
        despawnedPlatforms.Add(_platform);
        _platform.SetActive(false);
    }

    public void CheckToReposition()
    {
        if (despawnedPlatforms.Count > numberOfDespawnedPlatforms)
        {
            var randomPlatform = despawnedPlatforms[Random.Range(0, despawnedPlatforms.Count)];
            RepositionPlatform(randomPlatform);
            despawnedPlatforms.Remove(randomPlatform);
        }
    }


    public void RepositionPlatform(GameObject _platform)
    {
        platformIndex = (int)_platform.GetComponent<Platform>().platformObject.platformType;
        _platform.SetActive(true);
        spawnPosition.y += Random.Range(minY, maxY);
        spawnPosition.x = Random.Range(-levelWidth, levelWidth);
        _platform.transform.position = spawnPosition;
        _platform.transform.parent = platformParent;
    }


}
