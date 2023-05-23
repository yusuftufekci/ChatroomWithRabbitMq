The goal of this exercise is to create a simple browser-based chat application using .NET.
This application should allow several users to talk in a chatroom and also to get stock quotes
from an API using a specific command.


Mandatory Features
● Allow registered users to log in and talk with other users in a chatroom. Done
● Allow users to post messages as commands into the chatroom with the following format
/stock=stock_code Done
● Create a decoupled bot that will call an API using the stock_code as a parameter
(https://stooq.com/q/l/?s=aapl.us&f=sd2t2ohlcv&h&e=csv, here aapl.us is the
stock_code) Done
● The bot should parse the received CSV file and then it should send a message back into
the chatroom using a message broker like RabbitMQ. The message will be a stock quote
using the following format: “APPL.US quote is $93.42 per share”. The post owner will be
the bot. (I send information to rabbitmq but i couldnt get from there to chatroom)
● Have the chat messages ordered by their timestamps and show only the last 50
messages. Done
● Unit test the functionality you prefer. I Couldnt make it.


Need to install rabbitmq,mssql.

More than 2 person can register and join the system. After that they can join the chatroom and talk with each other. If they want to use StockBot they need to write
/stock=stock_code. Stock bot call the api and get the information. But unfortunately they cant see the message in the chatroom. All the message waiting in the que to someone
get them :)

