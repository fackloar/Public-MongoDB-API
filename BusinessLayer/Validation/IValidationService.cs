namespace Safe_Development.BusinessLayer.Validation
{
    public interface IValidationService<TEntity> where TEntity : class
    {
        IReadOnlyList<IOperationFailure> Validate(TEntity item);
    }
}
