namespace Safe_Development.BusinessLayer.Validation
{
    public interface IOperationResult<TResult>
    {
        TResult Result { get; }
        IReadOnlyList<IOperationFailure> Failures { get; }
        bool Succeed { get; }
    }
}
