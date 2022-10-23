using System.ComponentModel;

namespace NewsAPI.SharedKernel;

public enum ErrorCode
{
    [Description("Category dos not exist")]
    CATEGORY_NOT_FOUND = 10,
}
