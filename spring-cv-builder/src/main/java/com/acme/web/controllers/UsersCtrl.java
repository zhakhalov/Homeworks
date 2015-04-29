package com.acme.web.controllers;

import com.acme.db.entities.User;
import com.acme.db.repositories.UsersRepository;
import org.bson.types.ObjectId;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.propertyeditors.CustomDateEditor;
import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.web.bind.WebDataBinder;
import org.springframework.web.bind.annotation.*;

import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;

@RestController
@RequestMapping(value = "/api", method = { RequestMethod.GET, RequestMethod.POST })
public class UsersCtrl {

    @Autowired
    private UsersRepository users;

    @RequestMapping(value = "/users", method = RequestMethod.GET)
    public @ResponseBody List<User> getAll() {
        try {
            return this.users.getAll();
        } catch (Exception ex) {
            return null;
        }
    }

    @RequestMapping(value = "/users/{id}", method = RequestMethod.GET)
    public @ResponseBody User getById(@PathVariable String id) {
        try {
            return this.users.getById(new ObjectId(id));
        } catch (Exception ex) {
            return null;
        }
    }

    @RequestMapping(value = "/users/save", method = RequestMethod.POST, headers = {"Content-type=application/json"})
    public @ResponseBody User save(@RequestBody final User user) {
        try {
            this.users.save(user);
            return user;
        } catch (Exception ex) {
            return null;
        }
    }

    @RequestMapping(value = "/users/removeall", method = RequestMethod.GET)
    public @ResponseBody String removeAll() {
        try {
            this.users.removeAll();
            return "all users removed";
        } catch (Exception ex) {
            return null;
        }
    }
}
