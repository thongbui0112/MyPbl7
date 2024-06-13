using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UIElements;

public class UserPage : MonoBehaviour
{
    UIDocument uiDoc;
    VisualElement userPage,mainView;
    [SerializeField] VisualElement changePasswordPage,loginPage;
    [SerializeField] Button changePassBtn,logoutBtn;

    [SerializeField] VisualElement userAccountPage, loginRequirementPage;
    private void Awake() {
        this.uiDoc = GetComponent<UIDocument>();
        this.userPage = this.uiDoc.rootVisualElement.Q<VisualElement>("UserPage");
        this.mainView = this.uiDoc.rootVisualElement.Q<VisualElement>("MainView");
        this.changePasswordPage = this.uiDoc.rootVisualElement.Q<VisualElement>("ChangePasswordPage");
        this.loginPage = this.uiDoc.rootVisualElement.Q<VisualElement>("Login");
        this.userAccountPage = this.userPage.Q<VisualElement>("UserAccount");
        this.changePassBtn = this.userPage.Q<Button>("Password");
        this.changePassBtn.clicked += ChangePassBtn;
    }

    public void LogOutBtn(){
        
    }


    public void ChangePassBtn(){
        this.mainView.style.display = DisplayStyle.None;
        this.changePasswordPage.style.display = DisplayStyle.Flex;
    }


}
