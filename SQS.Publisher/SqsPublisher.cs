using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using System.Text.Json;

namespace SQS.Publisher;

public class SqsPublisher : ISqsPublisher
{
    private readonly IAmazonSQS _sqsClient;
    public string BaseUrl { get; set; }
    public string Queue { get; set; }

    public SqsPublisher()
    {
        BaseUrl = Environment.GetEnvironmentVariable("SQSBaseUrl")!;
        AmazonSQSConfig config = new() { ServiceURL = BaseUrl };
        _sqsClient = new AmazonSQSClient(config);
        Queue = Environment.GetEnvironmentVariable("SQSQueue")!;
    }

    public SqsPublisher(IAmazonSQS client, string baseUrl, string queueUrl)
    {
        _sqsClient = client ?? throw new ArgumentNullException(nameof(client));
        BaseUrl = baseUrl ?? throw new ArgumentNullException(nameof(baseUrl));
        Queue = queueUrl ?? throw new ArgumentNullException(nameof(queueUrl));
    }

    public async Task<SendMessageResponse> SendMessage(object model, CancellationToken cancellationToken)
    {
        SendMessageRequest request = new()
        {
            QueueUrl = $"{BaseUrl}/queue/{Queue}",
            MessageBody = JsonSerializer.Serialize(model)
        };

        var response = await _sqsClient.SendMessageAsync(request, cancellationToken);
        return response;
    }

    public async Task<SendMessageResponse> SendMessage(object model)
    {
        return await SendMessage(model, CancellationToken.None);
    }
}