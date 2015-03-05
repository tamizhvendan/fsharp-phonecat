namespace Identity

open Microsoft.AspNet.Identity.EntityFramework
open Microsoft.AspNet.Identity

[<AllowNullLiteral>]
type User() = 
  inherit IdentityUser()
  member val Name = "" with get, set

type UserDbContext() =
  inherit IdentityDbContext<User>("IdentityConnection")

type CreateUserRequest = {Name: string; Password: string; Email : string}