<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8"/>
    <title></title>
    <link href="client/css/cv.css" rel="stylesheet"/>
    <link href="client/css/style.css" rel="stylesheet"/>
    <!--<link href="bower_components/bootstrap/dist/css/bootstrap.css" rel="stylesheet"/>-->
    <link href="client/bower_components/font-awesome/css/font-awesome.css" rel="stylesheet"/>
    <script src="client/bower_components/jquery/dist/jquery.js"></script>
    <script src="client/bower_components/bootstrap/dist/js/bootstrap.js"></script>
    <script src="client/bower_components/angular/angular.js"></script>
    <script src="client/bower_components/angular-bootstrap/ui-bootstrap.js"></script>
    <script src="client/bower_components/angular-bootstrap/ui-bootstrap-tpls.js"></script>
    <script src="client/bower_components/angular-messages/angular-messages.js"></script>
    <script src="client/bower_components/angular-validation-match/dist/angular-input-match.js"></script>
    <script src="client/bower_components/underscore/underscore.js"></script>
    <script src="client/js/app.js"></script>
</head>
<body ng-app="app">

<div class="container" ng-controller="CvCtrl">
<h1 class="page-header">CV Builder</h1>
<tabset>
<!-- ========================================= GENERAL TAB ========================================= -->
<tab>
    <tab-heading><i class="fa fa-user"></i>General information</tab-heading>
    <form>
        <div class="form-group">
            <label>First name:</label>
            <input class="form-control"
                   type="text"
                   ng-model="user.firstName"
                   required/>
        </div>
        <div class="form-group">
            <label>Last name:</label>
            <input class="form-control"
                   type="text"
                   ng-model="user.lastName"
                   required/>
        </div>
        <div class="form-group"
             ng-controller="DateCtrl">
            <label>Date of birth:</label>

            <div class="row">
                <div class="col-md-6">
                    <p class="input-group">
                        <input type="text"
                               class="form-control"
                               datepicker-popup="dd-MMMM-yyyy"
                               ng-model="user.dateOfBirth"
                               is-open="isOpen"
                               close-text="Close"/>
              <span class="input-group-btn">
                <button type="button"
                        class="btn btn-default"
                        ng-click="open($event)">
                    <i class="fa fa-calendar"></i>
                </button>
              </span>
                    </p>
                </div>
            </div>
        </div>
        <div class="form-group">
            <label>Address:</label>
            <input class="form-control"
                   type="text"
                   ng-model="user.address"
                   required/>
        </div>
        <div class="form-group">
            <label>Telephone number:</label>
            <input class="form-control"
                   type="tel"
                   ng-model="user.tel"
                   required/>
        </div>
        <div class="form-group">
            <label>E-mail:</label>
            <input class="form-control"
                   type="email"
                   ng-model="user.email"
                   required/>
        </div>
        <div class="form-group">
            <label>Skype:</label>
            <input class="form-control"
                   type="text"
                   ng-model="user.skype"
                   required/>
        </div>
        <div class="form-group">
            <label>Objective:</label>
            <input class="form-control"
                   type="text" ng-model="user.objective"
                   ng-model-options="{ getterSetter: true }"
                   required/>
        </div>
    </form>
</tab>
<!-- ========================================= SKILLS TAB ========================================= -->
<tab>
    <tab-heading><i class="fa fa-tags"></i>Skills</tab-heading>
    <div ng-repeat="item in user.skills" ng-controller="ListItemCtrl">
        <form class="form-inline">
            <div class="form-group">
                <input class="form-control"
                       type="text"
                       ng-model="item.name"
                       required/>
                <select class="form-control"
                        ng-model="item.level"
                        ng-options="f for f in ['Beginner', 'Good', 'Strong']"
                        required>
                    <option></option>
                </select>
                <button class="btn" ng-click="remove('skills', item)">
                    <i class="fa fa-minus-circle"></i>Remove
                </button>
            </div>
        </form>
    </div>
    <!-- ========================================= ADD NEW SKILL ========================================= -->
    <form class="form-inline" ng-controller="ListItemCtrl">
        <fieldset>
            <legend>Add skill:</legend>
            <div class="form-group">
                <input class="form-control"
                       type="text"
                       ng-model="skill.name"
                       required/>
                <select class="form-control"
                        ng-model="skill.level"
                        ng-options="f for f in ['Beginner', 'Good', 'Strong']"
                        required>
                    <option></option>
                </select>
                <button class="btn"
                        ng-click="user.skills.push(skill); skill = {}">
                    <i class="fa fa-plus-circle"></i>Add
                </button>
            </div>
        </fieldset>
    </form>
