using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

public class Search : MonoBehaviour {
    UIDocument uiDoc;
    VisualElement searchPage, mainView,productPage;
    Button backBtn, searchBtn;
    string searchText;
    string apiUrl;
    TextField searchField;
    ScrollView searchView;
    [SerializeField] VisualTreeAsset productHolder;
    private void Awake() {
        this.apiUrl = FindObjectOfType<UIController>().apiUrl;
        uiDoc = GetComponent<UIDocument>();
        this.searchPage = this.uiDoc.rootVisualElement.Q<VisualElement>("Search");
        this.backBtn = this.searchPage.Q<Button>("BackBtn");
        this.searchBtn = this.searchPage.Q<Button>("SearchBtn");
        this.searchField = this.searchPage.Q<TextField>("SearchField");
        this.searchView = this.searchPage.Q<ScrollView>("SearchView");
        this.mainView = this.uiDoc.rootVisualElement.Q<VisualElement>("MainView");
        this.productPage = this.uiDoc.rootVisualElement.Q<VisualElement>("ProductPage");
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
            RootSearch search = JsonConvert.DeserializeObject<RootSearch>(respone);
            Debug.Log(search.data.results.Count);
            CreateProducts(search, this.searchView);
        }
    }
    public void CreateProducts(RootSearch data, VisualElement scrollView) {
        int count = 10;
        //if (data.data.results.Count <= count) {
        //    count = data.data.results.Count;
        //}
        for (int i = 0; i < 10; i = i + 2) {

            VisualElement productHolder = this.productHolder.CloneTree();
            Button btn1 = productHolder.Q<Button>("ProductBtn1");
            Button btn2 = productHolder.Q<Button>("ProductBtn2");
            scrollView.Add(productHolder);

            CreateProduct(data.data.results[i], btn1);
            CreateProduct(data.data.results[i+1], btn2);

        }
    }
    public void CreateProduct( DetailProduct productDetail, Button btn) {
        string name = productDetail.name;
        string price = productDetail.price;
        string rate = productDetail.rate;
        string originalPrice = productDetail.price_original;
        string linkSale = productDetail.link_sale;
        string image_product = productDetail.image_product;

        VisualElement img = btn.Q<VisualElement>("Image");
        Label lb1 = btn.Q<Label>("StoreTxt");
        Label lb2 = btn.Q<Label>("NameTxt");
        Label lb3 = btn.Q<Label>("PriceTxt");
        Label idProduct = btn.Q<Label>("IdProduct");

        lb1.text = linkSale;
        lb2.text = name;
        lb3.text = price;
        StartCoroutine(LoadImageFromUrl(image_product, img));
        btn.clicked += () => ProductBtnOnclick(btn);
    }
    IEnumerator LoadImageFromUrl(string imageUrl, VisualElement img) {
        string newUrl = imageUrl;
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(newUrl);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError) {
            Debug.Log("Error loading image: " + www.error);
        }
        else {
            Texture2D texture = DownloadHandlerTexture.GetContent(www);
            img.style.backgroundImage = texture;
        }
    }
    public void ProductBtnOnclick(Button btn) {
        //string idProduct = btn.Q<Label>("IdProduct").text;
        this.mainView.style.display = DisplayStyle.None;
        this.productPage.style.display = DisplayStyle.Flex;

      //  FindObjectOfType<ProductPage>().CreatePage(idProduct);
    }
}
