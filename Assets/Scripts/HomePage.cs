using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Newtonsoft.Json;
using UnityEngine.Networking;
using UnityEngine.Rendering;
using Unity.VisualScripting;

public class HomePage : MonoBehaviour {
    string accessTolken;
    string apiUrl;
    UIDocument uiDoc;
    [SerializeField] VisualElement homePage,productPage;
    [SerializeField] ScrollView productView;
    [SerializeField] VisualTreeAsset productHolder;
    [SerializeField] Button searchBtn;
    [SerializeField] VisualElement searchPage, mainView;
    private void Awake() {
        accessTolken = PlayerPrefs.GetString("Tolken");
        this.apiUrl = FindObjectOfType<UIController>().apiUrl;
        this.uiDoc = GetComponent<UIDocument>();
        this.homePage = uiDoc.rootVisualElement.Q<VisualElement>("HomePage");
        this.productView = this.homePage.Q<ScrollView>("ProductScroll");
        //for (int i = 0; i < 4; i++) {
        //    VisualElement productButton = this.productHolder.CloneTree();
        //    CreateProductHolder(productButton);
        //    this.productView.Add(productButton);
        //}

        this.searchBtn = this.homePage.Q<Button>("SearchBtn");
        this.searchPage = this.uiDoc.rootVisualElement.Q<VisualElement>("Search");
        this.mainView = this.uiDoc.rootVisualElement.Q<VisualElement>("MainView");
        this.productPage = this.uiDoc.rootVisualElement.Q<VisualElement>("ProductPage");

        this.searchBtn.clicked += SearchBtn;
    }
    private void Start() {
        StartCoroutine(GetRecommendedProducts());
    }


    public void SearchBtn() {
        this.mainView.style.display = DisplayStyle.None;
        this.searchPage.style.display = DisplayStyle.Flex;
    }

    public IEnumerator GetRecommendedProducts() {
        string authorizationHeader = "Bearer" + this.accessTolken;
        string newUrl = this.apiUrl + "/api/v1/product_recommender";
        Debug.Log(authorizationHeader);
        Debug.Log(newUrl);
        UnityWebRequest request = UnityWebRequest.Get(newUrl);
        request.SetRequestHeader("Authorization", authorizationHeader);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success) {
            Debug.LogError("Error : " + request.error);
        }
        else {
            string response = request.downloadHandler.text;
            Debug.Log("Response: " + response);

            ProductRecomender productRecomender = new ProductRecomender();
            productRecomender = JsonConvert.DeserializeObject<ProductRecomender>(response);
            Debug.Log(productRecomender.result.Count);

            CreateProducts(productRecomender.result,this.productView);

        }
    }
    public void CreateProducts(List<string> products,ScrollView scrollView){
        int count = 20;
        if(products.Count <= count){
            count = products.Count;
        }
        for (int i = 0; i < count; i = i + 2) {

            VisualElement productHolder = this.productHolder.CloneTree();
            Button btn1 = productHolder.Q<Button>("ProductBtn1");
            Button btn2 = productHolder.Q<Button>("ProductBtn2");
            scrollView.Add(productHolder);

            string productId1 = products[i];
            StartCoroutine(GetProductById(productId1, btn1));
            if (i + 1 % 2 != 0 && i + 1 == count) {
                btn2.style.visibility = Visibility.Hidden;
            }
            else {
                string productId2 =products[i + 1];
                StartCoroutine(GetProductById(productId2, btn2));
            }
        }
    }

    public IEnumerator GetProductById(string productId, Button btn) {
        string newUrl = this.apiUrl + "/api/v1/get_detail_product_by_id/" + "?productId=" + productId;
        UnityWebRequest request = UnityWebRequest.Get(newUrl);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success) {
            Debug.LogError("Error: " + request.error);
        }
        else {
            string response = request.downloadHandler.text;

            ProductDetail productDetail = JsonConvert.DeserializeObject<ProductDetail>(response);
            CreateProduct(productId,productDetail, btn);
        }
    }

    public void CreateProduct(string productId,ProductDetail productDetail, Button btn) {
        string name = productDetail.data.detailProduct.name;
        string price = productDetail.data.detailProduct.price;
        string rate = productDetail.data.detailProduct.rate;
        string originalPrice = productDetail.data.detailProduct.price_original;
        string linkSale = productDetail.data.detailProduct.link_sale;
        string image_product = productDetail.data.detailProduct.image_product;
        
        VisualElement img = btn.Q<VisualElement>("Image");
        Label lb1 = btn.Q<Label>("StoreTxt");
        Label lb2 = btn.Q<Label>("NameTxt");
        Label lb3 = btn.Q<Label>("PriceTxt");
        Label idProduct = btn.Q<Label>("IdProduct");

        lb1.text = linkSale;
        lb2.text = name;
        lb3.text = price;
        idProduct.text = productId;

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
    public void ProductBtnOnclick(Button btn){
        string idProduct = btn.Q<Label>("IdProduct").text;
        this.mainView.style.display = DisplayStyle.None;
        this.productPage.style.display = DisplayStyle.Flex;

        FindObjectOfType<ProductPage>().CreatePage(idProduct);
    }



}
public class ProductInformation{
    public string name;
    public string price;
    public string rate;
    public string originalPrice;
    public string linkSale;
    public string imageProduct;
    public string discount;
    public string linkProduct;

    public void GetData(ProductDetail p){
        this.name = p.data.detailProduct.name;
        this.price = p.data.detailProduct.price;
        this.rate = p.data.detailProduct.rate;
        this.originalPrice = p.data.detailProduct.price_original;
        this.linkSale = p.data.detailProduct.link_sale;
        this.imageProduct = p.data.detailProduct.image_product;
        this.discount = p.data.detailProduct.discount;
        this.linkProduct = p.data.detailProduct.link_product;
    }

}
