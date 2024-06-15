using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProductDetail;


public class ProductDetail {
    public DetailProductData data;
    public int status;
    public bool success;

}
public class DetailProductData {
    public DetailProduct detailProduct;
}
public class DetailProduct {
    public string category;
    public string discount;
    public string image_logo;
    public string image_product;
    public string link_product;
    public string link_sale;
    public string name;
    public string price;
    public string price_original;
    public string rate;
    public string description;
}

