#region License
// ExcelExportImportTest.cs
// 
// Copyright (c) 2012 Xoqal.com
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xoqal.ExportImport;

namespace Xoqal.Tests.ExportImportTests
{
    [TestClass]
    public class ExcelExportImportTest
    {
        private SampleModel[] sampleData;
        [TestInitialize]
        public void Initialize()
        {
            // I want to suppress the second part of the time.
            var now = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);

            sampleData = new SampleModel[]
            {
                new SampleModel { Id = 1, Title = "Test1", Price = 10000m, Title2 = "12,032", DateTime = now, Gender = Gender.Man }, 
                new SampleModel { Id = 2, Title = "Test2", Price = 20000m, Title2 = "32,123", DateTime = now.AddDays(1), Gender = Gender.Woman },
                new SampleModel { Id = 3, Title = "Test3", Price = 30000m, Title2 = "42,032", DateTime = now.AddDays(3), Gender = Gender.Man },
                new SampleModel { Id = 4, Title = "Test4", Price = 40000m, Title2 = "15,342", DateTime = now.AddDays(-1), Gender = Gender.Woman }
            };
        }

        [TestMethod]
        public void ExportImportWorksCorrectlyWithPersianDate()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("fa");
            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Threading.Thread.CurrentThread.CurrentCulture;

            ExcelExporter<SampleModel> exporter = new ExcelExporter<SampleModel>();
            MemoryStream stream = new MemoryStream();
            exporter.Export(sampleData, stream);

            ExcelImporter<SampleModel> importer = new ExcelImporter<SampleModel>();
            var importedData = importer.Import(stream).ToArray();

            Assert.IsTrue(sampleData.SequenceEqual(importedData, new SampleModelEqualityComparer()), "Exported data are not equal to the imported ones");
        }

        [TestMethod]
        public void ExportImportWorksCorrectlyWithNonPersianDate()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en");
            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Threading.Thread.CurrentThread.CurrentCulture;

            var exporter = new ExcelExporter<SampleModel>();
            Stream stream = new MemoryStream();
            exporter.Export(sampleData, stream);

            var importer = new ExcelImporter<SampleModel>();
            var importedData = importer.Import(stream).ToArray();

            Assert.IsTrue(sampleData.SequenceEqual(importedData, new SampleModelEqualityComparer()), "Exported data are not equal to the imported ones");
        }

        [TestMethod]
        public void ExportImportWorksCorrectlyWithIgnoreAttribute()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en");
            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Threading.Thread.CurrentThread.CurrentCulture;

            var now = DateTime.Now;
            var sampleDataWithIgnoredData = new SampleModel3[]
            {
                new SampleModel3 { Id = 1, Title = "Test1", Price = 10000m, Title2 = "12,032", DateTime = now, Gender = Gender.Man }, 
                new SampleModel3 { Id = 2, Title = "Test2", Price = 20000m, Title2 = "32,123", DateTime = now.AddDays(1), Gender = Gender.Woman },
                new SampleModel3 { Id = 3, Title = "Test3", Price = 30000m, Title2 = "42,032", DateTime = now.AddDays(3), Gender = Gender.Man },
                new SampleModel3 { Id = 4, Title = "Test4", Price = 40000m, Title2 = "15,342", DateTime = now.AddDays(-1), Gender = Gender.Woman }
            };
            
            var exporter = new ExcelExporter<SampleModel3>();
            Stream stream = new MemoryStream();
            exporter.Export(sampleDataWithIgnoredData, stream);

            var importer = new ExcelImporter<SampleModel3>();
            var importedData = importer.Import(stream).ToArray();

            Assert.IsTrue(importedData.All(d => d.Price == default(decimal)), "Ignored data is present yet!");
        }

        [TestMethod]
        public void ExportImportWorksCorrectlyOnDifferentClasses()
        {
            var exporter = new ExcelExporter<SampleModel>();
            MemoryStream stream = new MemoryStream();
            exporter.Export(sampleData, stream);

            var importer = new ExcelImporter<SampleModel2>();
            var importedData = importer.Import(stream).ToArray();

            Assert.IsTrue(sampleData.All(s =>
            {
                var importedItem = importedData.First(i => i.Id == s.Id);
                return importedItem.Title == s.Title &&
                    importedItem.Title2 == s.Title2 &&
                    importedItem.Price == s.Price &&
                    importedItem.Gender == s.Gender &&
                    importedItem.DateTime == s.DateTime;
            }), "Exported data are not equal to the imported ones");
        }

        [TestMethod]
        public void ExportImportWorksCorrectlyWithFileStream()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en");
            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Threading.Thread.CurrentThread.CurrentCulture;

            string filePath = Path.GetTempFileName() + ".xlsx";

            var exporter = new ExcelExporter<SampleModel>();
            using (Stream outStream = new FileStream(filePath, FileMode.Create))
            {
                exporter.Export(sampleData, outStream);
            }

            SampleModel[] importedData;
            var importer = new ExcelImporter<SampleModel>();
            using (Stream inStream = new FileStream(filePath, FileMode.Open))
            {
                importedData = importer.Import(inStream).ToArray();
            }

            Assert.IsTrue(sampleData.SequenceEqual(importedData, new SampleModelEqualityComparer()), "Exported data are not equal to the imported ones");
        }

        public class SampleModelEqualityComparer : IEqualityComparer<SampleModel>
        {
            public bool Equals(SampleModel x, SampleModel y)
            {
                return x.Id == y.Id &&
                    x.Title == y.Title &&
                    x.Title2 == y.Title2 &&
                    x.Price == y.Price &&
                    x.DateTime == y.DateTime &&
                    x.Gender == y.Gender;
            }

            public int GetHashCode(SampleModel obj)
            {
                return (int)obj.Id;
            }
        }

        public class SampleModel
        {
            public long Id { get; set; }

            [Display(Name = "عنوان")]
            public string Title { get; set; }

            public string Title2 { get; set; }

            public decimal Price { get; set; }

            public DateTime DateTime { get; set; }

            [Display(Name = "جنسیت")]
            public Gender Gender { get; set; }
        }

        public class SampleModel2
        {
            [Display(Name = "عنوان")]
            public string Title { get; set; }

            public decimal Price { get; set; }

            public long Id { get; set; }

            public DateTime DateTime { get; set; }

            public string Title2 { get; set; }

            [Display(Name = "جنسیت")]
            public Gender Gender { get; set; }
        }

        public class SampleModel3
        {
            [Display(Name = "عنوان")]
            public string Title { get; set; }

            [Xoqal.ExportImport.Attributes.Ignore]
            public decimal Price { get; set; }

            public long Id { get; set; }

            public DateTime DateTime { get; set; }

            public string Title2 { get; set; }

            [Display(Name = "جنسیت")]
            public Gender Gender { get; set; }
        }

        public enum Gender
        {
            [Display(Name = "مرد")]
            Man,

            [Display(Name = "زن")]
            Woman
        }
    }
}
