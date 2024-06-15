using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileData {

    public DataUser data;
    public string message;
    public int status;
    public bool success;
}




public class DataUser {
    public ProfileUser profile;
}

public class ProfileUser {
    public string avatar;
    public string birth_day;
    public string email;
    public string fullname;
    public string phone_number;
}
