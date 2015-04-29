package com.acme.db.entities;

import org.mongodb.morphia.annotations.Property;

import java.util.Date;

public class Experience {
    @Property("startAt")
    private Date startAt;
    @Property("endAt")
    private Date endAt;
    @Property("present")
    private Boolean present;
    @Property("company")
    private String company;
    @Property("position")
    private String position;

    public Date getStartAt() {
        return startAt;
    }

    public void setStartAt(Date startAt) {
        this.startAt = startAt;
    }

    public Date getEndAt() {
        return endAt;
    }

    public void setEndAt(Date endAt) {
        this.endAt = endAt;
    }

    public Boolean getPresent() {
        return present;
    }

    public void setPresent(Boolean present) {
        this.present = present;
    }

    public String getCompany() {
        return company;
    }

    public void setCompany(String company) {
        this.company = company;
    }

    public String getPosition() {
        return position;
    }

    public void setPosition(String position) {
        this.position = position;
    }
}
