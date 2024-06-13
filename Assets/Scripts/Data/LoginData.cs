using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginData {

    public bool success;
    public int status;
    public string message;
    public LoginTolken data;

}

public class LoginTolken {
    public string accessToken;
}