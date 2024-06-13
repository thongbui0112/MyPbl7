using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Category {
    public bool success;
    public int status;
    public string message;
    public Data data;
}

[Serializable]
public class Data {
    public List<string> result;
}
