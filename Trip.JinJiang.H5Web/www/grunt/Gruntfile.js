//��װ����
module.exports=function(grunt){
	//��������,���в����������Ϣ
	grunt.initConfig({
		//��ȡ package.json ����Ϣ
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
		//jshint�����������Ϣ'
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
				//ð��ǰ�������ɺ�,ð��ǰ��������Դ
					     "../css/datepicker.min.css": 
						["../css/datepicker.css" ],
				 }				 
			 }  
		 },
		 watch:{
			build:{
				files:[
				//    /**/*js ��ָ�ø��ļ����µ����������ļ�.
				"../js/**/*.js"
				//,"../**/*.css" 
				],
				tasks:['jshint','uglify','cssmin'],
				options:{spawn:false}
			}				
		}		 
	});
	
	//����grunt���ǽ�Ҫʹ�ò��
	grunt.loadNpmTasks('grunt-contrib-jshint');//�﷨���
	grunt.loadNpmTasks('grunt-contrib-uglify');//ѹ��js
	grunt.loadNpmTasks('grunt-contrib-cssmin');
	grunt.loadNpmTasks('grunt-contrib-watch');
	
	//����grunt���������ն�������gruntʱ��Ҫ��Щʲô(ע���Ⱥ�˳��)
	grunt.registerTask('default',['jshint','uglify','cssmin','watch']);
	
};