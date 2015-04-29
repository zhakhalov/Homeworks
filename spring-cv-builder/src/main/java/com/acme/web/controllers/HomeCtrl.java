package com.acme.web.controllers;

import org.springframework.stereotype.Controller;
import org.springframework.ui.ModelMap;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@Controller
@RequestMapping("/")
public class HomeCtrl {
    @RequestMapping(value = "/home")
    public String hello() {
        return "index";
    }
}
