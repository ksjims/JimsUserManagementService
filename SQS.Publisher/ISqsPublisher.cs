using Amazon.SQS.Model;

namespace SQS.Publisher;

public interface ISqsPublisher
{
    Task<SendMessageResponse> SendMessage(object model, CancellationToken cancellationToken);
    Task<SendMessageResponse> SendMessage(object model);
}
