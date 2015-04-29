package com.acme.db.repositories;

import org.bson.types.ObjectId;

import java.net.UnknownHostException;
import java.util.List;

public interface Repository<E> {
    List<E> getAll() throws UnknownHostException;
    void save(E entity) throws UnknownHostException;
    E getById(ObjectId _id) throws UnknownHostException;
    void removeAll() throws UnknownHostException;
}
