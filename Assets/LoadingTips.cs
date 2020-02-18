using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingTips : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tipsText;
    [SerializeField] float refreshRate = 10f;
    List<string> tips;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadTipsFile());
        InvokeRepeating("ShowTip", 0.2f, refreshRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadTipsFile()
    {
        var request = UnityEngine.Networking.UnityWebRequest.Get(Application.streamingAssetsPath + "/JSON/Tips.json");
        yield return request.SendWebRequest();
        string json = request.downloadHandler.text;

        tips = JsonConvert.DeserializeObject<List<string>>(json);
    }

    void ShowTip()
    {
        if (tips?.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, tips.Count);

            tipsText.text = tips[randomIndex];
        }
    }
}
