using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UserPage : MonoBehaviour
{
    UIDocument uiDoc;
    VisualElement userPage,mainView;
    [SerializeField] VisualElement changePasswordPage;
    [SerializeField] Button changePassBtn;
    private void Awake() {
        this.uiDoc = GetComponent<UIDocument>();
        this.userPage = this.uiDoc.rootVisualElement.Q<VisualElement>("UserPage");
        this.mainView = this.uiDoc.rootVisualElement.Q<VisualElement>("MainView");
        this.changePasswordPage = this.uiDoc.rootVisualElement.Q<VisualElement>("ChangePasswordPage");

        this.changePassBtn = this.userPage.Q<Button>("Password");
        this.changePassBtn.clicked += ChangePassBtn;
    }


    public void ChangePassBtn(){
        this.mainView.style.display = DisplayStyle.None;
        this.changePasswordPage.style.display = DisplayStyle.Flex;
    }
}
