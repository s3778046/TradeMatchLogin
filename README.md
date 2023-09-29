# TradeMatchLogin

# Routes


## POST
### https://localhost:7247/api/auth/register
- Registers a new User 
- Adds all user register data to relevant tables and links with foreign keys
- returns a JWT token
### https://localhost:7247/api/auth/login
- Logs in a regigistered user (Checks username and password in login table)
- Returns a JWT token
- 
## GET
### https://localhost:7247/api/user
- Get all registered User (No authorization required)
### https://localhost:7247/api/user/{id}
- Get a single registered User (Authorization required - Bearer token)

## PUT
### https://localhost:7247/api/user
- Updates User (Authorization required - Bearer token)
### https://localhost:7247/api/login
- Updates a Login entry (Authorization required - Bearer token)
### https://localhost:7247/api/address
- Updates an Address entry (Authorization required - Bearer token)
- 
## DELETE
### https://localhost:7247/api/user/{id}
- Deletes a User (Authorization required - Bearer token)
### https://localhost:7247/api/login/{id}
- Deletes a Login entry (Authorization required - Bearer token)
### https://localhost:7247/api/address/{id}
- Deletes an Address entry (Authorization required - Bearer token)



