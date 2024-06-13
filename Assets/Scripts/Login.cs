using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

public class Login : MonoBehaviour
{
    string apiUrl;
    UIDocument uiDoc;
    VisualElement login;
    Button backButton;
    private void Awake() {
        this.uiDoc = GetComponent<UIDocument>();
        this.login = this.uiDoc.rootVisualElement.Q<VisualElement>("Login");
        this.apiUrl = FindObjectOfType<UIController>().apiUrl;

        this.backButton = this.login.Q<Button>("BackBtn");
        Debug.Log("Logining");
       // StartCoroutine(LoginAPI("tranvanluyt12b4@gmail.com", "22032002"));
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
            LoginData loginData = JsonConvert.DeserializeObject<LoginData>(jsonResponse);

            string accessTolken = loginData.data.accessToken;

            PlayerPrefs.SetString("Tolken",accessTolken);
            Debug.Log(PlayerPrefs.GetString("Tolken"));
        }
        else {
            Debug.Log("failed");
        }
    }
}
