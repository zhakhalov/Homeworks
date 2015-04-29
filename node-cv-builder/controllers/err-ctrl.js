
module.exports = function (router) {
  router
  .use(function (err, req, res, next) {
    res.status(err.status || 500).send(err.message || 'Something went wrong');
  });
};