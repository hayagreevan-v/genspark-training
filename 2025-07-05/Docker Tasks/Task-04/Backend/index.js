const express = require('express');
const bodyParser = require('body-parser');
const cors = require('cors');


const app = express();

app.use(bodyParser.urlencoded({extended: true}));
app.use(cors({
    origin: 'http://frontend:80'
}))

app.get('/',(req,res)=>{
    res.send("Response from Server");
})
app.listen(3000, () => {
    console.log("Server started at PORT : 3000")
})