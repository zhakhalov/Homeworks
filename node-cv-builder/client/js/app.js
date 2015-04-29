(function(ng, _) {
  ng.module('app', ['ui.bootstrap', 'ui.bootstrap.datepicker', 'ngMessages', 'validation.match'])
  // ========================================== CONSTANTS ==========================================
  .constant('restApiRoot', '/api/')
  // ========================================== SERVICES ==========================================
  .service('UsersSvc', ['$http', 'restApiRoot',
    function ($http, restApiRoot) {
      var self = this;
      self.getUsers = function (cb) {
        $http.get(restApiRoot + 'users')
        .success(function (res) {
          cb(null, res);
        })
        .error(function (res) {
          cb(res);
        });
      };
      self.getUser = function (id, cb) {
        self.getUsers(function (err, users) {
          if (err) {
            cb(err);
          } else {
            cb(null, users[0] || {});
          }
        });
      };
      self.saveUser = function (user, cb) {
        $http.post(restApiRoot + 'users/save', user)
        .success(function (res) {
          cb(null, res);
        })
        .error(function (res) {
          cb(res);
        });
      };
    }])
  // ========================================== FACTORIES ==========================================
  .factory('_', [
    function () {
      return _;
    }])
  // ========================================== CONTROLLERS ==========================================
  .controller('CvCtrl', ['$scope', '$log', 'UsersSvc', '_',
    function ($scope, $log, UsersSvc) {
      $scope.user = {
        skills: [],
        experience: [],
        education: [],
        languages: []
      };
      UsersSvc.getUser('', function (err, user) {
        if (err) {
          $log.error(err);
        } else {
          $scope.user = user;
          user.skills = user.skills || [];
          user.experience = user.experience || [];
          user.education = user.education || [];
          user.languages = user.languages || [];
        }
      });
      $scope.save = function () {
        UsersSvc.saveUser($scope.user);
      };
    }])
  .controller('ListItemCtrl', ['$scope',
    function ($scope) {
      $scope.add = function (listName, item) {
        $scope.$parent.user[listName].push(item);
        $scope.item = {};
      };
      $scope.remove = function (listName, item) {
        $scope.$parent.user[listName] = _.without($scope.$parent.user[listName], item);
      }
    }])
  .controller('DateCtrl', ['$scope',
    function ($scope) {
      $scope.isOpen = false;
      $scope.open = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.isOpen = true;
      };
    }])
})(window.angular, window._);