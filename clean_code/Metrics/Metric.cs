using System;
using CleanCode.Properties;

namespace CleanCode.Metrics
{
    public class Metric : IMetric
    {
        public short MaximumSizeOfEmbeddedBlock => Convert.ToInt16(Resources.Embeded_Blocks_Max_Line);

        public short MaximumNumberOfMethodParameters => Convert.ToInt16(Resources.Function_max_Parameter);

        public short MaximumSizeOfMethod => Convert.ToInt16(Resources.Function_Max_Size);

        public short AcceptableLcom4 => Convert.ToInt16(Resources.LCOM4_metric);

        public short MaximumLevelOfNestedStructure => Convert.ToInt16(Resources.Max_Nested_level);

        public short MinimumLengthOfName => Convert.ToInt16(Resources.Name_min_Len);
    }
}