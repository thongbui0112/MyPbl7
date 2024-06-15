using UnityEngine;
using UnityEngine.UIElements;


public class ModifyUIDocumentFonts : MonoBehaviour {
    public UIDocument uiDocument;
    public VisualElement rootVisualElement;
    public Font myFont;
  
    void Start() {
    uiDocument = GetComponent<UIDocument>();
        rootVisualElement = uiDocument.rootVisualElement;

        // Lấy tất cả các Label từ root visual element
        var labels = rootVisualElement.Query<Label>().ToList();

        // Duyệt qua từng Label để thiết lập font và font asset
        foreach (var label in labels) {
            // Thiết lập Font Asset cho Label (nếu có)
            //  label.style.unityFontAsset = yourFontAsset; // yourFontAsset là Font Asset của bạn

            // Thiết lập Font cho Label (nếu không sử dụng Font Asset)
          //  label.style.fontass = myFont; // Chỉ định tên của Font bạn muốn sử dụng

        }
    }
}

