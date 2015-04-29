package com.acme.db.repositories;

import com.mongodb.MongoClient;
import com.mongodb.MongoClientURI;
import org.bson.types.ObjectId;
import org.mongodb.morphia.Datastore;
import org.mongodb.morphia.Morphia;

import java.net.UnknownHostException;
import java.util.List;

public abstract class RepositoryImpl<E> implements Repository<E> {

    private Class<E> _type;

    public RepositoryImpl(Class<E> type) {
        this._type = type;
    }

    @Override
    public List<E> getAll() throws UnknownHostException {
        return this.createDataStore().find(this._type).asList();
    }

    @Override
    public void save(E entity) throws UnknownHostException  {
        this.createDataStore().save(entity);
    }

    @Override
    public E getById(ObjectId _id) throws UnknownHostException  {
        return this.createDataStore().get(this._type, _id);
    }

    @Override
    public void removeAll() throws UnknownHostException  {
        this.createDataStore().delete(this._type);
    }

    private Datastore createDataStore() throws UnknownHostException {
        MongoClient client = new MongoClient(new MongoClientURI("mongodb://system:123456@ds039010.mongolab.com:39010/cvbuilder"));
        Morphia morphia = new Morphia();
        morphia.map(this._type);
        return morphia.createDatastore(client, "cvbuilder");
    }
}
