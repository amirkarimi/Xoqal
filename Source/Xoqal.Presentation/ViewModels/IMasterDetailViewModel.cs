#region License
// IMasterDetailViewModel.cs
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
    /// <summary>
    /// Represents a master-detail CRUD view-model.
    /// </summary>
    /// <typeparam name="TMasterViewModel"> The type of the master view model. </typeparam>
    /// <typeparam name="TDetailViewModel"> The type of the detail view model. </typeparam>
    public interface IMasterDetailViewModel<TMasterViewModel, TDetailViewModel> : IRefreshableViewModel
        where TMasterViewModel : IItemCollectionViewModel, IRefreshableViewModel
        where TDetailViewModel : IDetailViewModel, IRefreshableViewModel
    {
        /// <summary>
        /// Gets the master view model.
        /// </summary>
        TMasterViewModel MasterViewModel { get; }

        /// <summary>
        /// Gets the detail view model.
        /// </summary>
        TDetailViewModel DetailViewModel { get; }
    }
}
