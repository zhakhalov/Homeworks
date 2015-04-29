package com.acme.db.entities;

import org.mongodb.morphia.annotations.Property;

import java.util.Date;

public class Skill {
    @Property("name")
    private String name;
    @Property("level")
    private String level;

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getLevel() {
        return level;
    }

    public void setLevel(String level) {
        this.level = level;
    }
}

