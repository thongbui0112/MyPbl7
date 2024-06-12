using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Search : MonoBehaviour
{
    UIDocument uiDoc;
    VisualElement searchPage, mainView;
    Button backBtn;


    private void Awake() {
        uiDoc = GetComponent<UIDocument>();
        this.searchPage = this.uiDoc.rootVisualElement.Q<VisualElement>("Search");
        this.backBtn = this.searchPage.Q<Button>("BackBtn");

        this.mainView = this.uiDoc.rootVisualElement.Q<VisualElement>("MainView");

        this.backBtn.clicked += BackBtn;
    }
    public void BackBtn(){
        this.mainView.style.display = DisplayStyle.Flex;
        this.searchPage.style.display = DisplayStyle.None;
    }
}
