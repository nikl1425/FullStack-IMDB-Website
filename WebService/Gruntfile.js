module.exports = function(grunt){

    grunt.loadNpmTasks('grunt-npm-install');
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-concat');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-shell');
    grunt.loadNpmTasks('grunt-exec');


    grunt.initConfig({
        watch: {
            js: {
                files: ['wwwroot/js/**/*.js']
            },
            css: {
                files: ['wwwroot/css/**/*.css']
            },
            html: {
                files: ['wwwroot/*']
            },
        },
    });
    
    
    //Laver en joined task på alle tasks. Dvs ved 'grunt build' så køres alle tasks.
    grunt.registerTask('build', ['watch']);
    

};