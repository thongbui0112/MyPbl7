using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductSearch {
    public string category;
    public string description;
    public string discount;
    public string image_logo;
    public string image_product;
    public string link_product;
    public string link_sale;
    public string name;
    public string price;
    public string price_original;
    public string rate;
}

public class DataSearch {
    public List<DetailProduct> results;
}



public class RootSearch {
    public DataSearch data;

}
