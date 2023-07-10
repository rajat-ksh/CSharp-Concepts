// See https://aka.ms/new-console-template for more information
using System.Security.Cryptography;

Console.WriteLine("Optimise the method GeneratePasswordHashUsingSalt ");

string myPassword = "MyPassword@123";

var specialSalt = new byte[70];


for (int i = 0; i < specialSalt.Length; i++)
{
    specialSalt[i] = 0x20;
}

var hashedPassword = GeneratePasswordHashUsingSalt(myPassword, specialSalt);
Console.WriteLine($"User input password:- {myPassword}.\n Hashedpassword:- {hashedPassword}");
Console.ReadLine();


static string GeneratePasswordHashUsingSalt(string passwordText, byte[] salt)
{
    var iterate = 10000;
    var pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, iterate);
    byte[] hash = pbkdf2.GetBytes(20);
    byte[] hashBytes = new byte[36];
    Array.Copy(salt, 0, hashBytes, 0, 16);
    Array.Copy(hash, 0, hashBytes, 16, 20);
    var passwordHash = Convert.ToBase64String(hashBytes);
    return passwordHash;
}