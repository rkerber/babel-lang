@echo off
echo Building Babel Grammar...
Gold\goldbuilder_main Grammar\Babel.grm
type Grammar\Babel.log
Gold\testgrammar_main Grammar\Babel.cgt ..\Babeler\bin\debug\TestCases.babel
type ..\Babeler\bin\debug\TestCases.out
