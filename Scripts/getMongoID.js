/*
 * JavaScript Pretty Date
 * Copyright (c) 2011 John Resig (ejohn.org)
 * Licensed under the MIT and GPL licenses.
 */

// Takes an ISO time and returns a string representing how
// long ago the date represents.
function getId(mongoId) {
    var result =
        pad0(mongoId.Timestamp.toString(16), 8) +
        pad0(mongoId.Machine.toString(16), 6) +
        pad0(mongoId.Pid.toString(16), 4) +
        pad0(mongoId.Increment.toString(16), 6);

    return result;
}

function pad0(str, len) {
    var zeros = "00000000000000000000000000";
    if (str.length < len) {
        return zeros.substr(0, len - str.length) + str;
    }

    return str;
}
