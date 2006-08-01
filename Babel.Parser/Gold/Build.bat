@echo off
echo Building Babel Grammer...
Gold\goldbuilder_main Grammer\Babel.grm
type Babel.log
Gold\testgrammar_main Babel.cgt ..\TestCases.babel