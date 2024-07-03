namespace EstudoDeCaso.Infra.Services.Interfaces
{
    public interface IHelperService
    {
        DateTime? RetornaDataDoArquivo(string nomeArquivo);
        string RetornaTamanhoDoArquivo(string nomeArquivo);
    }
}
