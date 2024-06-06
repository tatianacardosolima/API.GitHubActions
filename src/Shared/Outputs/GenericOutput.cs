namespace Fiap.Clientes.Shared.Outputs
{
    public class GenericOutput
    {
        public GenericOutput(bool sucesso, string message, object data)
        {
            Sucesso = sucesso;
            Message = message;
            Data = data;
        }
        public bool Sucesso { get; set; }
        public string Message { get; set; }
        public object Data{ get; set; }
    }
}
