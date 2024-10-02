using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NewsSummary.Infrastructure.Models;

public class RedisRetryPolicy: IValidatableObject
{
    public const int RetryCountUpperLimit = 100;
    public const int RetryTimeoutUpperLimit = 30000;
    public int RetryCount { get; init; }
    public int ExponentialRetryTimeout { get; init; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var messageBuilder = new StringBuilder();
        if (RetryCount < 0 || RetryCount > RetryCountUpperLimit)
        {
            messageBuilder.AppendLine($"{nameof(RetryCount)} must be between 0 and {RetryCountUpperLimit}");
        }
        if(ExponentialRetryTimeout < 1 || ExponentialRetryTimeout > RetryTimeoutUpperLimit)
        {
            messageBuilder.AppendLine($"{nameof(ExponentialRetryTimeout)} must be between 1 and {RetryTimeoutUpperLimit}");
        }
        var message = messageBuilder.ToString();
        yield return  message == "" ? ValidationResult.Success! : new ValidationResult(message);
    }
}
