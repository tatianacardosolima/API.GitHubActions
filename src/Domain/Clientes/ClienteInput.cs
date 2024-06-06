namespace Fiap.Clientes.Domain.Clientes
{
    public class ClienteInput
    {
        public ClienteInput()
        {
            
        }
        public ClienteInput(string[] input)
        {
            Id = int.Parse(input[0]);
            CPF = input[1].Trim();
            Nome = input[2].Trim();
            Email = input[3].Trim();
            Telefone = input[4].Trim();
        }
        public int Id { get; set; }
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
    }
}
