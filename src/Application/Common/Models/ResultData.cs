namespace EmployeeControl.Application.Common.Models;

public sealed class ResultData<TData> : Result
{
    private ResultData(TData data, bool succeeded, IEnumerable<string> errors)
        : base(succeeded, errors)
    {
        Data = data;
    }

    public TData Data { get; set; }

    public static ResultData<TData> Success(TData data)
    {
        return new ResultData<TData>(data, true, Array.Empty<string>());
    }
}
