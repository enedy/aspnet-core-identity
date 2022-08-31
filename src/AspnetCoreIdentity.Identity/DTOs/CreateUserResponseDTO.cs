using System.Collections.Generic;

namespace AspnetCoreIdentity.Identity.DTOs
{
    public class CreateUserResponseDTO
    {
        public bool Success { get; private set; }
        public List<string> Errors { get; private set; }

        public CreateUserResponseDTO() =>
            Errors = new List<string>();

        public CreateUserResponseDTO(bool sucesso = true) : this() =>
            Success = sucesso;

        public void AddErrors(IEnumerable<string> erros) =>
            Errors.AddRange(erros);
    }
}
