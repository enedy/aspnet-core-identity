using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AspnetCoreIdentity.Identity.DTOs.Response
{
    public class UserLoginReponseDTO
    {
        public bool Success => Errors.Count == 0;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string AccessToken { get; private set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string RefreshToken { get; private set; }

        public List<string> Errors { get; private set; }

        public UserLoginReponseDTO() =>
            Errors = new List<string>();

        public UserLoginReponseDTO(bool success, string accessToken, string refreshToken) : this()
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public void AddError(string erro) =>
            Errors.Add(erro);

        public void AdicionarErrors(IEnumerable<string> erros) =>
            Errors.AddRange(erros);
    }
}
