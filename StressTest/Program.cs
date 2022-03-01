// See https://aka.ms/new-console-template for more information
using System.Text;

Console.WriteLine("Hello, World!");

int concurrency = 20;

List<Task<string>> tasks = new List<Task<string>>();

for (int i = 0; i < concurrency; i++)
{
    tasks.Add(Test());
}

List<string> result = (await Task.WhenAll(tasks)).ToList();

int j = 0;
foreach(var str in result)
{
    j++;
    Console.WriteLine(j);
    if (!str.StartsWith("{"))
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
    }
    Console.WriteLine(str);
    Console.ResetColor();
    Console.WriteLine();
}


async Task<string> Test()
{
    using (var client = new HttpClient())
    {
        client.BaseAddress = new Uri("http://internal-DemoLambdaInDockerLB-837734845.ap-southeast-2.elb.amazonaws.com");
        var content = new StringContent($"\"{Guid.NewGuid()}\"", Encoding.UTF8, "text/plain");
        var response = await client.PostAsync("/2015-03-31/functions/function/invocations", content);
        try
        {
            response.EnsureSuccessStatusCode();
            string result = await response.Content.ReadAsStringAsync();
            return result;
        } catch (Exception ex)
        {
            return ex.Message;
        }
    }
}