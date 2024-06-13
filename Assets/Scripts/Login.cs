using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;

public class Login : MonoBehaviour
{
    string apiUrl;

    private void Awake() {
        this.apiUrl = FindObjectOfType<UIController>().apiUrl;
        Debug.Log("Logining");
        StartCoroutine(LoginAPI("tranvanluyt12b4@gmail.com", "22032002"));
    }
    public void AutoLogin(){
        if(PlayerPrefs.HasKey("Email") && PlayerPrefs.HasKey("Password")){
            string email = PlayerPrefs.GetString("Email");
            string password = PlayerPrefs.GetString("Password");

            
        }
    }
    public IEnumerator LoginAPI(string email, string password){
        string jsonRequestBody = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\"}";
        string newUrl = this.apiUrl + "/api/v1/login";

        UnityWebRequest request = new UnityWebRequest(newUrl,"POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonRequestBody); 
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        // Handle response
        if (request.result == UnityWebRequest.Result.Success) {
            string jsonResponse = request.downloadHandler.text;
            Debug.Log(jsonResponse);

            PlayerPrefs.SetString("Email", email);
            PlayerPrefs.SetString("Password", password);

        }
        else {
            Debug.Log("failed");
        }
    }
}
