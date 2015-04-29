var UserModel = global.__require('models/user-model.js');

module.exports = function (router) {
  router
  .get('/users/removeall', function (req, res, next) {
    UserModel.remove(function (err) {
      if (err) {
        next(err);
      } else {
        res.send({ message: 'all users removed' });
      }
    });
  })
  .get('/users', function (req, res, next) {
    UserModel.find().lean().exec(function (err, docs) {
      if (err) {
        next(err);
      } else {
        res.send(docs);
      }
    });
  })
  .get('/users/:id', function (req, res, next) {
    UserModel.findById(req.params.id).lean().exec(function (err, doc) {
      if (err) {
        next(err);
      } else {
        res.send(doc);
      }
    });
  })
  .post('/users/save', function (req, res, next) {
    var model = new UserModel(req.body);
    model.save(function (err, doc) {
      if (err) {
        next(err);
      } else {
        res.send(doc);
      }
    });
  });
};