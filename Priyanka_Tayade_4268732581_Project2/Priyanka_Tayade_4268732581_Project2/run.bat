@echo off
set cur=%CD%

cd compositeTextAnalysisTool\bin\Debug
@echo =============Composite Text Analyzer======================
@echo Language:    C#   
@echo Platform:    TOSHIBA C640, Windows 7 ultimate.                                          
@echo Application: CIS 681 -Software Modeling & Analysis,Fall 2013
@echo Instructor: Dr.Jim Fawcett, CST 2-187, Syracuse University     
@echo Author:	Priyanka Tayade, Syracuse University                  
@echo version: 1.0
@echo Purpose: Perform text search, metadata serch and generate metadata for text file
@echo         with added functionality of recursive search, doc search /A and /O switches

@echo =============Requirement 1 to 4 with O================================

compositeTextAnalysisTool.exe /C CLIENT\ /T blue /T street /mclient,contact /O

@echo =============Requirement 1 to 4 with A================================

compositeTextAnalysisTool.exe /C CLIENT\ /T blue /T street /mclient,contact /A

@echo =============Requirement 5 ===========================================

compositeTextAnalysisTool /C CONTENT\ /T LAND /T turn /mmusic,entertainment /R 

@echo =============Requirement 6 ===========================================

compositeTextAnalysisTool /G /C CONTENT\MUSIC\lyrics1.txt /T priyanka author  /D music /K entertainment,fun,music

@echo =============Doc Reading ===========================================
@echo note if the files is locked there can be reading error

compositeTextAnalysisTool  /c BUSINESS\  /T alternatives considered /T tax /R /O

@echo===============END OF DEMO=============================================

cd %cur%

