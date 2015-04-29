package com.acme.db.repositories;

import com.acme.db.entities.User;
import org.springframework.stereotype.Component;

@Component
public class UsersRepositoryImpl extends RepositoryImpl<User> implements UsersRepository {
    public UsersRepositoryImpl() {
        super(User.class);
    }
}
