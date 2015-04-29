global.__require = function (path) {
  return require(require('path').join(__dirname, path));
};
//-----------------------------------------------------------------
// npm modules
//-----------------------------------------------------------------
var http = require('http');
var express = require('express');
var path = require('path');
var mongoose = require('mongoose');
var cookieParser = require('cookie-parser');
var bodyParser = require('body-parser');

//-----------------------------------------------------------------
// modules
//-----------------------------------------------------------------

//-----------------------------------------------------------------
// controllers
//-----------------------------------------------------------------

var UsersCtrl = require('./controllers/users-ctrl.js');
var ErrCtrl = require('./controllers/err-ctrl.js');

//-----------------------------------------------------------------
// bootstrap app
//-----------------------------------------------------------------

// -----  express

var app = express();
var apiRouter = express.Router();
app.set('view engine', 'jade');
app.use(express.static(path.join(__dirname, 'client')));
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: false }));
app.use(cookieParser());
app.use('/api', apiRouter);

var server = http.createServer(app);

// -----  controllers

UsersCtrl(apiRouter);
ErrCtrl(apiRouter);

//-----------------------------------------------------------------
// start app
//-----------------------------------------------------------------

mongoose.connect('mongodb://system:123456@ds039010.mongolab.com:39010/cvbuilder', function (err) {
  if (err) {
    console.error({ category:'database', message: 'could not connect', err: err });
  } else {
    console.log('database connected');
  }
});

server.listen(1337, '127.0.0.1', function () {
  var addr = server.address();
  console.log('server started at ' + addr.port);
});