</tab>
<!-- ========================================= EXPERIENCE TAB ========================================= -->
<tab>
    <tab-heading><i class="fa fa-briefcase"></i>Experience</tab-heading>
    <div ng-repeat="item in user.experience">
        <form ng-controller="ListItemCtrl">
            <fieldset>
                <div class="form-group">
                    <label>Company:</label>
                    <input class="form-control" type="text" ng-model="item.company" required/>
                </div>
                <div class="form-group">
                    <label>Position:</label>
                    <input class="form-control" type="text" ng-model="item.position" required/>
                </div>
                <div class="form-group"
                     ng-controller="DateCtrl">
                    <label>Start date:</label>

                    <div class="row">
                        <div class="col-md-6">
                            <p class="input-group">
                                <input type="text"
                                       class="form-control"
                                       datepicker-popup="dd-MMMM-yyyy"
                                       ng-model="item.startAt"
                                       is-open="isOpen"
                                       close-text="Close"/>
              <span class="input-group-btn">
                <button type="button"
                        class="btn btn-default"
                        ng-click="open($event)">
                    <i class="fa fa-calendar"></i>
                </button>
              </span>
                            </p>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <label>End date:</label>

                    <div class="row">
                        <div class="col-md-6"
                             ng-controller="DateCtrl">
                            <p class="input-group">
                                <input type="text"
                                       class="form-control"
                                       datepicker-popup="dd-MMMM-yyyy"
                                       ng-model="item.endAt"
                                       is-open="isOpen"
                                       ng-disabled="item.present"
                                       close-text="Close"/>
              <span class="input-group-btn">
                <button type="button"
                        class="btn btn-default"
                        ng-click="open($event)">
                    <i class="fa fa-calendar"></i>
                </button>
              </span>
                            </p>
                        </div>
                        <div class="col-md-6 checkbox">
                            <lable>
                                <input type="checkbox"
                                       ng-model="item.present">
                                Present
                                </input>
                            </lable>
                        </div>
                    </div>
                </div>
                <button class="btn"
                        ng-click="remove('experience', item)">
                    <i class="fa fa-minus-circle"></i>Remove
                </button>
            </fieldset>
        </form>
    </div>
    <!-- ========================================= ADD NEW EXPERIENCE ========================================= -->
    <form>
        <fieldset>
            <legend>Add experience:</legend>
            <div class="form-group">
                <label>Company:</label>
                <input class="form-control" type="text" ng-model="experience.company" required/>
            </div>
            <div class="form-group">
                <label>Position:</label>
                <input class="form-control" type="text" ng-model="experience.position" required/>
            </div>
            <div class="form-group">
                <label>Start date:</label>

                <div class="row">
                    <div class="col-md-6"
                         ng-controller="DateCtrl">
                        <p class="input-group">
                            <input type="text"
                                   class="form-control"
                                   datepicker-popup="dd-MMMM-yyyy"
                                   ng-model="experience.startAt"
                                   is-open="isOpen"
                                   close-text="Close"/>
              <span class="input-group-btn">
                <button type="button"
                        class="btn btn-default"
                        ng-click="open($event)">
                    <i class="fa fa-calendar"></i>
                </button>
              </span>
                        </p>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <label>End date:</label>

                <div class="row">
                    <div class="col-md-6" ng-controller="DateCtrl">
                        <p class="input-group">
                            <input type="text"
                                   class="form-control"
                                   datepicker-popup="dd-MMMM-yyyy"
                                   ng-model="experience.endAt"
                                   is-open="isOpen"
                                   ng-disabled="experience.present"
                                   close-text="Close"/>
              <span class="input-group-btn">
                <button type="button"
                        class="btn btn-default"
                        ng-click="open($event)">
                    <i class="fa fa-calendar"></i>
                </button>
              </span>
                        </p>
                    </div>
                    <div class="col-md-6 checkbox">
                        <lable>
                            <input type="checkbox"
                                   ng-model="experience.present">
                            Present
                            </input>
                        </lable>
                    </div>
                </div>
            </div>
            <button class="btn"
                    ng-click="user.experience.push(experience)">
                <i class="fa fa-plus-circle"></i>Add
            </button>
        </fieldset>
    </form>
