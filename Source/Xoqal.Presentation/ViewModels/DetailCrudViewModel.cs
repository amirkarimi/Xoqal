#region License
// DetailCrudViewModel.cs
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
    using System.Collections.Generic;
    using Xoqal.Core.Models;
    using Xoqal.Services;

    /// <summary>
    /// Represents the base class for simple CRUD view-model which works as a detail.
    /// </summary>
    public abstract class DetailCrudViewModel<TDetailModel, TMasterModel, TDetailCriteria, TMasterCriteria> : CrudViewModel<TDetailModel, TDetailCriteria>, IDetailViewModel<TMasterModel>
        where TDetailModel : class, new() 
        where TMasterModel : class, new()
        where TDetailCriteria : DetailPaginatedCriteria<TMasterModel>, new()
        where TMasterCriteria : PaginatedCriteria, new()
    {
        private TMasterModel masterCurrentItem;

        /// <summary>
        /// Initializes a new instance of the <see cref="DetailCrudViewModel{TDetailModel, TMasterModel, TDetailCriteria, TMasterCriteria}" /> class.
        /// </summary>
        /// <param name="service"></param>
        public DetailCrudViewModel(ICrudService<TDetailModel, TDetailCriteria> service)
            : base(service)
        {
        }

        #region IDetailViewModel<TMaster> Members

        /// <summary>
        /// Gets or sets the master current item.
        /// </summary>
        /// <value> The master current item. </value>
        public TMasterModel MasterCurrentItem
        {
            get
            {
                return this.masterCurrentItem;
            }

            set
            {
                if (this.masterCurrentItem == value)
                {
                    return;
                }

                this.masterCurrentItem = value;
                this.Criteria.MasterCurrentItem = value;
                this.RaisePropertyChanged(() => this.MasterCurrentItem);
            }
        }

        /// <summary>
        /// Gets or sets the master current item.
        /// </summary>
        /// <value> The master current item. </value>
        object IDetailViewModel.MasterCurrentItem
        {
            get { return this.MasterCurrentItem; }
            set { this.MasterCurrentItem = (TMasterModel)value; }
        }

        #endregion

        /// <summary>
        /// Determines whether this instance can execute AddCommand.
        /// </summary>
        /// <returns> <c>true</c> if this instance can execute AddCommand; otherwise, <c>false</c> . </returns>
        protected override bool CanExecuteAddCommand()
        {
            return this.MasterCurrentItem != null && base.CanExecuteAddCommand();
        }
    }
}
