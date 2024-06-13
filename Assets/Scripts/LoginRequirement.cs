using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LoginRequirement : MonoBehaviour {

    UIDocument uiDoc;
    VisualElement loginPage, mainView, loginRequirementPage;
    Button loginBtn;

    private void Awake() {
        this.uiDoc = GetComponent<UIDocument>();
        this.loginPage = this.uiDoc.rootVisualElement.Q<VisualElement>("Login");
        this.mainView = this.uiDoc.rootVisualElement.Q<VisualElement>("MainView");
        this.loginRequirementPage = this.uiDoc.rootVisualElement.Q<VisualElement>();

        this.loginBtn = this.loginRequirementPage.Q<Button>("LoginBtn");

        this.loginBtn.clicked += LoginBtn;
        }

    public void LoginBtn() {
        this.mainView.style.display = DisplayStyle.None;
        this.loginPage.style.display = DisplayStyle.Flex;

    }
}