</tab>
<!-- ========================================= EDUCATION TAB ========================================= -->
<tab>
    <tab-heading><i class="fa fa-graduation-cap"></i>Education</tab-heading>
    <div ng-repeat="item in user.education">
        <form ng-controller="ListItemCtrl">
            <fieldset>
                <div class="form-group">
                    <label>Institution:</label>
                    <input class="form-control" type="text" ng-model="item.institution" required/>
                </div>
                <div class="form-group">
                    <label>Specialization:</label>
                    <input class="form-control" type="text" ng-model="item.specialization" required/>
                </div>
                <div class="form-group">
                    <label>Degree:</label>
                    <input class="form-control" type="text" ng-model="item.degree" required/>
                </div>
                <div class="form-group" ng-controller="DateCtrl">
                    <label>Start date:</label>

                    <div class="row">
                        <div class="col-md-6">
                            <p class="input-group">
                                <input type="text"
                                       class="form-control"
                                       datepicker-popup="dd-MMMM-yyyy"
                                       ng-model="item.startAt"
                                       is-open="isOpen"
                                       close-text="Close"/>
              <span class="input-group-btn">
                <button type="button" class="btn btn-default" ng-click="open($event)">
                    <i class="fa fa-calendar"></i>
                </button>
              </span>
                            </p>
                        </div>
                    </div>
                </div>
                <div class="form-group" ng-controller="DateCtrl">
                    <label>End date:</label>

                    <div class="row">
                        <div class="col-md-6">
                            <p class="input-group">
                                <input type="text"
                                       class="form-control"
                                       datepicker-popup="dd-MMMM-yyyy"
                                       ng-model="item.endAt"
                                       is-open="isOpen"
                                       ng-disabled="item.present"
                                       close-text="Close"/>
              <span class="input-group-btn">
                <button type="button"
                        class="btn btn-default"
                        ng-click="open($event)">
                    <i class="fa fa-calendar"></i>
                </button>
              </span>
                            </p>
                        </div>
                        <div class="col-md-6 checkbox">
                            <lable>
                                <input type="checkbox"
                                       ng-model="item.present">
                                Present
                                </input>
                            </lable>
                        </div>
                    </div>
                </div>
                <button class="btn" ng-click="remove('education', item)">
                    <i class="fa fa-minus-circle"></i>Remove
                </button>
            </fieldset>
        </form>
    </div>
    <!-- ========================================= ADD NEW EDUCATION ========================================= -->
    <form>
        <fieldset>
            <legend>Add education:</legend>
            <div class="form-group">
                <label>Institution:</label>
                <input class="form-control" type="text" ng-model="education.institution" required/>
            </div>
            <div class="form-group">
                <label>Specialization:</label>
                <input class="form-control" type="text" ng-model="education.specialization" required/>
            </div>
            <div class="form-group">
                <label>Degree:</label>
                <input class="form-control" type="text" ng-model="education.degree" required/>
            </div>
            <div class="form-group" ng-controller="DateCtrl">
                <label>Start date:</label>

                <div class="row">
                    <div class="col-md-6">
                        <p class="input-group">
                            <input type="text"
                                   class="form-control"
                                   datepicker-popup="dd-MMMM-yyyy"
                                   ng-model="education.startAt"
                                   is-open="isOpen"
                                   close-text="Close"/>
              <span class="input-group-btn">
                <button type="button" class="btn btn-default" ng-click="open($event)">
                    <i class="fa fa-calendar"></i>
                </button>
              </span>
                        </p>
                    </div>
                </div>
            </div>
            <div class="form-group" ng-controller="DateCtrl">
                <label>End date:</label>

                <div class="row">
                    <div class="col-md-6">
                        <p class="input-group">
                            <input type="text"
                                   class="form-control"
                                   datepicker-popup="dd-MMMM-yyyy"
                                   ng-model="education.endAt"
                                   is-open="isOpen"
                                   ng-disabled="education.present"
                                   close-text="Close"/>
              <span class="input-group-btn">
                <button type="button" class="btn btn-default" ng-click="open($event)">
                    <i class="fa fa-calendar"></i>
                </button>
              </span>
                        </p>
                    </div>
                    <div class="col-md-6 checkbox">
                        <lable>
                            <input type="checkbox"
                                   ng-model="education.present">
                            Present
                            </input>
                        </lable>
                    </div>
                </div>
            </div>
            <button class="btn" ng-click="user.education.push(education); education = {} ">
                <i class="fa fa-plus-circle"></i>Add
            </button>
        </fieldset>
    </form>
