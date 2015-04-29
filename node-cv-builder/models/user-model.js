var mongoose = require('mongoose');

var SkillSchema = new mongoose.Schema({
  name: { type:Boolean, required: true },
  level: { type:String ,required: true }
});

var ExpSchema = new mongoose.Schema({
  startAt: { type: Date, default: Date.now },
  endAt: { type: Date, default: Date.now },
  present: { type:Boolean, required: true },
  company: { type:String ,required: true },
  position: { type:String ,required: true }
});

var EduSchema = new mongoose.Schema({
  startAt: { type: Date, default: Date.now },
  endAt: { type: Date, default: Date.now },
  present: { type:Boolean, required: true },
  institution: { type:String ,required: true },
  spec: { type:String ,required: true },
  degree: { type:String ,required: true }
});

var LangSchema = new mongoose.Schema({
  name: { type: String, required: true },
  level: { type: String, required: true}
});

var UserSchema = new mongoose.Schema({
  firstName: { type: String, required: true },
  lastName: { type: String, required: true},
  email: { type: String, required: true},
  tel: { type: String, required: true},
  skype: { type: String, required: true},
  address: { type: String, required: true},
  dateOfBirth: { type: Date, required: true},
  objective: { type: String, required: true},
  skills: [SkillSchema],
  experience: [ExpSchema],
  education: [EduSchema],
  languages: [LangSchema],
  createdAt: { type: Date, default: Date.now },
  updatedAt: { type: Date, default: Date.now }
});

module.exports = mongoose.model('UserModel', UserSchema);