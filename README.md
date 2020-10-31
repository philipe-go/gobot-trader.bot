![BOT](./Resources/bird-icon-png.png?Style=centerme)

# GObot Trader Robot - PERSONAL PROJECT #

#######################################################################

**Section 1: Project**

Trading robot created to operate on the future market. (used: c#, DDE, .net and SQL)
Project was developed using concepts and patterns learnt on Object Oriented Programming course. 
Additionally, .Net 4.8 and other libraries were used, alongside with multi-threading operations. 
It was also necessary to use a remote database to handle some aspects consumed by the app. Other 
than that, due to some limitations, it was found the need to implement a windows' inter-process 
communication protocol, known as DDE (Dynamic Data Exchange) in order to exchange information 
between applications.

Progress: 1) First version deployed to be used by market strategy experts.

OBS: the bot was originally designed to work on a software which limits the interface through API,
therefore it was necessary to find a solution as the client's requirement. Thus, it was found a
solution using the protocol supported by the trading software and the key bindings to send trade 
orders.
      
**Section 2: Manual**

      2.1 - Application Manual:

![LoginScreen](./Media/login.png?style=centerme)

To login you will use a default username and password, however you can change then on the source 
code of the class Pombo.cs. Or, create a data base to store those information and access it throw 
the Pombos.cs class created to manage those access and some other aspects. 
- default username: guest
- default password: guest 

![AppDashboard](./Media/dashboard.png?style=centerme)


      2.2 - SourceCode Manual:
      
The following sourcodes need to be changed when you implement your strategy. Those lines where
deliberately removed.

[1 - Code that needs to be changed](https://github.com/philipe-go/GObot-TraderRobot/blob/master/RobotLibrary/Pombos.cs)

[2 - Code that needs to be changed](https://github.com/philipe-go/GObot-TraderRobot/blob/master/Login.cs)

[3 - Code that needs to be changed](https://github.com/philipe-go/GObot-TraderRobot/blob/master/RobotLibrary/Strategy.cs)

```c#
//***UNCOMMENT here when inserting the database link on the class Pombos ***//
//***DELETE HERE when inserting the database link on the class Pombos ***//
/*Insert you Strategy logic here*/
/*Insert your remote database login and credentials here -> */
```

#######################################################################

## External Resources ##

[Ndde Codeplex] (https://ndde.codeplex.com/)


#######################################################################
