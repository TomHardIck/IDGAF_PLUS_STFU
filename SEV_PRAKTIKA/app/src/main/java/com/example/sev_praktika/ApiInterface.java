package com.example.sev_praktika;

import com.google.gson.JsonArray;
import com.google.gson.JsonObject;

import java.util.ArrayList;
import retrofit2.Call;
import retrofit2.http.GET;
import retrofit2.http.PUT;
import retrofit2.http.Query;

public interface ApiInterface {
    @PUT("Users/Login")
    Call<Boolean> Authorize(@Query("login") String login, @Query("password") String password);
    @GET("Positions")
    Call<JsonObject> getPositions();
}
