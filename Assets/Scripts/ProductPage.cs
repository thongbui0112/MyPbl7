using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

public class ProductPage : MonoBehaviour {
    string apiUrl;
    string token;
    UIDocument uiDoc;
    VisualElement productPage, mainView;
    Button backBtn, addToCartBtn, goToStoreBtn;
    Label name, store, rate, price, originPrice, discount, description;
    VisualElement img;
    string id, url;
    [SerializeField] Texture2D defaultImg;
    private void Awake() {
        this.apiUrl = FindObjectOfType<UIController>().apiUrl;


        this.uiDoc = GetComponent<UIDocument>();
        this.productPage = this.uiDoc.rootVisualElement.Q<VisualElement>("ProductPage");
        this.mainView = this.uiDoc.rootVisualElement.Q<VisualElement>("MainView");

        this.backBtn = this.productPage.Q<Button>("BackBtn");
        this.addToCartBtn = this.productPage.Q<Button>("AddToCartBtn");
        this.goToStoreBtn = this.productPage.Q<Button>("GoToStoreBtn");
        this.name = this.productPage.Q<Label>("Name");
        this.store = this.productPage.Q<Label>("Store");
        this.rate = this.productPage.Q<Label>("Rate");
        this.price = this.productPage.Q<Label>("Price");
        this.discount = this.productPage.Q<Label>("Discount");
        this.originPrice = this.productPage.Q<Label>("OriginPrice");
        this.description = this.productPage.Q<Label>("DescriptionTxt");
        this.img = this.productPage.Q<VisualElement>("ImageProduct");

        this.goToStoreBtn.clicked += GoToStoreBtn;
        this.backBtn.clicked += BackBtn;

        this.addToCartBtn.clicked += AddToCardBtn;
    }
    private void Start() {
        this.token = PlayerPrefs.GetString("Token");
    }


    public void CreateProductPage(DetailProduct productInformation) {
        this.name.text = productInformation.name;
        this.store.text = productInformation.link_sale;
        this.rate.text = productInformation.rate;

        this.price.text = productInformation.price;
        if (productInformation.discount != "0%")
            this.discount.text = "(-" + productInformation.discount + ")";
        if (productInformation.price_original != "0")
            this.originPrice.text = "<s>" + productInformation.price_original + "</s>";
      //  if (!string.IsNullOrEmpty(productInformation.description))
            this.description.text = productInformation.description;

        this.url = productInformation.link_product;

        StartCoroutine(SetImage(productInformation.image_product));
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
            this.img.style.backgroundImage = texture;
        }
    }
    public void GoToStoreBtn() {
        // string url = productInformation.linkSale;
        //string url = "https://shopee.vn/product/61333030/15988701479";
        Application.OpenURL(url);
    }
    public void BackBtn() {
        this.mainView.style.display = DisplayStyle.Flex;
        this.productPage.style.display = DisplayStyle.None;
        ResetPage();
    }

    public void CreatePage(string idProduct) {
        StartCoroutine(GetProductById(idProduct));
    }

    public IEnumerator GetProductById(string idProduct) {
        string newUrl = this.apiUrl + "/api/v1/get_detail_product_by_id/" + "?productId=" + idProduct;
        UnityWebRequest request = UnityWebRequest.Get(newUrl);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success) {
            Debug.LogError("Error: " + request.error);
        }
        else {
            string response = request.downloadHandler.text;

            ProductDetail productDetail = JsonConvert.DeserializeObject<ProductDetail>(response);

            DetailProduct infor = productDetail.data.detailProduct;
            this.id = idProduct;
            CreateProductPage(infor);

        }
    }

    public void AddToCardBtn() {
        StartCoroutine(AddToCart(id));
    }


    public IEnumerator AddToCart(string idProduct) {

        string newUrl = this.apiUrl + "/api/v1/add_to_cart" + "?idProduct=" + idProduct;
        Debug.Log(newUrl);
        string author = "Bearer" + token;
        Debug.Log(author);
        // Tạo UnityWebRequest cho yêu cầu POST
        UnityWebRequest www = new UnityWebRequest(newUrl, "POST");

        // Set header Authorization
        www.SetRequestHeader("Authorization", author);
        www.SetRequestHeader("Content-Type", "application/json");

        // Gửi yêu cầu và chờ phản hồi
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log("Error: " + www.error);
        }
        else {
            Debug.Log("Da them vao cart");
        }
    }
    public void ResetPage(){
        this.name.text = string.Empty;
        this.store.text = string.Empty;
        this.rate.text = string.Empty;

        this.price.text = string.Empty ;

        this.discount.text = string.Empty;

        this.originPrice.text = string.Empty;
        //  if (!string.IsNullOrEmpty(productInformation.description))
        this.description.text = string.Empty;

        this.url = string.Empty;
        this.img.style.backgroundImage = this.defaultImg;

    }
}

