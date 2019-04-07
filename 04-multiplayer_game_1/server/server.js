var io = require('socket.io')(process.env.PORT || 3000);

console.log("server started")

io.on("connection", function (socket) { console.log("cilent connected") });