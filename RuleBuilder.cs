using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace ValidationExperiments;

public class RuleSetBuilder<TModel>
{
    private Dictionary<string, IRule> _rules = new Dictionary<string, IRule>();
    private Dictionary<string, PropertyInfo> _propertyInfos = new Dictionary<string, PropertyInfo>();
    public IRule<TProperty> RuleFor<TProperty>(Expression<Func<TModel, TProperty>> expression)
    {
        Type type = typeof(TModel);
        var propertyInfo = type.GetPropertyInfo(expression);
        var rule = new Rule<TProperty>(propertyInfo.Name);
        _rules.Add(rule.Name, rule);
        _propertyInfos.Add(rule.Name, propertyInfo);
        return rule;
    }

    public RuleSet Build()
    {
        return new RuleSet(nameof(TModel), _rules);
    }
}