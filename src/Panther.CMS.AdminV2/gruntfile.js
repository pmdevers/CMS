/*
This file in the main entry point for defining grunt tasks and using grunt plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkID=513275&clcid=0x409
*/
module.exports = function (grunt){

    grunt.loadNpmTasks('grunt-contrib-concat');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-contrib-sass');
    grunt.loadNpmTasks('grunt-contrib-htmlmin');

    grunt.initConfig({
        concat: {
            //uglify: {
                targets: {
                    files: {
                        'wwwroot/js/angular.js': ['bower_components/angular/angular.js'],
                        'wwwroot/js/jquery.js': ['bower_components/jquery/dist/jquery.js'],
                        'wwwroot/js/jquery-ui.js': ['bower_components/jquery-ui/jquery-ui.js'],
                        'wwwroot/js/panther.js': [
                            'app/*.js',
                            'app/*/*/*.js'
                        ]
                    } // files
                } // targets
            //} // uglify
        },
        sass: {
            targets: {
                files : {
                    'wwwroot/css/jquery-ui.css' : ['bower_components/jquery-ui/themes/smoothness/jquery-ui.css']
                }, // files
            },
            dist: {
                    options: {
                        style : 'expanded'
                    }, //options
                    files: {
                        'wwwroot/css/site.css': [
                            'assets/scss/*.scss', 'assets/scss/*/*.scss'
                        ]
                    } //files
                } //dist
            //} //targets
        },
        htmlmin : {
            dist: {
                options: {
                    removeComments: true,
                    collapseWhitespace: true
                }, //options
                files: [{
                    expand: true,
                    cwd: 'app/components/',
                    src: ['**/*.html'],
                    dest: 'wwwroot/views/'
                    //'wwwroot/views/': ['app/components/*/*.html'],
                    //'wwwroot/partials/' : ['app/shared/*/*.html']
                    },
                    {
                        expand: true,
                        cwd: 'app/shared/',
                        src: ['**/*.html'],
                        dest: 'wwwroot/views/partials'
                    },
                    {
                        src : 'index.html',
                        dest : 'wwwroot/index.html'
                    
               }]//files
            } //dist
        }, //htmlmin
        watch : {
            options: { livereload: 1337 },
            scripts: {
                files: [
                    'app/*.js',
                    'app/*/*/*.js'
                ],
                //tasks: ['uglify']
                tasks: ['concat']
            }, //scripts
            css: {
                files : ['assets/scss/*.scss', 'assets/scss/*/*.scss'],
                tasks : ['sass']
            },
            html : {
                files: ['app/components/*/*.html', 'app/shared/*/*.html'],
                tasks: ['htmlmin']
            }
        } // watch
    }); //init
    grunt.registerTask('default', 'watch');
}; //exports