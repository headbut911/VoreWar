using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

public class ConditionalTraitContainer
{
    internal int id = -1;
    internal string name;
    internal TraitConditionalClassification classification;
    internal bool active = false;
    internal TraitConditionTrigger trigger;

    internal Traits associatedTrait;

    internal Dictionary<ConditionalTraitOperationBlock, TraitConditionLogicalOperator> OperationBlocks;
}