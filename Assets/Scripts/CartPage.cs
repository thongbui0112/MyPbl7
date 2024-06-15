using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

public class CartPage : MonoBehaviour {
    UIDocument uidoc;
    VisualElement cartPage,mainView,productPage;
    ScrollView cartView;
    [SerializeField] VisualTreeAsset cartTemp, productCartBtn;
    string apiUrl;
    private void Awake() {
        this.uidoc = GetComponent<UIDocument>();
        this.cartPage = this.uidoc.rootVisualElement.Q<VisualElement>("CartPage");
        this.cartView = this.cartPage.Q<ScrollView>("CartView");
        this.mainView = this.uidoc.rootVisualElement.Q<VisualElement>("MainView");
        this.productPage = this.uidoc.rootVisualElement.Q<VisualElement>("ProductPage");


    }
    private void Start() {
        this.apiUrl = FindObjectOfType<UIController>().apiUrl;
    }
    public void GetCard() {
        StartCoroutine(GetAllCart());
        this.cartView.Clear();
    }
    public IEnumerator GetAllCart() {
        string token = PlayerPrefs.GetString("Token");
        string newUrl = apiUrl + "/api/v1/get_cart";
        UnityWebRequest request = UnityWebRequest.Get(newUrl);
        request.SetRequestHeader("Authorization", "Bearer" + token);
        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success) {

        }
        else {

            string response = request.downloadHandler.text;
            Debug.Log(response);
            ProductCart cart = JsonConvert.DeserializeObject<ProductCart>(response);
            Debug.Log(cart.data.result.listIdProduct.Count);
            foreach (string i in cart.data.result.listIdProduct) {
                StartCoroutine(GetProductById(i));
                Debug.Log("hi");
            }
        }

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
            CreateCart(infor,idProduct);
            Debug.Log("hiii");
        }
    }
    public void CreateCart(DetailProduct infor, string id){
        VisualElement cartHolder = this.productCartBtn.CloneTree();
        cartHolder.Q<Label>("NameTxt").text = infor.name;
        cartHolder.Q<Label>("StoreTxt").text = infor.link_sale;
        cartHolder.Q<Label>("RateTxt").text = infor.rate;
        cartHolder.Q<Label>("PriceTxt").text = infor.price;
        cartHolder.Q<Label>("Id").text = id;
      //  cartHolder.Q<Label>("Url").text = infor.link_product;
        VisualElement img = cartHolder.Q<VisualElement>("Image");
        StartCoroutine(SetImage(infor.image_product,img));
        Button detailBtn = cartHolder.Q<Button>("ProductCartBtn");
        detailBtn.clicked += () => ProductBtnOnclick(detailBtn);

        Button linkBtn = cartHolder.Q<Button>("LinkBtn");
        linkBtn.clicked += () => LinkBtn(infor.link_product);
        this.cartView.Add(cartHolder);
    }

    public void LinkBtn(string url){
        Application.OpenURL(url);
    }
    public void ProductBtnOnclick(Button btn) {
        string idProduct = btn.Q<Label>("Id").text;
        this.mainView.style.display = DisplayStyle.None;
        this.productPage.style.display = DisplayStyle.Flex;

        FindObjectOfType<ProductPage>().CreatePage(idProduct);
    }
    public IEnumerator SetImage(string imageUrl,VisualElement img) {
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
}
