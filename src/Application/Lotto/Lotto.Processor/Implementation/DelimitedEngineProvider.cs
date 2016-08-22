// <copyright file="DelimitedEngineProvider.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using FileHelpers;
using FileHelpers.Dynamic;
using FileHelpers.Options;
using Lotto.Model.Entities.Hub;
using Lotto.Model.Entities.Process;
using Lotto.Processor.Interfaces;

namespace Lotto.Processor.Implementation
{
    public class DelimitedEngineProvider : IDataProvider
    {
        public List<LotteryDrawing> Provide(string fileName, LotterySourceConfig config, List<LotterySourceColumnConfig> columns)
        {
            DelimitedClassBuilder cb = new DelimitedClassBuilder("Combination_" + config.Id, config.FieldDelimiter)
            {
                IgnoreEmptyLines = true,
                IgnoreFirstLines = config.HeadersCount,
                IgnoreLastLines = config.FootersCount
            };

            foreach (var column in columns)
            {
                cb.AddField(column.GetColumnName(), Type.GetType(column.DotNetTypeName));
            }

            Type dynamicallyCreatedRecordClass = cb.CreateRecordClass();
            FileHelperEngine engine = new FileHelperEngine(dynamicallyCreatedRecordClass);
            DataTable parsedColumns = engine.ReadFileAsDT(fileName);

            var result = new List<LotteryDrawing>();
            foreach (DataRow dataRow in parsedColumns.Rows)
            {
                var drawing = new LotteryDrawing { Combination = new List<int>() };
                foreach (var column in columns.Where(c => c.BelongsToCombination))
                {
                    drawing.Combination.Add(dataRow.Field<int>(column.GetColumnName()));
                }
                result.Add(drawing);
            }

            return result;
        }
    }
}
