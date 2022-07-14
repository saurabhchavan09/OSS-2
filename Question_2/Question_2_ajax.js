var exp = require('express');
var mysql = require('mysql2');
var bp = require('body-parser');

var app = exp();

//database connection
var con = mysql.createConnection({
	host: "localhost",
	user: "root",
	password: "root",
	database: "users"
});

con.connect(function(err){
	if(!err)
		console.log("Succesfully Connected to database user")
})

//middlewares
app.use(exp.static('myFiles'));
app.use(bp.urlencoded({extended: false}));

app.listen(9000,function(){
	console.log("server started at 9000");
})

app.get('/userform',function(req,res){
	res.sendFile(__dirname+"/userform.html");
});

app.post('/getusername',function(req,res){
	//var q = "select username from user";
	var q = "select username from user where username != '" + req.body.uname + "'";
	//var q ="select username from user where username !=" + req.body.uname;
	con.query(q,function(err,result){
		if(!err)
			res.send(JSON.stringify(result));
	});	
});


app.all('*',function(req,res){
	res.send("Invlaid URL !!!");
});
