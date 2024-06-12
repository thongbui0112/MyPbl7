using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChangePassword : MonoBehaviour
{
    UIDocument uidoc;
    VisualElement changePasswordPage, mainView;
    Button acpBtn,backBtn;
    TextField currentPassTf, newPassTf, repeatPassTf;

    private void Awake() {
        this.uidoc = GetComponent<UIDocument>();
        this.changePasswordPage = this.uidoc.rootVisualElement.Q<VisualElement>("ChangePasswordPage");
        this.mainView = this.uidoc.rootVisualElement.Q<VisualElement>("MainView");

        this.acpBtn = this.changePasswordPage.Q<Button>("AcceptBtn");
        
        this.acpBtn.clicked += AcceptBtn;


        this.currentPassTf = this.changePasswordPage.Q<TextField>("CurrentPasswordTf");
        this.newPassTf = this.changePasswordPage.Q<TextField>("NewPasswordTf");
        this.repeatPassTf = this.changePasswordPage.Q<TextField>("RepeatPasswordTf");

        this.backBtn = this.changePasswordPage.Q<Button>("BackBtn");
        this.backBtn.clicked += BackBtn;
    }

    public void BackBtn(){
        this.mainView.style.display = DisplayStyle.Flex;
        this.changePasswordPage.style.display = DisplayStyle.None;
    }
    public void AcceptBtn(){
        Debug.Log("hihi");
    }
}
