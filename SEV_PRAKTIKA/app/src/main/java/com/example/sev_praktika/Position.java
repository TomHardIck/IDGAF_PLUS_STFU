package com.example.sev_praktika;

import java.util.Date;

public class Position {
    public int idPosition;
    public String positionName;
    public int positionQuantity;
    public String positionSlife;
    public int categoryId;
    public String photoUrl;
    public boolean isExists;
    public int dealerId;
    public float positionPrice;

    public Position(int idPosition, String positionName, int positionQuantity, String positionSlife, int categoryId, String photoUrl, boolean isExists, int dealerId, float positionPrice) {
        this.idPosition = idPosition;
        this.positionName = positionName;
        this.positionQuantity = positionQuantity;
        this.positionSlife = positionSlife;
        this.categoryId = categoryId;
        this.photoUrl = photoUrl;
        this.isExists = isExists;
        this.dealerId = dealerId;
        this.positionPrice = positionPrice;
    }

    public int getIdPosition() {
        return idPosition;
    }

    public void setIdPosition(int idPosition) {
        this.idPosition = idPosition;
    }

    public String getPositionName() {
        return positionName;
    }

    public void setPositionName(String positionName) {
        this.positionName = positionName;
    }

    public int getPositionQuantity() {
        return positionQuantity;
    }

    public void setPositionQuantity(int positionQuantity) {
        this.positionQuantity = positionQuantity;
    }

    public String getPositionSlife() {
        return positionSlife;
    }

    public void setPositionSlife(String positionSlife) {
        this.positionSlife = positionSlife;
    }

    public int getCategoryId() {
        return categoryId;
    }

    public void setCategoryId(int categoryId) {
        this.categoryId = categoryId;
    }

    public String getPhotoUrl() {
        return photoUrl;
    }

    public void setPhotoUrl(String photoUrl) {
        this.photoUrl = photoUrl;
    }

    public boolean isExists() {
        return isExists;
    }

    public void setExists(boolean exists) {
        isExists = exists;
    }

    public int getDealerId() {
        return dealerId;
    }

    public void setDealerId(int dealerId) {
        this.dealerId = dealerId;
    }

    public float getPositionPrice() {
        return positionPrice;
    }

    public void setPositionPrice(float positionPrice) {
        this.positionPrice = positionPrice;
    }
}
