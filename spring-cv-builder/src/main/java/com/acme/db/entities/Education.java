package com.acme.db.entities;

import org.mongodb.morphia.annotations.Property;

import java.util.Date;

public class Education {
    @Property("startAt")
    private Date startAt;
    @Property("endAt")
    private Date endAt;
    @Property("present")
    private Boolean present;
    @Property("institution")
    private String institution;
    @Property("spec")
    private String spec;
    @Property("degree")
    private String degree;

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

    public String getInstitution() {
        return institution;
    }

    public void setInstitution(String institution) {
        this.institution = institution;
    }

    public String getSpec() {
        return spec;
    }

    public void setSpec(String spec) {
        this.spec = spec;
    }

    public String getDegree() {
        return degree;
    }

    public void setDegree(String degree) {
        this.degree = degree;
    }
}
