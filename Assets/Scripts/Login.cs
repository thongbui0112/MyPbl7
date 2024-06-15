using Newtonsoft.Json;
using System.Collections;

using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Login : MonoBehaviour {
    string apiUrl;
    UIDocument uiDoc;
    VisualElement login,mainView;
    Button backButton,loginBtn;
    string email, password;
    TextField emailTf, passwordTf;

    private void Awake() {
        this.uiDoc = GetComponent<UIDocument>();
        this.login = this.uiDoc.rootVisualElement.Q<VisualElement>("Login");
        this.apiUrl = FindObjectOfType<UIController>().apiUrl;
        this.emailTf = this.login.Q<TextField>("Email");
        this.passwordTf = this.login.Q<TextField>("Password");
        this.backButton = this.login.Q<Button>("BackBtn");
        this.mainView = this.uiDoc.rootVisualElement.Q<VisualElement>("MainView");
        this.loginBtn = this.login.Q<Button>("LoginBtn");
        this.backButton.clicked += BackButton;
        this.loginBtn.clicked += LoginBtn;
        AutoLogin(); 
    }
    public void AutoLogin() {
        if (PlayerPrefs.HasKey("Email") && PlayerPrefs.HasKey("Password")) {
            string email = PlayerPrefs.GetString("Email");
            string password = PlayerPrefs.GetString("Password");
            StartCoroutine(LoginAPI(email,password));
        }
    }
    public void LoginBtn(){
        this.email = this.emailTf.value;
        this.password = this.passwordTf.value;

        Debug.Log(email);
        Debug.Log(password);
        StartCoroutine(LoginAPIInput(email,password));
    }
    public IEnumerator LoginAPI(string email, string password) {
        string jsonRequestBody = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\"}";
        string newUrl = this.apiUrl + "/api/v1/login";

        UnityWebRequest request = new UnityWebRequest(newUrl, "POST");
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

            PlayerPrefs.SetString("Token", accessTolken);
            Debug.Log(PlayerPrefs.GetString("Token"));
           // SceneManager.LoadScene(0);

            FindObjectOfType<HomePage>().GetRecommendProducts();
        }
        else {
            Debug.Log("failed");
        }
    }
    public IEnumerator LoginAPIInput(string email, string password) {
        string jsonRequestBody = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\"}";
        string newUrl = this.apiUrl + "/api/v1/login";

        UnityWebRequest request = new UnityWebRequest(newUrl, "POST");
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

            PlayerPrefs.SetString("Token", accessTolken);
            Debug.Log(PlayerPrefs.GetString("Token"));
              SceneManager.LoadScene(0);

            FindObjectOfType<HomePage>().GetRecommendProducts();
        }
        else {
            Debug.Log("failed");
        }
    }
    public void BackButton(){
        this.mainView.style.display = DisplayStyle.Flex;
        this.login.style.display = DisplayStyle.None;
    }
}
