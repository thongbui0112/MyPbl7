using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CartPage : MonoBehaviour {
    UIDocument uidoc;
    VisualElement cartPage;
    ScrollView cartView;
    [SerializeField] VisualTreeAsset cartTemp, productCartBtn;


    private void Awake() {
        this.uidoc = GetComponent<UIDocument>();
        this.cartPage = this.uidoc.rootVisualElement.Q<VisualElement>("CartPage");
        this.cartView = this.cartPage.Q<ScrollView>("CartView");


        for (int i = 0; i < 5; i++) {
            VisualElement cartHolder = this.cartTemp.CloneTree();
            cartHolder.Q<Label>("TimeTxt").text = "You shallnot pass";
            this.cartView.Add(cartHolder);

            for (int j = 0; j < 3; j++) {

                VisualElement productCart = this.productCartBtn.CloneTree();
                this.cartView.Add(productCart);
            }
        }
    }
}
