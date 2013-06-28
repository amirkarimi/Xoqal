#region License
// CrudViewModel{TModel}.cs
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

namespace Xoqal.Presentation.ViewModels
{
    using System;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;
    using Resources;
    using Xoqal.Core.Models;
    using Xoqal.Data;
    using Xoqal.Presentation.Commands;
    using Xoqal.Services;

    /// <summary>
    /// Represents the base functionality for a simple view-model CRUD operation.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public abstract class CrudViewModel<TModel> : CrudViewModel<TModel, PaginatedCriteria>
        where TModel : class, new()
    {
        private readonly ICrudService<TModel> service;

        /// <summary>
        /// Initializes a new instance of the <see cref="CrudViewModel{TModel}" /> class.
        /// </summary>
        /// <param name="service"></param>
        protected CrudViewModel(ICrudService<TModel> service)
            : base(service)
        {
            this.service = service;
        }
    }
}
