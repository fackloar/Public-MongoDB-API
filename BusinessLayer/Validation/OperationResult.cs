namespace Safe_Development.BusinessLayer.Validation
{
    public class OperationResult<TResult> : IOperationResult<TResult>
    {
        public TResult Result { get; }

        public IReadOnlyList<IOperationFailure> Failures { get; }

        public bool Succeed { get; set; }

        public OperationResult(TResult result)
        {
            Result = result;
            Succeed = true;
        }

        public OperationResult(IReadOnlyList<IOperationFailure> failures)
        {
            Failures = failures;
        }
    }
}
