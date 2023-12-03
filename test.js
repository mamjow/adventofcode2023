"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var listScores = [];
var myarray = ['abc', 'abc', 'abc', 'abc', 'qwe', 'qwe', 'qwe', 'sss', 'sss', 'sss', 'sss', 'sss'];
myarray.forEach(function (x) { return listScores[x] = (listScores[x] ? ++listScores[x] : 1); });
/// ( listScores[x] !== null ? ++listScores[x] : 1 )
/// dit is gewoon een if 
/// if (listScores[x] !== null) blablabla : blablablaVanElse
var student = {
    // data property
    firstName: 'Monica',
    // accessor property(getter)
    res: function (s, t) {
        return s + "hi" + t;
    }
};
var st = student.res(",", "asd");
console.log(st);
