# Topic: Web Authentication & Authorisation.

### Course: Cryptography & Security
### Author: Mustuc Mihai

----
&ensp;&ensp;&ensp; Authentication & authorization are 2 of the main security goals of IT systems and should not be used interchangibly. Simply put, during authentication the system verifies the identity of a user or service, and during authorization the system checks the access rights, optionally based on a given user role.

&ensp;&ensp;&ensp; There are multiple types of authentication based on the implementation mechanism or the data provided by the user. Some usual ones would be the following:
- Based on credentials (Username/Password);
- Multi-Factor Authentication (2FA, MFA);
- Based on digital certificates;
- Based on biometrics;
- Based on tokens.

&ensp;&ensp;&ensp; Regarding authorization, the most popular mechanisms are the following:
- Role Based Access Control (RBAC): Base on the role of a user;
- Attribute Based Access Control (ABAC): Based on a characteristic/attribute of a user.

###  JSON Web Token?

&ensp;&ensp;&ensp; JSON Web Token  is a proposed Internet standard for creating data with optional signature and/or optional encryption whose payload holds JSON that asserts some number of claims. The tokens are signed either using a private secret or a public/private key.


### Header 

Identifies which algorithm is used to generate the signature
HS256 indicates that this token is signed using HMAC-SHA256.
Typical cryptographic algorithms used are HMAC with SHA-256 (HS256) and RSA signature with SHA-256 (RS256). JWA (JSON Web Algorithms) RFC 7518 introduces many more for both authentication and encryption.

### Payload 

Contains a set of claims. The JWT specification defines seven Registered Claim Names which are the standard fields commonly included in tokens.[1] Custom claims are usually also included, depending on the purpose of the token.
This example has the standard Issued At Time claim (iat) and a custom claim (loggedInAs).

### Signature

Securely validates the token. The signature is calculated by encoding the header and payload using Base64url Encoding RFC 4648 and concatenating the two together with a period separator. That string is then run through the cryptographic algorithm specified in the header. This example uses HMAC-SHA256 with a shared secret (public key algorithms are also defined). The Base64url Encoding is similar to base64, but uses different non-alphanumeric characters and omits padding.

## Implementation 
&ensp;&ensp;&ensp; First of all I configure my API to work with JWT Tokens and added some registered claims.
```
private string Generate(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Email, user.EmailAddress),
                new Claim(ClaimTypes.GivenName, user.GivenName),
                new Claim(ClaimTypes.Surname, user.Surname),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
```



&ensp;&ensp;&ensp; I add a models folder to hold our three models. One will be the user model,  that's gonna hold all the data about a user. This is gonna be some sort of like an asp.net user entity in the ef core but obviously, we're not going to work within the ef core database with the sql server database uh, therefore, we're just going to simulate the presence of it. 
```
public class UserModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public string Role { get; set; }
        public string Surname { get; set; }
        public string GivenName { get; set; }
    }
```


&ensp;&ensp;&ensp; So model user login that's going to have only a username and password and the user constants.
```
public class UserLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
```
&ensp;&ensp;&ensp; User constants, this just holds a list of two users.

```
public class UserConstants
    {
        public static List<UserModel> Users = new List<UserModel>()
        {
            new UserModel() { Username = "mihai", EmailAddress = "mihai.admin@email.com", Password = "pass", GivenName = "Mihai", Surname = "Mustuc", Role = "Administrator" },
            new UserModel() { Username = "valerian", EmailAddress = "valerian.seller@email.com", Password = "pass", GivenName = "Valerian", Surname = "cucu", Role = "Seller" },
        };
    }
```
&ensp;&ensp;&ensp;First api controller that I want to add is the login controller which is the one that will authenticate the user as well as will uh generate the token.
```
[Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            var user = Authenticate(userLogin);

            if (user != null)
            {
                var token = Generate(user);
                return Ok(token);
            }

            return NotFound("User not found");
        }
```

&ensp;&ensp;&ensp;User controller and I  define a bunch of endpoints as part of this user controller to be able to show whether or not we can hit those endpoints if we are authenticated um and if we're not .

&ensp;&ensp;&ensp;A JWT token that I may use to submit requests that call for authorisation is sent to me in the response body following a successful login. Now that some API endpoints are protected with the [Authorize] property, I can send calls to them. A Http 401 Unauthorized error will appear if I attempt to access them. Now, I can only visit permitted endpoints that pertain to my particular position.
```
public class UserController : ControllerBase
    {


        [HttpGet("Admins")]
        [Authorize(Roles = "Administrator")]
        public IActionResult AdminsEndpoint()
        {
            var currentUser = GetCurrentUser();

            return Ok($"Hi {currentUser.GivenName}, you are an {currentUser.Role}");
        }


        [HttpGet("Sellers")]
        [Authorize(Roles = "Seller")]
        public IActionResult SellersEndpoint()
        {
            var currentUser = GetCurrentUser();

            return Ok($"Hi {currentUser.GivenName}, you are a {currentUser.Role}");
        }
```



## Screenshots



![image](https://cdn.discordapp.com/attachments/1035278370105204786/1052718236304687104/image.png)

![image](https://cdn.discordapp.com/attachments/1035278370105204786/1052718443469733918/image.png)

![image](https://cdn.discordapp.com/attachments/1035278370105204786/1052718704938471444/image.png)
## Conclusions

I learned about authorisation and authentication throughout that laboratory work.