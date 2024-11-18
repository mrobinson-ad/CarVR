using UnityEngine.Networking;
using UnityEngine;
using System.Collections;
using Newtonsoft.Json;

public class InternetCheck : MonoBehaviour
{
    public GameObject displayError;
    public GameObject LeftController;
    public GameObject RightController;

    private void Awake()
    {
        StartCoroutine(CheckInternet());
    }
    private IEnumerator CheckInternet()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get("https://virtualhome.hopto.org/car/checkinternet"))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                displayError.SetActive(true);
                yield return new WaitForSeconds(1);
                LeftController.SetActive(false);
                RightController.SetActive(false);
            }
            else
            {
                string jsonResponse = webRequest.downloadHandler.text;
               
               
                if (jsonResponse != "\"reached\"") 
                {
                    displayError.SetActive(true);
                    yield return new WaitForSeconds(1);
                    LeftController.SetActive(false);
                    RightController.SetActive(false);
                }
            }
        }
    }
}
