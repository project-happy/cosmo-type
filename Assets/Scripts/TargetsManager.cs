using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class WordDataList
{
    public List<Word> words;
}

public class TargetsManager : MonoBehaviour
{
    [SerializeField] GameObject enemy;

    [SerializeField] List<GameObject> targets;

    [SerializeField] bool ranodomSpawn;

    public int Count { get { return targets.Count; } }

    public static event Action<GameObject> OnTargetRemoved;


    // Start is called before the first frame update
    void Start()
    {
        SpawnTargets();
    }


    private List<Word> LoadWordsFromFile(string filename)
    {
        // Read the JSON file
        string jsonString = System.IO.File.ReadAllText(filename);

        // Deserialize the JSON data into a list of WordData objects
        List<Word> words = JsonUtility.FromJson<WordDataList>(jsonString).words;
        return words;
    }

    public void SpawnTargets()
    {
        List<Word> words = LoadWordsFromFile("Assets/words-en.json");
        targets = new List<GameObject>();
        StartCoroutine(SpawnRoutine(words));
    }



    int _targetsAllowed = 1;
    IEnumerator SpawnRoutine(List<Word> words)
    {
        GameObject obj;
        TextType textType;
        int delay = 1;

        foreach (Word word in words)
        {
            obj = Instantiate(enemy, new Vector3(UnityEngine.Random.Range(1, 11), UnityEngine.Random.Range(1, 11)), Quaternion.identity);
            textType = obj.GetComponent<TextType>();
            textType.SetWords(new List<Word> { word });
            targets.Add(obj);

            yield return new WaitUntil(() => targets.Count != _targetsAllowed);
        }
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
        OnTargetRemoved?.Invoke(gameObject);
        _targetsAllowed++;
    }


}