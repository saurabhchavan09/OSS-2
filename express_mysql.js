var exp = require('express');
var mysql = require('mysql2');

var app = exp();

var con = mysql.createConnection({
	host: "localhost",
	user: "root",
	password: "root",
	database: "knowitdb"
});

con.connect(function(err){
	if(!err)
		console.log("Connected");
	else
		console.log("connection failed");
});

app.listen(9000, function(){
	console.log("express app up at 9000");
});

app.get('/emps',function(req,res){
     con.query("select * from emp1",function(err,result){
	 if(!err)
	 {
		res.write("<table border=1>");
		result.forEach(function(v){
			res.write("<tr>");
			res.write("<td>"+v.eno+"</td>");
			res.write("<td>"+v.ename+"</td>");
			res.write("<td>"+v.deptno+"</td>");
			res.write("</tr>");	
		});
		res.write("</table>");
		res.end();
	 }
     });

});

app.all('*',function(req,res){
	res.send("Invalid URL !!!");
});