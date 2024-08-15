RapidPayWebAPI

this is the RapidPay code test

I created a new DB named RapidPayDB, including a new table named Cards. Make sure to change the values on the connection string at the appsettings.json it is currently pointing to localhost and receiving the user ID and password.

You'll also see there is a Migrations directory with one migration there, to create the Card table and insert some dummy data. So, it's necessary to run the 'dotnet ef database update' command to run that migration.

Please note that I included swagger so you can see the 3 endpoints required in the test. I also included another endpoint to generate the token to authenticate and be able to call the endpoints.

as this is only for the test I leave the user and password hardcoded so please use 'rapidpay_user' as user and 'rapidpay_password' as password. Once you have a token you can include the Authorization header on your request, so the endpoint will work otherwise an Unahutorized code will be returned.

I hope you like the approach and I'll be pending for any comment.

Thanks!!! :)
