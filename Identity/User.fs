namespace Identity

open Microsoft.AspNet.Identity.EntityFramework

[<AllowNullLiteral>]
type User() = 
  inherit IdentityUser()
  member val Name = "" with get, set

type UserDbContext() =
  inherit IdentityDbContext<User>("IdentityConnection")
