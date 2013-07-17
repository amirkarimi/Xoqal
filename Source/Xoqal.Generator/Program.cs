#region License
// Program.cs
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

namespace Xoqal.Generator
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public class Program
    {
        /// <summary>
        /// Main method.
        /// </summary>
        /// <param name="args">The args.</param>
        public static void Main(string[] args)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Xoqal Generator - Copyright (c) 2012 Xoqal.com");
            Console.ResetColor();

            if (args.Length < 3)
            {
                Console.WriteLine("Usage: Xoqal.Generator (project-name) (entities-assembly) (output-folder) [excluded-entities(comma seperated)]");
                return;
            }

            var options = new GeneratorOptions();

            options.ProjectName = args[0];
            options.EntitiesAssemblyPath = args[1];
            options.OutputFolder = args[2];
            if (args.Length >= 4)
            {
                options.ExcludedEntities = args[3].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            }            

            CheckDirectoryExistance(options.OutputFolder);
            CheckDirectoryExistance(options.DataDirectory);
            CheckDirectoryExistance(options.ServicesDirectory);

            var entitiesAssembly = Assembly.LoadFrom(options.EntitiesAssemblyPath);

            options.EntityInfoes = entitiesAssembly.GetTypes()
                .Where(type =>
                    type.IsPublic &&
                    type.IsClass &&
                    type.Namespace == options.EntitiesNamespace &&
                    !type.GetCustomAttributes(true).Any(ca => ca.GetType().Name == "ComplexTypeAttribute") &&
                    !options.ExcludedEntities.Contains(type.Name))
                .Select(type => new EntityInfo(type))
                .ToArray();

            // Set inherited entities
            foreach (var entityInfo in options.EntityInfoes)
            {
                entityInfo.IsInheritedEntity = options.EntityInfoes.Any(e => e.EntityType.Equals(entityInfo.EntityType.BaseType));
            }

            var entityInfoes = new List<EntityInfo>();
            foreach (var entityInfo in options.EntityInfoes)
            {
                GenerateRepository(options, entityInfo);
                GenerateService(options, entityInfo);
                entityInfoes.Add(entityInfo);
            }

            GenerateDbContext(options);
        }

        private static void CheckDirectoryExistance(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private static void GenerateRepository(GeneratorOptions options, EntityInfo entityInfo)
        {
            string repositoryInterfaceFilePath = Path.Combine(options.DataDirectory, string.Format("I{0}Repository.cs", entityInfo.EntityName));
            string repositoryClassFilePath = Path.Combine(options.DataDirectory, string.Format("{0}Repository.cs", entityInfo.EntityName));

            var repositoryInterfaceTemplate = new Templates.RepositoryInterfaceTemplate(options, entityInfo);
            File.WriteAllText(repositoryInterfaceFilePath, repositoryInterfaceTemplate.TransformText());

            var repositoryClassTemplate = new Templates.RepositoryClassTemplate(options, entityInfo);
            File.WriteAllText(repositoryClassFilePath, repositoryClassTemplate.TransformText());
        }

        private static void GenerateService(GeneratorOptions options, EntityInfo entityInfo)
        {
            string serviceInterfaceFilePath = Path.Combine(options.ServicesDirectory, string.Format("I{0}Service.cs", entityInfo.EntityName));
            string serviceClassFilePath = Path.Combine(options.ServicesDirectory, string.Format("{0}Service.cs", entityInfo.EntityName));

            var serviceInterfaceTemplate = new Templates.ServiceInterfaceTemplate(options, entityInfo, new CodeConventionService());
            File.WriteAllText(serviceInterfaceFilePath, serviceInterfaceTemplate.TransformText());

            var serviceClassTemplate = new Templates.ServiceClassTemplate(options, entityInfo, new CodeConventionService());
            File.WriteAllText(serviceClassFilePath, serviceClassTemplate.TransformText());
        }

        private static void GenerateDbContext(GeneratorOptions options)
        {
            var template = new Templates.DbContextTemplate(options, new CodeConventionService());
            File.WriteAllText(Path.Combine(options.DataDirectory, string.Format("{0}Context.cs", options.ProjectName)), template.TransformText());
        }
    }
}
