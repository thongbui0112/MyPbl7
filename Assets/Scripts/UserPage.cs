using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UserPage : MonoBehaviour
{
    string apiUrl;
    UIDocument uiDoc;
    VisualElement userPage,mainView;
    [SerializeField] VisualElement changePasswordPage,loginPage;
    [SerializeField] Button changePassBtn,logoutBtn;

    [SerializeField] VisualElement userAccountPage, loginRequirementPage;

    [SerializeField] Label email, fullname, phone, birth;
    [SerializeField] VisualElement avatar;
    private void Awake() {
        this.uiDoc = GetComponent<UIDocument>();
        this.userPage = this.uiDoc.rootVisualElement.Q<VisualElement>("UserPage");
        this.mainView = this.uiDoc.rootVisualElement.Q<VisualElement>("MainView");
        this.changePasswordPage = this.uiDoc.rootVisualElement.Q<VisualElement>("ChangePasswordPage");
        this.loginPage = this.uiDoc.rootVisualElement.Q<VisualElement>("Login");
        this.userAccountPage = this.userPage.Q<VisualElement>("UserAccount");
        this.changePassBtn = this.userPage.Q<Button>("Password");
        this.logoutBtn = this.userPage.Q<Button>("Logout");
        this.changePassBtn.clicked += ChangePassBtn;
        this.logoutBtn.clicked += LogOutBtn;

        this.email = this.userPage.Q<Label>("EmailTxt");
        this.fullname = this.userPage.Q<Label>("FullNameTxt");
        this.phone = this.userPage.Q<Label>("PhoneTxt");
        this.birth = this.userPage.Q<Label>("BirthTxt");
        this.avatar = this.userPage.Q<VisualElement>("Image");
    }
    private void Start() {
        this.apiUrl = FindObjectOfType<UIController>().apiUrl;
        
    }
    public void LogOutBtn(){
        StartCoroutine(LogOut());
    }
    public IEnumerator LogOut (){
        // Tạo một UnityWebRequest với phương thức POST
        string newApi = apiUrl + "/api/v1/logout";
        UnityWebRequest request = new  UnityWebRequest(newApi, "POST");
        string token = PlayerPrefs.GetString("Token");

        // Thêm header Authorization
        request.SetRequestHeader("Authorization", "Bearer" + token);

        // Gửi yêu cầu và chờ phản hồi
        yield return request.SendWebRequest();

        // Kiểm tra trạng thái phản hồi
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError) {
            Debug.Log("Error: " + request.error);
        }
        else {
            // Phản hồi thành công
            //Debug.Log("Response: " + request.downloadHandler.text);

            // Xử lý dữ liệu JSON nhận được từ API
            // Bạn có thể sử dụng JsonUtility hoặc một thư viện JSON khác để phân tích cú pháp JSON
            // Ví dụ:
            // LogoutResponse response = JsonUtility.FromJson<LogoutResponse>(request.downloadHandler.text);\
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene(0);
        }
    }

    public IEnumerator GetUserProfile(){
        string newUrl = this.apiUrl + "/api/v1/get_profile";
        string token = PlayerPrefs.GetString("Token");
        UnityWebRequest request = UnityWebRequest.Get(newUrl);

        // Thêm header Authorization
        request.SetRequestHeader("Authorization","Bearer" + token);

        // Gửi yêu cầu và chờ phản hồi
        yield return request.SendWebRequest();

        // Kiểm tra trạng thái phản hồi
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError) {
            Debug.Log("Error: " + request.error);
        }
        else {
            // Phản hồi thành công
            Debug.Log("Response: " + request.downloadHandler.text);
            string respone = request.downloadHandler.text;
            ProfileData profileData = JsonConvert.DeserializeObject<ProfileData>(respone);
            Debug.Log(profileData.data.profile.fullname);
            UpdateProfie(profileData);
        }
    }
    public void UpdateProfie(ProfileData data) {
        this.email.text = data.data.profile.email;
        this.fullname.text = data.data.profile.fullname;
        this.phone.text = data.data.profile.phone_number;
        this.birth.text = data.data.profile.birth_day;
        string link = data.data.profile.avatar;
        StartCoroutine( SetImage(link));
    }
    public IEnumerator SetImage(string imageUrl) {
        string newUrl = imageUrl;
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(newUrl);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError) {
            Debug.Log("Error loading image: " + www.error);
        }
        else {
            Texture2D texture = DownloadHandlerTexture.GetContent(www);
            this.avatar.style.backgroundImage = texture;
        }
    }

    public void ChangePassBtn(){
        this.mainView.style.display = DisplayStyle.None;
        this.changePasswordPage.style.display = DisplayStyle.Flex;
    }


}
