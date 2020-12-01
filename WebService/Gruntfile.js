module.exports = function(grunt){

    grunt.loadNpmTasks('grunt-npm-install');
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-concat');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-shell');
    grunt.loadNpmTasks('grunt-exec');


    grunt.initConfig({
        uglify: {
            js: {
                'wwwroot/build/output.min.js': ['wwwroot/js/*.js']
            },
            css: {
                'wwwroot/build/output.min.css' : ['wwwroot/css/*.css']
            },
        },
        concat: {
            js: {
                src: ['wwwroot/js/*.js'],
                dest: 'wwwroot/build/scripts.js',
            },
            css: {
                src: ['wwwroot/css/*.css'],
                dest: 'wwwroot/build/styles.css',
            },
        },
        exec: {
            remove_logs: {
                command: 'rm -f *.log'
            },
            list_files: {
                command: 'npm install moment',
                stdout: true
            },
        },
        watch: {
            js: {
                files: ['wwwroot/js/**/*.js'],
                tasks: ['concat'],
            },
            css: {
                files: ['wwwroot/css/**/*.css'],
                tasks: ['concat'],
            },
            html: {
                files: ['wwwroot/index.html']
            },
        },
    });
    
    //Brug denne fremgangsmåde til at installere npm packages automatisk.
    //Her installerer vi 'async' pakkerne.
    grunt.registerTask('pack', ['exec']);
    
    //Laver en joined task på alle tasks. Dvs ved 'grunt build' så køres alle tasks.
    grunt.registerTask('build', ['uglify', 'concat', 'watch']);
    

};