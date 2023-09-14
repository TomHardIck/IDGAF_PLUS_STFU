package com.example.sev_praktika;

public class User {
    public int IdUser;
    public String UserLogin;
    public String UserPassword;
    public String UserFname;
    public String UserLname;
    public int UserRoleId;
    public String Salt;

    public User(int idUser, String userLogin, String userPassword, String userFname, String userLname, int userRoleId, String salt) {
        IdUser = idUser;
        UserLogin = userLogin;
        UserPassword = userPassword;
        UserFname = userFname;
        UserLname = userLname;
        UserRoleId = userRoleId;
        Salt = salt;
    }

    public int getIdUser() {
        return IdUser;
    }

    public void setIdUser(int idUser) {
        IdUser = idUser;
    }

    public String getUserLogin() {
        return UserLogin;
    }

    public void setUserLogin(String userLogin) {
        UserLogin = userLogin;
    }

    public String getUserPassword() {
        return UserPassword;
    }

    public void setUserPassword(String userPassword) {
        UserPassword = userPassword;
    }

    public String getUserFname() {
        return UserFname;
    }

    public void setUserFname(String userFname) {
        UserFname = userFname;
    }

    public String getUserLname() {
        return UserLname;
    }

    public void setUserLname(String userLname) {
        UserLname = userLname;
    }

    public int getUserRoleId() {
        return UserRoleId;
    }

    public void setUserRoleId(int userRoleId) {
        UserRoleId = userRoleId;
    }

    public String getSalt() {
        return Salt;
    }

    public void setSalt(String salt) {
        Salt = salt;
    }
}
