using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ReiDoChopp.Domain.Emails.Repositories;
using ReiDoChopp.Domain.Emails.Repositories.Models;
using ReiDoChopp.Domain.Roles.Entities;
using ReiDoChopp.Domain.Roles.Repositories;
using ReiDoChopp.Domain.Users.Entities;
using ReiDoChopp.Domain.Users.Exceptions;
using ReiDoChopp.Domain.Users.Repositories;
using ReiDoChopp.Domain.Users.Services.Interfaces;
using ReiDoChopp.Domain.Utils.Exceptions;
using System.Data;
using System.Security.Claims;

namespace ReiDoChopp.Domain.Users.Services
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<User> userManager;
        private readonly ITokensService tokensService;
        private readonly IEmailsRepository emailsRepository;
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly RoleManager<Role> roleManager;
        private readonly IUsersRepository usersRepository;
        private readonly IRolesRepository rolesRepository;

        public UsersService(
            UserManager<User> userManager,
            ITokensService tokensService,
            IHttpContextAccessor httpContextAccessor,
            RoleManager<Role> roleManager,
            IUsersRepository usersRepository,
            IRolesRepository rolesRepository,
            IEmailsRepository emailsRepository,
            IConfiguration configuration
        )
        {
            this.userManager = userManager;
            this.tokensService = tokensService;
            this.httpContextAccessor = httpContextAccessor;
            this.roleManager = roleManager;
            this.usersRepository = usersRepository;
            this.rolesRepository = rolesRepository;
            this.emailsRepository = emailsRepository;
            this.configuration = configuration;
        }

        public void MatchPassword(string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                throw new Exception("Passwords do not match");
            }
        }

        public async Task<User> InsertAsync(string firstName, string lastName, string email, string password)
        {
            User existingUser = await userManager.FindByEmailAsync(email);

            if (existingUser != null)
            {
                throw new UserAlreadyExistsException(email);
            }

            User user = new User(firstName, lastName, email);

            IdentityResult result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                return user;
            }
            else
            {
                throw new IdentityException("Could not create user", result.Errors);
            }
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            User user = await userManager.FindByEmailAsync(email);

            if (user != null && await userManager.CheckPasswordAsync(user, password))
            {
                string token = await tokensService.GenerateToken(user);
                return token;
            }

            throw new InvalidLoginException(email, password);

        }

        public async Task<User> ValidateAsync(int id)
        {
            User user = await userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                throw new RegisterNotFound(id);
            }

            return user;
        }

        public async Task ForgotPasswordAsync(User user)
        {
            string resetToken = await userManager.GeneratePasswordResetTokenAsync(user);
            string tokenEncoded = Uri.EscapeDataString(resetToken);

            string resetUrl = $"{configuration["ClientUrl"]}alterar-senha?email={user.Email}&token={tokenEncoded}";

            string text = @$"
                <!DOCTYPE html>
                <html lang=""pt-BR"">
                  <head>
                    <meta charset=""UTF-8"" />
                    <style>
                      body {{
                        font-family: Arial, sans-serif;
                        background-color: #f4f4f4;
                        padding: 20px;
                        color: #333;
                      }}
                      .container {{
                        max-width: 500px;
                        margin: auto;
                        background-color: #FFEFC1;
                        padding: 30px;
                        border-radius: 8px;
                        box-shadow: 0 2px 6px rgba(0, 0, 0, 0.1);
                        text-align: center;
                      }}
                      .logo {{
                        max-width: 150px;
                        margin-bottom: 20px;
                      }}
                      h2 {{
                        color: #2c3e50;
                      }}
                      p {{
                        font-size: 16px;
                        line-height: 1.5;
                      }}
                      .button {{
                        display: inline-block;
                        margin-top: 20px;
                        padding: 12px 24px;
                        background-color: #C83537;
                        color: white;
                        text-decoration: none !important;
                        border-radius: 5px;
                        font-weight: bold;
                        transition: background-color 0.3s ease;
                      }}
                      .footer {{
                        margin-top: 30px;
                        font-size: 12px;
                        color: #888;
                      }}
                    </style>
                  </head>
                  <body>
                    <div class=""container"">
                      <img
                        src=""https://raw.githubusercontent.com/BernardoAbaurre/bernardo-dev-images/refs/heads/main/rei-do-chopp/logo-rei-do-chopp.png""
                        alt=""Logo""
                        class=""logo""
                      />
                      <h2>Redefinição de Senha</h2>
                      <p>
                        Olá <b>{user.FirstName}</b>, Recebemos uma solicitação para redefinir sua senha. Se foi você quem solicitou, clique no botão abaixo para continuar:
                      </p>
                      <a href=""{resetUrl}""
                        style=""display: inline-block; padding: 12px 24px; background-color: #C83537; color: white; text-decoration: none !important; border-radius: 5px; font-weight: bold;"">
                        Esqueci Minha Senha
                      </a>

                      <p style=""margin-top: 20px;"">
                        Se você não solicitou essa alteração, pode ignorar este e-mail com segurança.
                      </p>
                      <div class=""footer"">
                        &copy; 2025 Rei do Chopp. Todos os direitos reservados.
                      </div>
                    </div>
                  </body>
                </html>
            ";

            EmailSendCommand email = new EmailSendCommand()
            {
                RecipientAdress = user.Email,
                RecipientName = user.FirstName,
                Subject = "Alterar Senha",
                Text = text,
                IsHtml = true
            };

            await emailsRepository.SendEmailAsync(email);
        }

        public async Task ResetPasswordAsync(User user, string resetToken, string password)
        {
            IdentityResult result = await userManager.ResetPasswordAsync(user, resetToken, password);

            if (!result.Succeeded)
            {
                throw new IdentityException("Could not reset password", result.Errors);
            }
        }

        public async Task<User> GetCurrentUserAsync()
        {
            string userId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                throw new Exception("No user authenticated");
            }

            User user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new Exception($"The User {userId} doesn't exist");
            }

            return user;
        }

        public async Task<User> SetUserRolesAsync(User user, int[] rolesIds)
        {
            IList<int> currentRolesIds = user.Roles.Select(x => x.Role.Id).ToList();

            IList<int> rolesIdsToAdd = rolesIds.Where(r => !currentRolesIds.Contains(r)).ToList();
            IList<int> rolesIdsToRemove = currentRolesIds.Where(cr => !rolesIds.Contains(cr)).ToList();

            IEnumerable<Role> roles = rolesRepository.Query().Where(r => rolesIdsToAdd.Concat(rolesIdsToRemove).Contains(r.Id));

            await RemoveRoleFromUserAsync(user, roles.Where(r => rolesIdsToRemove.Contains(r.Id)).Select(r => r.Name).ToArray());
            await AddRoleToUserAsync(user, roles.Where(r => rolesIdsToAdd.Contains(r.Id)).Select(r => r.Name).ToArray());

            return user;
        }

        private async Task<User> AddRoleToUserAsync(User user, string[] rolesNames)
        {
            IdentityResult result = await userManager.AddToRolesAsync(user, rolesNames);

            if (!result.Succeeded)
            {
                throw new IdentityException("Could not assign roles to user", result.Errors);
            }

            return user;
        }

        private async Task<User> RemoveRoleFromUserAsync(User user, string[] rolesNames)
        {


            IdentityResult result = await userManager.RemoveFromRolesAsync(user, rolesNames);

            if (!result.Succeeded)
            {
                throw new IdentityException("Could not remove roles from user", result.Errors);
            }

            return user;
        }
        public async Task<IList<string>> GetRolesByUserAsync(User user)
        {
            return await userManager.GetRolesAsync(user);
        }

        public User ChangeStatus(User user)
        {
            user.SetActive(!user.Active);
            usersRepository.Edit(user);

            return user;
        }

        public async Task<User> UserEditAsync(User user, string email, string firstName, string lastName)
        {
            user.SetEmail(email);
            user.SetFirstName(firstName);
            user.SetLastName(lastName);

            IdentityResult result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
                throw new IdentityException("Erro ao editar o usuário", result.Errors);

            return user;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            User user = await userManager.FindByEmailAsync(email);

            if (user == null)
                throw new Exception($"Email não cadastrado: {email}");

            return user;
        }

        public async Task<bool> ConfirmCurrentUserPassword(string password)
        {
            User user = await GetCurrentUserAsync();

            return await userManager.CheckPasswordAsync(user, password);
        }
    }
}
