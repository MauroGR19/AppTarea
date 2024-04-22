using IdentityModel;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using AppTarea.Dominio.Response;

namespace AppTarea.Infraestructura.APII.Utilities
{
    public class JWToken
    {
        public Task<Response<JwtSecurityToken>> DecodificarJwtAsync(string? auth)
        {
            try
            {
                var jsonToken = new JwtSecurityToken();
                if (!auth.IsNullOrEmpty())
                {
                    string token = auth!.Substring("Bearer ".Length).Trim();
                    string[] parts = token.Split('.');
                    string header = "";
                    string payload = parts[1];
                    string signature = parts[2];
                    if (payload.IsNullOrEmpty() || signature.IsNullOrEmpty())
                    {
                        return Task.FromResult(Response<JwtSecurityToken>.Success(null!, ""));
                    }
                    var tokenHandler = new JwtSecurityTokenHandler();

                    jsonToken = tokenHandler.ReadJwtToken(token);
                    if (jsonToken.Header.TryGetValue("nonce", out object? nonceAsObject))
                    {
                        string? plainNonce = nonceAsObject.ToString();
                        using (SHA256 sha256 = SHA256.Create())
                        {
                            byte[] hashedNonceAsBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(plainNonce!));
                            string hashedNonce = Base64Url.Encode(hashedNonceAsBytes);
                            jsonToken.Header.Remove("nonce");
                            jsonToken.Header.Add("nonce", hashedNonce);
                            header = tokenHandler.WriteToken(jsonToken).Split('.')[0];

                            jsonToken = tokenHandler.ReadJwtToken($"{header}.{payload}.{signature}");
                        }
                    }
                    return Task.FromResult(Response<JwtSecurityToken>.Success(jsonToken, "OK."));
                }
                else
                {
                    return Task.FromResult(Response<JwtSecurityToken>.Success(null!, ""));
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(Response<JwtSecurityToken>.Error(null!, "", ex));
            }
        }
    }
}