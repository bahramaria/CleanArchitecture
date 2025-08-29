using Framework.DomainRules.Extensions;
using Infrastructure.CommoditySystem;

namespace CleanArchitecture.Ordering.Domain.Services.BusinessRules;

internal class CustomerCommodityRule(ICommoditySystem commoditySystem, CustomerCommodityRule.Inquiry inquiry) : IBusinessRule
{
    public readonly struct Inquiry
    {
        public required int CustomerId { get; init; }
        public required int CommodityId { get; init; }
    }

    public async IAsyncEnumerable<Clause> Evaluate()
    {
        var result = await commoditySystem.Handle(new CustomerCommodityValidationRequest
        {
            CustomerId = inquiry.CustomerId,
            CommodityId = inquiry.CommodityId,
        }, default);

        if (result.IsFailure)
        {
            foreach (var error in result.Errors)
            {
                yield return error.ToClause();
            }
        }
        else
        {
            yield return new Clause
            (
                result.Value,
                Resources.RuleMessages.CustomerCommodityRelationRule,
                (nameof(Inquiry.CustomerId), inquiry.CustomerId),
                (nameof(Inquiry.CommodityId), inquiry.CommodityId)
            );
        }
    }
}
