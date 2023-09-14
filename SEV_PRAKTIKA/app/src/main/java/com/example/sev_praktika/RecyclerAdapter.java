package com.example.sev_praktika;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.squareup.picasso.Picasso;

import java.util.ArrayList;
import java.util.List;

public class RecyclerAdapter extends RecyclerView.Adapter<RecyclerAdapter.ViewHolder> {

    private Context context;
    private List<Position> positions;

    public RecyclerAdapter(Context context, List<Position> positions){
        this.context = context;
        this.positions = positions;
    }

    @NonNull
    @Override
    public RecyclerAdapter.ViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(context).inflate(R.layout.item_card, parent, false);
        return new ViewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull RecyclerAdapter.ViewHolder holder, int position) {
        Position pos = positions.get(position);
        holder.posPrice.setText(String.valueOf(pos.positionPrice));
        holder.posName.setText(String.valueOf(pos.positionName));
        Picasso.with(context).load(pos.photoUrl).into(holder.img);
    }

    @Override
    public int getItemCount() {
        return positions.size();
    }

    public class ViewHolder extends RecyclerView.ViewHolder {
        TextView posName, posPrice;
        ImageView img;
        ViewHolder(View view){
            super(view);
            posName = view.findViewById(R.id.posName);
            posPrice = view.findViewById(R.id.posPrice);
            img = view.findViewById(R.id.img);
        }
    }
}