</tab>
<!-- ========================================= LANGUAGES TAB ========================================= -->
<tab>
    <tab-heading><i class="fa fa-language"></i>Languages</tab-heading>
    <div ng-repeat="item in user.languages" ng-controller="ListItemCtrl">
        <form class="form-inline">
            <div class="form-group">
                <input class="form-control"
                       type="text"
                       ng-model="item.name"
                       required/>
                <select class="form-control"
                        ng-model="item.level"
                        ng-options="f for f in ['Beginner', 'Intermediate', 'Strong', 'Native']"
                        required>
                    <option></option>
                </select>
                <button class="btn" ng-click="remove('languages', item)">
                    <i class="fa fa-minus-circle"></i>Remove
                </button>
            </div>
        </form>
    </div>
    <!-- ========================================= ADD NEW LANGUAGE ========================================= -->
    <form class="form-inline">
        <fieldset>
            <legend>Add language:</legend>
            <div class="form-group">
                <input class="form-control"
                       type="text"
                       ng-model="language.name"
                       required/>
                <select class="form-control"
                        ng-model="language.level"
                        ng-options="f for f in ['Beginner', 'Intermediate', 'Strong', 'Native']"
                        required>
                    <option></option>
                </select>
                <button class="btn"
                        ng-click="user.languages.push(language); language = {}">
                    <i class="fa fa-plus-circle"></i>Add
                </button>
            </div>
        </fieldset>
    </form>
</tab>
<!-- ========================================= PREVIEW ========================================= -->
<tab>
    <tab-heading><i class="fa fa-user"></i>Preview</tab-heading>
    <div class="page">
        <div class="row">
            <div class="col-xs-7">
                <table class="table">
                    <tr>
                        <td><h3>{{user.firstName + ' ' + user.lastName}}</h3></td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                </table>
                <i class="fa fa-crosshairs"></i><i>Objective:</i> {{user.objective}}
            </div>
            <div class="col-xs-5">
                <table class="table">
                    <tr>
                        <td><i class="fa fa-envelope"></i> {{user.email}}</td>
                    </tr>
                    <tr>
                        <td><i class="fa fa-phone"></i> {{user.tel}}</td>
                    </tr>
                    <tr>
                        <td><i class="fa fa-home"></i> {{user.address}}</td>
                    </tr>
                    <tr>
                        <td><i class="fa fa-skype"></i> {{user.skype}}</td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="well well-sm">
            <h5 class="page-header"><i class="fa fa-tags"></i> Languages and Technologies</h5>
            <span class="label label-default" ng-repeat="item in user.skills">{{item.name}}</span>
        </div>
        <div class="well well-sm">
            <h5 class="page-header"><i class="fa fa-briefcase"></i> Experience</h5>

            <div class="row" ng-repeat="item in user.experience">
                <div class="col-xs-9">
                    <span class="h5">{{item.position}}</span> at {{item.company}}
                </div>
                <div class="col-xs-3">
                    <i class="fa fa-calendar"></i> {{item.startAt | date: 'MMM. yyyy'}} -
                    <span ng-if="item.present">present</span>
                    <span ng-if="!item.present">{{item.endAt | date: 'MMM. yyyy'}}</span>
                </div>
            </div>
        </div>
        <div class="well well-sm">
            <h5 class="page-header"><i class="fa fa-graduation-cap"></i> Education</h5>

            <div class="row" ng-repeat="item in user.education">
                <div class="col-xs-9">
                    <span class="h5">{{item.institution}}</span>
                </div>
                <div class="cv-col-xs-3">
                    <i class="fa fa-calendar"></i> {{item.startAt | date: 'MMM. yyyy'}} -
                    <span ng-if="item.present">present</span>
                    <span ng-if="!item.present">{{item.endAt | date: 'MMM. yyyy'}}</span>
                </div>
                <ul>
                    <li ng-if="!item.present">{{item.degree}}'s degree</li>
                    <li>Specialization - {{item.specialization}}</li>
                </ul>
            </div>
        </div>
        <div class="well well-sm">
            <h5 class="page-header"><i class="fa fa-info-circle"></i> Additional information</h5>

            <div class="row">
                <div class="col-xs-4">
                    <i class="fa fa-language"></i> Languages:
                    <ul>
                        <li ng-repeat="item in user.languages"><i>{{item.name}}</i> - {{item.level}}</li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</tab>
</tabset>
<div class="row">
    <div class="col-md-3"></div>
    <div class="col-md-6">
        <button class="btn btn-success btn-block"
                ng-click="save()">
            Save
        </button>
    </div>
</div>
</div>
</body>
</html>