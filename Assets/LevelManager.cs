using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public CanvasGroup canvas;
    public Slider progressBar;
    int loadingProgress = 0;
    bool loading = true;

    bool dungeonLoaded;
    bool roomsOptimised;
    bool navmeshGenerated;
    bool aiGenerated;
    bool processActive;
    bool end;

    int levelNumber;

    double time;

    LevelGenerator levelGenerator;
    LevelNavMeshBuilder levelNavMeshBuilder;
    LevelDifficultyManager levelDifficultyManager;

    // Start is called before the first frame update
    void Awake()
    {
        levelGenerator = gameObject.GetComponent<LevelGenerator>();
        levelNavMeshBuilder = gameObject.GetComponent<LevelNavMeshBuilder>();
        levelDifficultyManager = gameObject.GetComponent<LevelDifficultyManager>();
    }

    // Update is called once per frame
    void Update()
    {


        if (loading == true)
        {
            //progressBar.value += Mathf.Clamp01(loadingProgress * 0.1f);
        }
        if (dungeonLoaded == false)
        {
            GenerateLevel();
        }

        if (navmeshGenerated == false && dungeonLoaded == true)
        {
            GenerateNavmesh();
            navmeshGenerated = true;
        }

        if (navmeshGenerated && end == false)
        {
            end = true;
            EndLoading();
        }
    }


    void GenerateLevel()
    {
        System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
        stopWatch.Start();
        Debug.Log("Generating Dungeon");

        levelGenerator.GenerateNewLevel();
        dungeonLoaded = true;
        stopWatch.Stop();
        Debug.Log(time = stopWatch.Elapsed.TotalMilliseconds);
        loadingProgress = 20;
    }
    void OptimiseRooms()
    {
        loadingProgress = 40;
        Debug.Log("Optimising Rooms");
        roomsOptimised = true;
        //meshCombiner.CombineAll();
    }
    void GenerateNavmesh()
    {
        loadingProgress = 60;
        Debug.Log("Generating Navmesh");
        levelNavMeshBuilder.BuildNavMesh();

    }
    void EndLoading()
    {

        GameManager.instance.sceneLoader.OnLevelGenerated();
        gameObject.SetActive(false);

        loadingProgress = 100;
        Debug.Log("Ending Loading screen");
        //StartCoroutine(FadeLoadingScreen(2));
    }

    IEnumerator FadeLoadingScreen(float duration)
    {
        float startValue = canvas.alpha;
        float time = 0;

        while (time < duration)
        {
            canvas.alpha = Mathf.Lerp(startValue, 0, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        canvas.alpha = 0;
        canvas.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
