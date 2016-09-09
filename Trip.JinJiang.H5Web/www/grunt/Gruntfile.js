//包装函数
module.exports=function(grunt){
	//任务配置,所有插件的配置信息
	grunt.initConfig({
		//获取 package.json 的信息
		pkg:grunt.file.readJSON('package.json'),
	
		uglify:{

			compress:{
				files:{
				'../js/dest/zlDate.js':
				['../js/build/zlDate.js'],
				
				'../js/dest/jquery.spinner.js':
				['../js/build/jquery.spinner.js'],
				
				'../js/dest/controllers.js':
				['../js/build/controllers.js'],
				
				'../js/dest/controllerspay.js':
				['../js/build/controllerspay.js'],
				
				'../js/dest/controllersuser.js':
				['../js/build/controllersuser.js'],
				
				'../js/dest/directive.js':
				['../js/build/directive.js'],
				
				'../js/dest/jscommon.js':
				['../js/build/jscommon.js'],
				
				'../js/dest/service.js':
				['../js/build/service.js']
			}}
		},
		//jshint插件的配置信息'
		 jshint: {
			 build:["../js/build/directive.js","../js/build/controllers.js"],
			 options:{
				 "asi":true,
				 "-W041": false,
				  "evil": true
				 }
           // all: ['src/*.js'],
        },
		 cssmin: {  
			 compress: {  
				files: {  
				//冒号前面是生成后,冒号前面是生成源
					     "../css/datepicker.min.css": 
						["../css/datepicker.css" ],
				 }				 
			 }  
		 },
		 watch:{
			build:{
				files:[
				//    /**/*js 是指该父文件夹下的所有子孙文件.
				"../js/**/*.js"
				//,"../**/*.css" 
				],
				tasks:['jshint','uglify','cssmin'],
				options:{spawn:false}
			}				
		}		 
	});
	
	//告诉grunt我们将要使用插件
	grunt.loadNpmTasks('grunt-contrib-jshint');//语法检查
	grunt.loadNpmTasks('grunt-contrib-uglify');//压缩js
	grunt.loadNpmTasks('grunt-contrib-cssmin');
	grunt.loadNpmTasks('grunt-contrib-watch');
	
	//告诉grunt当我们在终端中输入grunt时需要做些什么(注意先后顺序)
	grunt.registerTask('default',['jshint','uglify','cssmin','watch']);
	
};