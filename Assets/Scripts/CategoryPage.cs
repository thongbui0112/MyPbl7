using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
public class CategoryPage : MonoBehaviour
{
    UIDocument uiDoc;
    [SerializeField] VisualElement categoryPage;
    [SerializeField] ScrollView categoryView, productView;
    [SerializeField] VisualTreeAsset categoryBtn, productHolder;
    Color defaulColor = new Color(190/255f,190/255f,190 / 255f);
    Color pickedColor = new Color(230 / 255f, 230/255f, 230 / 255f);
    private void Awake() {
        this.uiDoc = GetComponent<UIDocument>();
        this.categoryPage = this.uiDoc.rootVisualElement.Q<VisualElement>("CategoryPage");
        this.categoryView = this.categoryPage.Q<ScrollView>("CategoryScroll");
        this.productView = this.categoryPage.Q<ScrollView>("ProductScroll");
        for(int i=0;i<15;i++){
            VisualElement category = this.categoryBtn.CloneTree();
            Button categoryBtn = category.Q<Button>();
            categoryBtn.text = "I am fire, I am death";
            categoryBtn.style.backgroundColor = defaulColor;
            categoryBtn.clicked += () => ButtonOnclick(categoryBtn);

            this.categoryView.Add(category);

            VisualElement productButton = this.productHolder.CloneTree();
            CreateProductHolder(productButton);
            this.productView.Add(productButton);

        }

    }
    public void ButtonOnclick(Button button){
        List<Button> buttons = this.categoryView.Query<Button>().ToList();
        foreach(Button button1 in buttons){
            button1.style.backgroundColor = this.defaulColor;
        }
        button.style.backgroundColor = this.pickedColor;
    }
    public void CreateProductHolder(VisualElement productButton) {
        Button btn1 = productButton.Q<Button>("ProductBtn1");
        Button btn2 = productButton.Q<Button>("ProductBtn2");
        CreateProductBtn(btn1);
        CreateProductBtn(btn2);
    }
    public void CreateProductBtn(Button btn) {
        if (btn.name == "ProductBtn2")
            btn.style.visibility = Visibility.Hidden;
        Image img = btn.Q<Image>("Image");
        Label lb1 = btn.Q<Label>("StoreTxt");
        Label lb2 = btn.Q<Label>("NameTxt");
        Label lb3 = btn.Q<Label>("PriceTxt");
        lb1.text = lb2.text = lb3.text = "";
    }
}
