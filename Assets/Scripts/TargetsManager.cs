using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.GraphicsBuffer;

[System.Serializable]
public class WordDataList
{
    public List<InnerWord> words;
}

[System.Serializable]
public class InnerWord
{
    public List<Word> word;
    public EnemyType type;
}

public enum EnemyType
{
    REGULAR,
    BOSS,
}

public enum ModeType
{
    [Description("normal-mode")]
    NORMAL,

    [Description("learning-mode")]
    LEARNING,

    [Description("story-mode")]
    STORY,

    [Description("tutorial")]
    TUTORIAL,
}

public class TargetsManager : MonoBehaviour
{
    [SerializeField]
    Transform player;

    [SerializeField]
    GameObject enemy;

    [SerializeField]
    List<GameObject> targets;

    [SerializeField]
    int currentParrallelEnemiesLimit = 6;

    [SerializeField]
    private List<GameObject> targets;
    public int Count
    {
        get { return targets.Count; }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (mode == ModeType.TUTORIAL)
            return;
        targets = new List<GameObject>();
        StartCoroutine(SpawnTargets());
    }

    // Loading words from json file
    private IEnumerator LoadWordsFromFile(string filename)
    {
        filename = string.Join(
            "/",
            Application.streamingAssetsPath,
            "JsonFiles",
            mode.DisplayName(),
            filename
        );

        // In the Unity Editor, you need to use a different file access method

#if UNITY_EDITOR
        Debug.Log(filename);
        // Read the JSON file
        string jsonString = System.IO.File.ReadAllText(filename);

#else
        // In a built game, you need to use Unity's WWW or UnityWebRequest classes to read the file
        UnityWebRequest www = UnityWebRequest.Get(filename);
        yield return www.SendWebRequest();

        string jsonString = www.downloadHandler.text;
#endif

        // Deserialize the JSON data into a list of WordData objects
        yield return loadedWords = JsonUtility.FromJson<WordDataList>(jsonString).words;
        if (shuffleWords)
            loadedWords = loadedWords.Shuffle();
    }

    private IEnumerator SpawnTargets()
    {
        while (true)
        {
            // display wave started
            isUIMessageDiplayed = true;
            StartCoroutine(UpdaeUI($"Wave {wave} started"));
            yield return new WaitUntil(() => !isUIMessageDiplayed);

            // load words
            StartCoroutine(LoadWordsFromFile($"wave-{wave}.json"));
            yield return new WaitUntil(() => loadedWords != null);

            // spawn enemies
            StartCoroutine(Spawn());
            yield return new WaitUntil(() => loadedWords.Count == 0);
            yield return new WaitUntil(() => targets.Count == 0);

            // show animation for ending rouund and wait
            yield return new WaitForSeconds(3);

            loadedWords = null;
            wave++;
            spawnDelay = Mathf.Clamp(spawnDelay - .4f, minSpawnDelay, float.MaxValue);
            currentParrallelEnemiesLimit = Mathf.Clamp(
                currentParrallelEnemiesLimit + 2,
                1,
                maxParrallelEnemies
            );
        }
    }

    IEnumerator UpdaeUI(string message)
    {
        List<Word> words = await LoadWordsFromFile("words-en.json");
        targets = new List<GameObject>();
        StartCoroutine(SpawnRoutine(words));
    }

    int _targetsAllowed = 1;

    IEnumerator SpawnRoutine(List<Word> words)
    {
        GameObject obj;
        TextType textType;
        Mover mover;
        obj = Instantiate(GetEnemyByType(type), positon, Quaternion.identity);
        mover = obj.GetComponent<Mover>();
        mover.playerTransform = player;
        textType = obj.GetComponent<TextType>();
        textType.SetWords(words);
        targets.Add(obj);
        return obj;
    }

    // find the next target with min distance to the gameobject that starts with char that we first typed
#nullable enable
    public GameObject? FindTarget(GameObject gameObj, char searchChar)
    {
        MeshText enemyText;
        float minDist = float.MaxValue;
        GameObject? target = null;

        foreach (GameObject enemy in targets)
        {
            enemyText = enemy.GetComponentInChildren<MeshText>();
            float dist = Vector3.Distance(gameObj.transform.position, enemy.transform.position);
            if (enemyText.Length > 0 && enemyText.FirstChar() == searchChar && dist < minDist)
            {
                target = enemy;
                minDist = dist;
            }
        }

        return target;
    }

    public void RemoveTarget(GameObject gameObject)
    {
        targets.Remove(gameObject);
    }
}
