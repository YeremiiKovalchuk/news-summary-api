using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NewsSummary.Infrastructure.Models;

public class RedisRetryPolicy: IValidatableObject
{
    public int RetryCount { get; init; }
    public int ExponentialRetryTimeout { get; init; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var messageBuilder = new StringBuilder();
        if (RetryCount <= 0 || RetryCount >= 101)
        {
            messageBuilder.AppendLine($"{nameof(RetryCount)} must be between 0 and 100");
        }
        if(ExponentialRetryTimeout <= 0 || ExponentialRetryTimeout >= 30001)
        {
            messageBuilder.AppendLine($"{nameof(ExponentialRetryTimeout)} must be between 1 and 30000");
        }
        var message = messageBuilder.ToString();
        yield return  message == "" ? ValidationResult.Success! : new ValidationResult(message);
    }
}
