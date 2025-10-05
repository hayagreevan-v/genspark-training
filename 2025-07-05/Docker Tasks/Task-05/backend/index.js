const express = require('express');
const db = require("mongoose");
require('dotenv').config();

const app = express();
app.listen(process.env.PORT || 3000,()=>{
    console.log("Server started running!");
    db.connect(process.env.MONGODB).then(() => {
        console.log("DB Conntected");
    })

})