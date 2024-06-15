using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductCart {

    public CartData data;
    public string message;
    public int status;
    public bool success;
}

public class CartData {
    public ResultCart result;
}

public class ResultCart {
    public List<string> listIdProduct;
}
