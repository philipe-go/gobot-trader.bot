<p align="center"><img src="./Resources/bird-icon-png.png" width="50%" height="50%"></p>

<h1 align="center"> GObot Trader Robot - PROTOTYPE </h1>

<p align="center"> ####################################################################### </p>

**Section 1: Project**

It is a trading robot created to operate on the future | stock market. 
The project was developed using c#, .Net 4.8 and some libraries, alongside with multi-threading programming. 
It was also necessary to use a remote database to handle some aspects consumed by the app. Other 
than that, due to some limitations, it was found the need to implement a windows' inter-process 
communication protocol, known as DDE (Dynamic Data Exchange) in order to exchange information 
between applications.

Progress: 1) First version deployed to be used by market strategy experts.

OBS: the bot was originally designed to work with a software that limits the interface with external applications.
Therefore, it was necessary to find a solution to convey with client's requirements. Thus, I figured a
solution using a protocol supported by the trading software and using key bindings to send trading 
orders.

**Section 2: Asymptotic Analysis - Scalability**

Refering to the mathematical bounds of the application's functions run-time / performance. 
Functions defined in the scope of the application are in general linear *O(n)* or constant *O(1)*, some are *O(n^2)*. Even considering scalability 
of the application it is noted the maintenance of said time complexity. 

**Section 3: Readability**

As this project is a prototype alpha version the code still needs to comply standards (ie SOLID principles), which was later applied at
the private development (for the client).  
Though, for this prototype it is still *not predicted* the refactoring on the near future as my goal is to focus on the final and deployed product .
      
**Section 4: Manual**

      4.1 - Application Manual:

<p align="center"><img src="./Media/login.png"></p>

To login you will use a default username and password, however you can change then in the source 
code of the class Pombos.cs. Or, create a data base to store those information and access it through 
the Pombos.cs class, created to manage those access and some other aspects. 
- default username: guest
- default password: guest 

<p align="center"><img src="./Media/dashboard.png"></p>


      4.2 - SourceCode Manual:
      
The following are few source codes which need to be changed when you implement your strategy. Those lines where
deliberately removed / commented.

[1 - Code that needs to be changed](https://github.com/philipe-go/GObot-TraderRobot/blob/master/RobotLibrary/Pombos.cs#L30-L42)

[2 - Code that needs to be changed](https://github.com/philipe-go/GObot-TraderRobot/blob/master/Login.cs#L102-L122)

[3 - Code that needs to be changed](https://github.com/philipe-go/GObot-TraderRobot/blob/master/RobotLibrary/Strategy.cs#L418)


```c#
//***UNCOMMENT here when inserting the database link on the class Pombos ***//
//***DELETE HERE when inserting the database link on the class Pombos ***//
/*Insert you Strategy logic here*/
/*Insert your remote database login and credentials here -> */
```

#######################################################################

## External Resource ##

[Ndde](https://www.codeplex.com/)

