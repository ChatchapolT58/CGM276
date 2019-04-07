var io = require('socket.io')(process.env.PORT || 3000);

console.log("server started")

var playerCount = 0;

io.on("connection", function (socket) {

    console.log("cilent connected");

    socket.broadcast.emit('spawn');
    playerCount++;

    for (i = 0; i < playerCount; i++) {
        socket.emit("spawn");
        console.log("spawn existing player");
    }

    socket.on("move", function (data) {
        console.log("move");
    });

    socket.on("disconnect", function () {
        console.log("client disconnect");
        playerCount--;
    });
});