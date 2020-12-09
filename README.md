# CCSU OIRA Assessment Software

This was started in September 2020 as part of a class project for CCSU. This application is intended to replace an existing enterprise application used in the Office on Institutional Research and Assessment that is now out of budget. As part of the class, we were tasked with creating the base application, and a few of the initial components.

### Models project

Models.csporj contains all the POCO classes that are used within the solution.

### Data project

The Data project contains two primary responsibilities: first is it contains the DbContext that is used to connect to the database. It also houses all EF migration files, so the database can be recreated at runtime, if it doesn't exist.

### Logic project

The Logic project holds business logic that can be spiked out for reuse. This is intended in case, in the future, there is a need for an API for this application.

### Web project

This is the primary project, containing the .NET Core web application.

### Tests project

This project holds all unit tests for the solution.



## Authors

#### Fall 2020 Semester

Dan Champagne

Parvathy Kumar

Mansimran Singh

Jason Smith

Trung Minh Tri Nguyen

Yash Dalsania

Paul Pasquarelli

Luis Gutierrez

Chen Yang Lin

#### Spring 2020 Semester
