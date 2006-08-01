@echo off
echo Building Babel Grammer...
Gold\goldbuilder_main Grammer\Babel.grm
type Grammer\Babel.log
Gold\testgrammar_main Grammer\Babel.cgt ..\Babeler\bin\debug\TestCases.babel
type ..\Babeler\bin\debug\TestCases.out
