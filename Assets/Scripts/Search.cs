using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

public class Search : MonoBehaviour {
    UIDocument uiDoc;
    VisualElement searchPage, mainView;
    Button backBtn, searchBtn;
    string searchText;
    string apiUrl;
    TextField searchField;
    private void Awake() {
        this.apiUrl = FindObjectOfType<UIController>().apiUrl;
        uiDoc = GetComponent<UIDocument>();
        this.searchPage = this.uiDoc.rootVisualElement.Q<VisualElement>("Search");
        this.backBtn = this.searchPage.Q<Button>("BackBtn");
        this.searchBtn = this.searchPage.Q<Button>("SearchBtn");
        this.searchField = this.searchPage.Q<TextField>("SearchField");

        this.mainView = this.uiDoc.rootVisualElement.Q<VisualElement>("MainView");

        this.backBtn.clicked += BackBtn;
        this.searchBtn.clicked += SearchBtn;
        this.searchField.RegisterValueChangedCallback(this.OnSearchFieldValueChanged);
    }
    public void BackBtn() {
        this.mainView.style.display = DisplayStyle.Flex;
        this.searchPage.style.display = DisplayStyle.None;
    }

    void OnSearchFieldValueChanged(ChangeEvent<string> evt) {

        this.searchText = evt.newValue;
        Debug.Log(searchText);
    }

    public void SearchBtn() {
        if (!string.IsNullOrEmpty(searchText))
            StartCoroutine(SearchProducts());
    }

    public IEnumerator SearchProducts() {
        string newUrl = this.apiUrl + "/api/v1/search_product_by_name/?find=" + this.searchText;

        Debug.Log(newUrl);

        UnityWebRequest request = UnityWebRequest.Get(newUrl);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success) {
            Debug.Log("Nothing");
        }
        else {
            string respone = request.downloadHandler.text;
            Debug.Log(respone);
        }
    }

}
