var gulp = require('gulp');
var gulpSass = require('gulp-sass');
var gulpCssNano = require('gulp-cssnano');

var sassSrc = './styles/*.scss',
    cssDest = './styles/';

gulp.task('sass', function (done) {
    gulp.src(sassSrc)
        .pipe(gulpSass())
        .pipe(gulpCssNano())
        .pipe(gulp.dest(cssDest));
    done();
});

gulp.task('watch', function () {
    gulp.watch(sassSrc, gulp.series('sass'));
});
