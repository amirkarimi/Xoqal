#region License
// CrudServiceModelDecorator{TViewModel,TPersistenceModel,TCriteria}.cs
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

namespace Xoqal.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Xoqal.Core.Models;

    /// <summary>
    /// Represents a model decorator for a <see cref="CrudService{TModel, TCriteria}" /> class.
    /// </summary>
    /// <typeparam name="TViewModel">The type of the view model.</typeparam>
    /// <typeparam name="TPersistenceModel">The type of the persistence model.</typeparam>
    /// <typeparam name="TCriteria">The type of the criteria.</typeparam>
    public abstract class CrudServiceModelDecorator<TViewModel, TPersistenceModel, TCriteria> : ICrudService<TViewModel, TCriteria>, ITwoWayMapper<TPersistenceModel, TViewModel>
        where TViewModel : class
        where TPersistenceModel : class
        where TCriteria : PaginatedCriteria
    {
        private readonly ICrudService<TPersistenceModel, TCriteria> persistenceService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CrudServiceModelDecorator{TViewModel, TPersistenceModel, TCriteria}"/> class.
        /// </summary>
        /// <param name="persistenceService">The persistence service.</param>
        public CrudServiceModelDecorator(ICrudService<TPersistenceModel, TCriteria> persistenceService)
        {
            this.persistenceService = persistenceService;
        }

        /// <summary>
        /// Gets the item by key.
        /// </summary>
        /// <param name="keys">The keys.</param>
        /// <returns></returns>
        public virtual TViewModel GetItemByKey(params object[] keys)
        {
            return this.SafeMap(this.persistenceService.GetItemByKey(keys));
        }

        /// <summary>
        /// Adds the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public virtual void Add(TViewModel model)
        {
            this.persistenceService.Add(this.SafeMap(model));
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public virtual void Update(TViewModel model)
        {
            // Load and change the properties in this way to prevent disconnected update and lost of untouched properties.            
            // Create a new persistence model
            var persistenceModel = this.SafeMap(model);

            // Reload the persistence model by its DB data
            persistenceModel = this.persistenceService.Reload(persistenceModel);

            // Update the loaded persistence model properties using the view model
            this.SafeMap(model, persistenceModel);

            // Update
            this.persistenceService.Update(persistenceModel);
        }

        /// <summary>
        /// Removes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public virtual void Remove(TViewModel model)
        {
            this.persistenceService.Remove(this.SafeMap(model));
        }

        /// <summary>
        /// Removes the specified models.
        /// </summary>
        /// <param name="models">The models.</param>
        public virtual void Remove(IEnumerable<TViewModel> models)
        {
            this.persistenceService.Remove(models.Select(vm => this.SafeMap(vm)));
        }

        /// <summary>
        /// Reloads the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public virtual TViewModel Reload(TViewModel model)
        {
            var persistanceModel = this.SafeMap(model);
            this.persistenceService.Reload(persistanceModel);
            this.SafeMap(persistanceModel, model);
            return model;
        }

        /// <summary>
        /// Gets all items.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TViewModel> GetAllItems()
        {
            return this.persistenceService.GetAllItems().Select(pm => this.SafeMap(pm));
        }

        /// <summary>
        /// Gets the item count.
        /// </summary>
        /// <returns></returns>
        public virtual int GetItemCount()
        {
            return this.persistenceService.GetItemCount();
        }

        /// <summary>
        /// Gets an <see cref="IPaginated"/> instance according to the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public abstract IPaginated<TViewModel> GetItems(TCriteria criteria);

        /// <summary>
        /// Maps the specified source object to the destination type.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination which will be instantiate if it is null.</param>
        /// <returns></returns>
        public abstract TPersistenceModel Map(TViewModel source, TPersistenceModel destination = null);

        /// <summary>
        /// Maps the specified source object to the destination type.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination which will be instantiate if it is null.</param>
        /// <returns></returns>
        public abstract TViewModel Map(TPersistenceModel source, TViewModel destination = null);

        /// <summary>
        /// Maps the specified source object to the destination type. If the source type was 
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <returns></returns>
        private TPersistenceModel SafeMap(TViewModel source, TPersistenceModel destination = null)
        {
            if (source == null)
            {
                return null;
            }

            return this.Map(source, destination);
        }

        /// <summary>
        /// Maps the specified source object to the destination type.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination which will be instantiate if it is null.</param>
        /// <returns></returns>
        private TViewModel SafeMap(TPersistenceModel source, TViewModel destination = null)
        {
            if (source == null)
            {
                return null;
            }

            return this.Map(source, destination);
        }
    }
}
