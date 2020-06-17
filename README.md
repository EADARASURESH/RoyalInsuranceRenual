# RoyalInsuranceRenual-SampleProject
 
 
Acceptance Creteria
-------------------
Write an application that creates renewal invitation letters to customers from a fictitious insurance company.
It should generate a text file containing the body of the letter for each customer record in an input file. The
name of each generated text file should include the customer’s ID and name, and the application should not
generate a text file if there is already one for the same customer in the output location.
Together with this specification document, you will have received the input file needed to generate the letter
(Customer.csv).

Application Structure
---------------------
  Total we have 4 Projects inside the solution
  
1.Royal.Insura.Renual.Test
2.Royal.Insurance.Renual.UIApplication( HTML request will send to MVC controller)                        
3.Royal.Insurance.Renual.DTO  
4.Royal.Insurance.Renual.Application(WebAPi APPlication)   

Request and Responce flow
--------------------------

User will send the request to "Royal.Insurance.Renual.Application(WebAPi APPlication)" , request forword from HTML to mvc contoller
Here actual file convert into byte[] and will send to "Royal.Insurance.Renual.Application(WebAPi APPlication)" using HTTPCLIENT,once received request from mvc controller web api calculate premium and send responce back to mvc controller,once mvc received responce ,here implemented some business login and convert into base64 string ,it will convert into file.

HTTP Client 


Serice Layes(Royal.Insurance.Renual.Application(WebAPi APPlication) 

1.Design Patterns
-----------------
1.Dependency Injection Design Pattern:- using Constructor injection design pattern
2.Sigleton Design Pattern :- for exception tracking 

Solid Principles
----------------
1. Interface Segregation Principle
2.Single Responsibility Principle (SRP)

Unit Test Cases
---------------

created unit test cases for calculating premium in different scenarios.

Royal.Insurance.Renual.Application(WebAPi APPlication)
