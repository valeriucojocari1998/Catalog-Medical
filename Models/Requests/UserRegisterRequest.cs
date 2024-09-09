namespace Catalog_Medical.Models.Requests;

public record UserRegisterRequest(string UserName, string Password, string Name, string Surname, string Email, string Phone);
