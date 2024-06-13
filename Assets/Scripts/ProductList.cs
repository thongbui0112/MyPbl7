using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProductList {
    public ProductListData data;
    public string message;
    public int status;
    public bool success;
}

[System.Serializable]
public class ProductListData {
    public List<string> result;
}

