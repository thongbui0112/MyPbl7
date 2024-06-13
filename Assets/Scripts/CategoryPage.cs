using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;
public class CategoryPage : MonoBehaviour {
    UIDocument uiDoc;
    [SerializeField] VisualElement categoryPage;
    [SerializeField] ScrollView categoryView, productView;
    [SerializeField] VisualTreeAsset categoryBtn, productHolder;
    Color defaulColor = new Color(190 / 255f, 190 / 255f, 190 / 255f);
    Color pickedColor = new Color(230 / 255f, 230 / 255f, 230 / 255f);
    string apiUrl;
    private void Awake() {
        this.apiUrl = FindObjectOfType<UIController>().apiUrl;
        this.uiDoc = GetComponent<UIDocument>();
        this.categoryPage = this.uiDoc.rootVisualElement.Q<VisualElement>("CategoryPage");
        this.categoryView = this.categoryPage.Q<ScrollView>("CategoryScroll");
        this.productView = this.categoryPage.Q<ScrollView>("ProductScroll");
        //for (int i = 0; i < 15; i++) {
        //    VisualElement category = this.categoryBtn.CloneTree();
        //    Button categoryBtn = category.Q<Button>();
        //    categoryBtn.text = "I am fire, I am death";
        //    categoryBtn.style.backgroundColor = defaulColor;
        //    categoryBtn.clicked += () => ButtonOnclick(categoryBtn);

        //    this.categoryView.Add(category);

        //    VisualElement productButton = this.productHolder.CloneTree();
        //    CreateProductHolder(productButton);
        //    this.productView.Add(productButton);

        //}
        StartCoroutine(CreateCategories());

    }


    public IEnumerator CreateCategories() {
        string newUrl = this.apiUrl + "/api/v1/get_categories";
        UnityWebRequest request = UnityWebRequest.Get(newUrl);

        yield return request.SendWebRequest();

        if(request.result != UnityWebRequest.Result.Success) {
            
        }
        else {
            string sponse = request.downloadHandler.text;
            Debug.Log(sponse);

            Category categories = JsonConvert.DeserializeObject<Category>(sponse);
            foreach(string  i in categories.data.result) {
                VisualElement category = this.categoryBtn.CloneTree();
                Button categoryBtn = category.Q<Button>();
                categoryBtn.text = i;
                categoryBtn.style.backgroundColor = defaulColor;
                categoryBtn.clicked += () => CategoryBtnOnclick(categoryBtn,i);

                this.categoryView.Add(category);
            }
        }
    }

    public void CategoryBtnOnclick(Button button,string nameCategory) {

        this.productView.Clear();
        List<Button> buttons = this.categoryView.Query<Button>().ToList();
        foreach (Button button1 in buttons) {
            button1.style.backgroundColor = this.defaulColor;
        }
        button.style.backgroundColor = this.pickedColor;
        Debug.Log(nameCategory);

        StartCoroutine(GetProducts(nameCategory));

        
    }

    public IEnumerator GetProducts(string nameCat){
        
        string newCate = UnityWebRequest.EscapeURL(nameCat);
        string newUrl = this.apiUrl + "/api/v1/get_list_id_products_from_category" + "?name=" + newCate;
        Debug.Log(newUrl);

        UnityWebRequest request = UnityWebRequest.Get(newUrl);

        yield return request.SendWebRequest(); 

        

        if(request.result !=UnityWebRequest.Result.Success) {
            
        }
        else{
            string sponse = request.downloadHandler.text;
            Debug.Log(sponse);

            ProductList productList = JsonConvert.DeserializeObject<ProductList>(sponse);
            FindObjectOfType<HomePage>().CreateProducts(productList.data.result, this.productView);
        }

    }

}
