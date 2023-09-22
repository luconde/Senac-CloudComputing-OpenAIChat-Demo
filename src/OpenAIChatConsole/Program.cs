using OpenAI_API.Completions;
using Microsoft.Extensions.Configuration;
using System.Reflection;

// Capturando as configurações do arquivo de configurações
var builder = new ConfigurationBuilder();
builder.SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
IConfiguration config = builder.Build();
string strOpenAIAppKey = config["OpenAI:AppKey"];

// Cria o objeto da API para OpenAI
var api = new OpenAI_API.OpenAIAPI(strOpenAIAppKey);

// Prepara as variaveis de pergunta e resposta
string strQuestion = string.Empty;
string strAnswer = string.Empty;

// Parametros de Versão
Console.WriteLine(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileVersionInfo.ProductName);
Console.WriteLine(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileVersionInfo.LegalCopyright);
Console.WriteLine(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileVersionInfo.ProductVersion);

// Enquanto o usuario não pressionar CTRL + C mantenha o loop de pedido de pergunta
while (true)
{
    Console.WriteLine("Aperte CTRL + C para fechar o aplicativo.");
    Console.WriteLine("-----------------------------------------");
    Console.WriteLine("Digite a sua questão:");
    strQuestion = Console.ReadLine();

    // Envia para o OpenAI o pedido de resposta com máximo de 1,024 tokens
    var results = api.Completions.CreateCompletionsAsync(
        new CompletionRequest(
            strQuestion, temperature: 0.1, max_tokens: 1024,
            model: OpenAI_API.Models.Model.DavinciText
            )
        , 1).Result;

    // Exibe as respostas
    strAnswer = results.Completions[0].Text;
    Console.WriteLine(strAnswer);
    Console.WriteLine();
    Console.WriteLine("-----------------------------------------");
}
