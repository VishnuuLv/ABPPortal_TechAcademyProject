Change the appsettings.json file according to your server for the below microservices and Run "update-database" command:
	1. Flight.Services.BookingSchedule
	2. Flight.Services.CouponAPI
	3. Flight.Services.LoggingAPI
	4. Flight.Services.ManageAPI
	5. Flight.Services.UserManagement


Install RabbitMQ and Erlang:
	GOTO "C:\Program Files\RabbitMQ Server\rabbitmq_server-3.9.15\sbin"

	From there run this command "rabbitmq-plugins enable rabbitmq_management"