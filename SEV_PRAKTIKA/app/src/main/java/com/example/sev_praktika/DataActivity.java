package com.example.sev_praktika;

import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.os.Bundle;
import android.util.Log;
import android.widget.Toast;

import java.lang.reflect.Type;
import java.util.ArrayList;
import java.util.List;

import com.google.gson.Gson;
import com.google.gson.JsonArray;
import com.google.gson.JsonObject;
import com.google.gson.reflect.TypeToken;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class DataActivity extends AppCompatActivity {
    ApiInterface apiInterface;
    RecyclerView recycler;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_data);

        apiInterface = RequestBuilder.buildRequest().create(ApiInterface.class);
        recycler = findViewById(R.id.recyclerView);

        Call<JsonObject> getPositions = apiInterface.getPositions();
        getPositions.enqueue(new Callback<JsonObject>() {
            @Override
            public void onResponse(Call<JsonObject> call, Response<JsonObject> response) {
                if(response.isSuccessful()){
                    Gson gson = new Gson();
                    Type type = new TypeToken<List<Position>>(){}.getType();
                    List<Position> positions = gson.fromJson(response.body().get("data"), type);
                    recycler.setLayoutManager(new LinearLayoutManager(getApplicationContext()));
                    recycler.setHasFixedSize(true);
                    RecyclerAdapter adapter = new RecyclerAdapter(getApplicationContext(), positions);
                    recycler.setAdapter(adapter);
                }
            }

            @Override
            public void onFailure(Call<JsonObject> call, Throwable t) {
                Toast.makeText(DataActivity.this, t.getMessage(), Toast.LENGTH_SHORT).show();
            }
        });
    }
}