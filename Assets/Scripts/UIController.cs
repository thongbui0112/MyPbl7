using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour {
    public string apiUrl = "http://127.0.0.1:5000";
    [SerializeField] UIDocument uiDocument;
    [SerializeField] Button homeBtn, categoryBtn, historyBtn, cartBtn, userBtn;
    [SerializeField] VisualElement homeIcon, categoryIcon, cartIcon, historyIcon, userIcon;
    [SerializeField] VisualElement homePage, categoryPage, cartPage, historyPage, userPage;
    private void Awake() {
        uiDocument = GetComponent<UIDocument>();
        this.homeBtn = uiDocument.rootVisualElement.Q<Button>("HomeBtn");
        this.categoryBtn = uiDocument.rootVisualElement.Q<Button>("CategoryBtn");
        this.historyBtn = uiDocument.rootVisualElement.Q<Button>("HistoryBtn");
        this.cartBtn = uiDocument.rootVisualElement.Q<Button>("CartBtn");
        this.userBtn = uiDocument.rootVisualElement.Q<Button>("UserBtn");

        this.homeIcon = this.homeBtn.Q<VisualElement>("HomeIcon");
        this.categoryIcon = this.categoryBtn.Q<VisualElement>("CategoryIcon");
        this.cartIcon = this.cartBtn.Q<VisualElement>("CartIcon");
        this.historyIcon = this.historyBtn.Q<VisualElement>("HistoryIcon");
        this.userIcon = this.userBtn.Q<VisualElement>("UserIcon");

        this.homePage = uiDocument.rootVisualElement.Q<VisualElement>("HomePage");
        this.categoryPage = uiDocument.rootVisualElement.Q<VisualElement>("CategoryPage");
        this.cartPage = uiDocument.rootVisualElement.Q<VisualElement>("CartPage");
        this.historyPage = uiDocument.rootVisualElement.Q<VisualElement>("HistoryPage");
        this.userPage = uiDocument.rootVisualElement.Q<VisualElement>("UserPage");

        this.homeBtn.clicked += HomeBtnOnclick;
        this.categoryBtn.clicked += CategoryBtnOnclick;
        this.cartBtn.clicked += CartBtnOnclick;
        this.historyBtn.clicked += HistoryBtnOnclick;
        this.userBtn.clicked += UserBtnOnclick;

    }
    [SerializeField] Texture2D homeNormal, homeSelected;
    [SerializeField] Texture2D categoryNormal, categorySelected;
    [SerializeField] Texture2D cartNormal, cartSelected;
    [SerializeField] Texture2D historyNormal, historySelected;
    [SerializeField] Texture2D userNormal, userSelected;



    private void Start() {
        HomeBtnOnclick();
    }
    public void HomeBtnOnclick() {
        SetIconPage("HomePage");
        DisplayPageSelected("HomePage");
    }
    public void CategoryBtnOnclick() {
        SetIconPage("CategoryPage");
        DisplayPageSelected("CategoryPage");
    }
    public void CartBtnOnclick() {
        SetIconPage("CartPage");
        DisplayPageSelected("CartPage");
    } 
    public void HistoryBtnOnclick() {
        SetIconPage("HistoryPage");
        DisplayPageSelected("HistoryPage");
    }
    public void UserBtnOnclick() {
        SetIconPage("UserPage");
        DisplayPageSelected("UserPage");
    }
    public void DisplayPageSelected(string namePage) {
        HideAllPage();
        if (namePage == "HomePage") {
            this.homePage.style.display = DisplayStyle.Flex;
        }
        if (namePage == "CategoryPage") {
            this.categoryPage.style.display = DisplayStyle.Flex;
        }
        if (namePage == "CartPage") {
            this.cartPage.style.display = DisplayStyle.Flex;
        }
        if (namePage == "HistoryPage") {
            this.historyPage.style.display = DisplayStyle.Flex;
        }
        if (namePage == "UserPage") {
            this.userPage.style.display = DisplayStyle.Flex;
        }
    }
    public void HideAllPage() {
        this.homePage.style.display = DisplayStyle.None;
        this.categoryPage.style.display = DisplayStyle.None;
        this.cartPage.style.display = DisplayStyle.None;
        this.historyPage.style.display = DisplayStyle.None;
        this.userPage.style.display = DisplayStyle.None;
    }
    public void SetIconPage(string namePage){
        this.homeIcon.style.backgroundImage = this.homeNormal;
        this.categoryIcon.style.backgroundImage = this.categoryNormal;
        this.cartIcon.style.backgroundImage = this.cartNormal;
        this.historyIcon.style.backgroundImage = this.historyNormal;
        this.userIcon.style.backgroundImage = this.userNormal;

        if (namePage == "HomePage") {
            this.homeIcon.style.backgroundImage = this.homeSelected;
        }
        if (namePage == "CategoryPage") {
            this.categoryIcon.style.backgroundImage = this.categorySelected;
        }
        if (namePage == "CartPage") {
            this.cartIcon.style.backgroundImage = this.cartSelected;
        }
        if (namePage == "HistoryPage") {
            this.historyIcon.style.backgroundImage = this.historySelected;
        }
        if (namePage == "UserPage") {
            this.userIcon.style.backgroundImage = this.userSelected;
        }
    }

}
