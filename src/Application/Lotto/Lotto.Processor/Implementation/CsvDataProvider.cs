// <copyright file="CsvDataProvider.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using FileHelpers;
using Lotto.Model.Entities.Hub;
using Lotto.Model.Entities.Process;
using Lotto.Processor.Interfaces;

namespace Lotto.Processor.Implementation
{
    internal class CsvDataProvider : IDataProvider
    {
        public List<LotteryDrawing> Provide(string fileName, LotterySourceConfig config, List<LotterySourceColumnConfig> columns)
        {
            List<LotteryDrawing> items = new List<LotteryDrawing>();
            DelimitedFileEngine engine = new DelimitedFileEngine(typeof(FileFormat));
            engine.Options.Delimiter = ";";
            FileFormat[] records = engine.ReadFile(fileName) as FileFormat[];

            foreach (FileFormat t in records)
            {
                LotteryDrawing drawing = new LotteryDrawing { Id = t.DrawingNumber };
                drawing.Combination.Add(t.Ball1);
                drawing.Combination.Add(t.Ball2);
                drawing.Combination.Add(t.Ball3);
                drawing.Combination.Add(t.Ball4);
                drawing.Combination.Add(t.Ball5);
                drawing.Combination.Add(t.Ball6);
                drawing.Combination.Add(t.Ball7);
                drawing.Combination.Add(t.Ball8);
                drawing.Combination.Add(t.Ball9);
                drawing.Combination.Add(t.Ball10);
                drawing.Combination.Add(t.Ball11);
                drawing.Combination.Add(t.Ball12);
                drawing.Combination.Add(t.Ball13);
                drawing.Combination.Add(t.Ball14);
                drawing.Combination.Add(t.Ball15);
                drawing.Combination.Add(t.Ball16);
                drawing.Combination.Add(t.Ball17);
                drawing.Combination.Add(t.Ball18);
                drawing.Combination.Add(t.Ball19);
                drawing.Combination.Add(t.Ball20);
                items.Add(drawing);
            }
            return items;
        }
    }
}
