var express = require("express");
var app = express();
// app.use(express.static("front"));
// app.use(express.static(__dirname + '/app'));
app.use(express.static("./"));
app.get("/", function (req, res,next) {
    res.sendFile(__dirname + '/index.html');
});

app.listen(8080, "localhost");
console.log("Server is Listening on port 8080");