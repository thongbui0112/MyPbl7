using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

public class ProductPage : MonoBehaviour
{
    string apiUrl;
    UIDocument uiDoc;
    VisualElement productPage,mainView;
    Button backBtn, addToCartBtn, goToStoreBtn;
    Label name, store, rate, price,originPrice,discount;
    ProductInformation productInformation;

    private void Awake() {
        this.apiUrl = FindObjectOfType<UIController>().apiUrl;

        this.uiDoc = GetComponent<UIDocument>();
        this.productPage = this.uiDoc.rootVisualElement.Q<VisualElement>("ProductPage");

        this.backBtn = this.productPage.Q<Button>("BackBtn");
        this.addToCartBtn = this.productPage.Q<Button>("AddToCartBtn");
        this.goToStoreBtn = this.productPage.Q<Button>("GoToStoreBtn");
        this.name = this.productPage.Q<Label>("Name");
        this.store = this.productPage.Q<Label>("Store");
        this.rate = this.productPage.Q<Label>("Rate");
        this.price = this.productPage.Q<Label>("Price");
        this.discount = this.productPage.Q<Label>("Discount");
        this.originPrice = this.productPage.Q<Label>("OriginPrice");

        this.goToStoreBtn.clicked += GoToStoreBtn;
        this.backBtn.clicked += BackBtn;
    }


    public void CreateProductPage(DetailProduct productInformation){
        this.name.text = productInformation.name;
        this.store.text = productInformation.link_sale;
        this.rate.text = productInformation.rate;
        
        this.price.text = productInformation.price;
        this.discount.text = productInformation.discount;
        this.originPrice.text = "<s>" + productInformation.price_original + "</s>";

        }
    public void GoToStoreBtn(){
       // string url = productInformation.linkSale;
        string url = "https://shopee.vn/product/61333030/15988701479";
        Application.OpenURL(url);
    }
    public void BackBtn(){
        this.mainView.style.display = DisplayStyle.Flex;
        this.productPage.style.display = DisplayStyle.None;
    }

    public void CreatePage(string idProduct){
        StartCoroutine(GetProductById(idProduct));
    }

    public IEnumerator GetProductById(string idProduct){
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

            CreateProductPage(infor);

        }
    }
}
