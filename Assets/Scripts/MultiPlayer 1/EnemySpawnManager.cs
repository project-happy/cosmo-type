using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using System;
using System.Threading.Tasks;

/*[System.Serializable]
public class WordDataList
{
    public List<Word> words;
}*/

public class EnemySpawnManager : MonoBehaviour
{

    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject enemy;

    [SerializeField] List<GameObject> targets;


    [SerializeField] bool ranodomSpawn;


    [SerializeField] private PhotonView photonView;



    public int Count { get { return targets.Count; } }


    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            SpawnTargets();
        }

    }


    private async Task<List<Word>> LoadWordsFromFile(string filename)
    {
        filename = string.Join("/", Application.streamingAssetsPath, filename);

        // In the Unity Editor, you need to use a different file access method

#if UNITY_EDITOR

        // Read the JSON file
        string jsonString = System.IO.File.ReadAllText(filename);

#else
        // In a built game, you need to use Unity's WWW or UnityWebRequest classes to read the file
        UnityWebRequest www = UnityWebRequest.Get(filename);
        var operation = www.SendWebRequest();

        while (!operation.isDone)
            await Task.Yield();

        string jsonString = www.downloadHandler.text;
#endif


        // Deserialize the JSON data into a list of WordData objects
        return JsonUtility.FromJson<WordDataList>(jsonString).words;
    }

    //private List<Word> LoadWordsFromFile(string filename)
    //{
    //    // Read the JSON file
    //    string jsonString = System.IO.File.ReadAllText(filename);

    //    // Deserialize the JSON data into a list of WordData objects
    //    List<Word> words = JsonUtility.FromJson<WordDataList>(jsonString).words;
    //    return words;
    //}

    public async Task SpawnTargets()
    {
        List<Word> words = await LoadWordsFromFile("words-en.json");
        targets = new List<GameObject>();
        StartCoroutine(SpawnRoutine(words));
    }




    int _targetsAllowed = 1;
    IEnumerator SpawnRoutine(List<Word> words)
    {


        GameObject obj;
        TextTypeNetwork textType;
        int delay = 1;

        foreach (Word word in words)
        {
            Vector3 randomPostion = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)].position;
            obj = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "EnemyNetWork"), randomPostion, Quaternion.identity);

            textType = obj.GetComponent<TextTypeNetwork>();
            textType.SetWords(new List<Word> { word });
            /*     targets.Add(obj);*/
            photonView.RPC("AddTarget", RpcTarget.All, obj.GetComponent<PhotonView>().ViewID);

            yield return new WaitUntil(() => targets.Count != _targetsAllowed);
        }
    }





    [PunRPC]
    private void AddTarget(int viewID)
    {
        GameObject target = PhotonView.Find(viewID).gameObject;
        targets.Add(target);
    }

    public List<GameObject> GetTargets()
    {
        return targets;
    }



    public void RemoveTarget(GameObject target)
    {

        targets.Remove(target);
        _targetsAllowed++;
    }

}
